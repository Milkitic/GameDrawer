using GameDrawer.IO;
using GameDrawer.Model;
using GameDrawer.Windows;
using Microsoft.VisualBasic.FileIO;
using Milkitic.WpfApi;
using Milkitic.WpfApi.Commands;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GameDrawer.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ConsoleMachine> _consoleMachines;
        private ConsoleMachine _currentMachine;
        private Game _currentGame;

        private CancellationTokenSource _cts;
        private Task _syncTask;
        private bool _windowActivated;
        private string _consoleSearchString = "";
        private string _gameSearchString;
        private ObservableCollection<ConsoleMachine> _searchedConsoleMachines;
        private Config _config;

        private bool IsScanRunning => _syncTask != null && !(_syncTask.IsCanceled || _syncTask.IsCompleted || _syncTask.IsFaulted);

        public Config Config
        {
            get => _config;
            private set
            {
                _config = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ConsoleMachine> ConsoleMachines
        {
            get => _consoleMachines;
            set
            {
                _consoleMachines = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ConsoleMachine> SearchedConsoleMachines
        {
            get => _searchedConsoleMachines;
            set
            {
                _searchedConsoleMachines = value;
                OnPropertyChanged();
            }
        }

        public ConsoleMachine CurrentMachine
        {
            get => _currentMachine;
            set
            {
                _currentMachine = value;
                OnPropertyChanged();
            }
        }

        public Game CurrentGame
        {
            get => _currentGame;
            set
            {
                _currentGame = value;
                OnPropertyChanged();
            }
        }

        public string ConsoleSearchString
        {
            get => _consoleSearchString;
            set
            {
                _consoleSearchString = value;
                OnPropertyChanged();
            }
        }
        public string GameSearchString
        {
            get => _gameSearchString;
            set
            {
                _gameSearchString = value;
                OnPropertyChanged();
            }
        }

        public GameListProperties GameListProperties { get; } = GameListLoader.GameListProperties;
        public GameListExtensionProperties GameListExtensionProperties { get; } = GameListExtension.GameListExtensionProperties;

        public ICommand ChangeEditabilityCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    if (CurrentMachine == null) return;
                    if (CurrentGame == null) return;
                    if (CurrentMachine.DescriptionEditable)
                    {
                        CurrentGame.Description = (string)obj;
                        CurrentGame.CommitChanges();
                        CurrentMachine.DescriptionEditable = false;
                    }
                    else
                    {
                        CurrentMachine.DescriptionEditable = true;
                    }
                });
            }
        }

        public bool WindowActivated
        {
            get => _windowActivated;
            set
            {
                _windowActivated = value;
                OnPropertyChanged();
            }
        }

        public ICommand ConsoleConfigCommand
        {
            get
            {
                return new DelegateCommand(async obj =>
                {
                    if (CurrentMachine == null) return;
                    var window = new ConsoleConfigWindow(CurrentMachine)
                    {
                        Owner = (MainWindow)obj
                    };

                    Execute.OnUiThread(() =>
                    {
                        try
                        {
                            window.ShowDialog();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            window.Close();
                        }
                    });

                    if (window.DialogResult != true) return;
                    if (CurrentMachine == null) return;
                    if (!Directory.Exists(CurrentMachine.Path))
                    {
                        ConsoleMachines.Remove(CurrentMachine);
                        ConsoleSearchString = "";
                        GameSearchString = "";
                        CurrentMachine = null;
                        App.GameListLoader.SaveCache();
                        App.Config.SaveConfig();
                    }
                    else
                    {
                        await CurrentMachine.Refresh();
                    }
                });
            }
        }

        public ICommand GameConfigCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    if (CurrentMachine == null) return;
                    var window = new ConsoleConfigWindow(CurrentGame);

                    Execute.OnUiThread(() =>
                    {
                        try
                        {
                            window.ShowDialog();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            window.Close();
                        }
                    });

                    //if (window.DialogResult == true)
                    //{
                    //    CurrentMachine.Refresh();
                    //}
                });
            }
        }

        public ICommand BrowseCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    if (CurrentGame == null)
                    {
                        Process.Start(CurrentMachine.RomDirectoryPath);
                    }
                    else
                    {
                        Process.Start("Explorer", "/select," + CurrentGame.Path);
                    }
                });
            }
        }

        public ICommand RenameCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    if (CurrentGame != null)
                    {
                        CurrentGame.NameEditable = true;
                    }
                });
            }
        }

        public ICommand RefreshConsole
        {
            get
            {
                return new DelegateCommand(async obj =>
                {
                    await StopScanTask();
                    await CurrentMachine.Refresh();
                    StartScanTask();
                });
            }
        }

        public ICommand RunCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    try
                    {
                        var gameConfig =
                            App.Config.GameConsoleConfigs.FirstOrDefault(k => k.Identity == CurrentGame.Identity);
                        var consoleConfig =
                            App.Config.GameConsoleConfigs.FirstOrDefault(k => k.Identity == CurrentMachine.Identity);
                        Process proc;
                        if (gameConfig != null || consoleConfig != null)
                        {
                            string host = null, args = null;
                            if (!string.IsNullOrEmpty(consoleConfig?.HostApplication))
                                host = consoleConfig?.HostApplication;
                            if (!string.IsNullOrEmpty(consoleConfig?.StartupArguments))
                                args = consoleConfig?.StartupArguments;
                            if (!string.IsNullOrEmpty(gameConfig?.HostApplication)) host = gameConfig?.HostApplication;
                            if (!string.IsNullOrEmpty(gameConfig?.StartupArguments))
                                args = gameConfig?.StartupArguments;

                            if (host == null && args == null)
                            {
                                proc = new Process
                                {
                                    StartInfo = new ProcessStartInfo
                                    {
                                        FileName = CurrentGame.Path,
                                    },
                                    EnableRaisingEvents = true
                                };
                            }
                            else
                            {
                                bool useQuote = CurrentGame.Path.Contains(' ');
                                var path = useQuote ? $"\"{CurrentGame.Path}\"" : CurrentGame.Path;
                                string arguments;
                                const string flag = "%APPPATH%";
                                if (args?.Contains(flag) == true)
                                    arguments = args.Replace(flag, path);
                                else
                                {
                                    arguments = args == null ? path : $"{path} {args}";
                                }
                                proc = new Process
                                {
                                    StartInfo = new ProcessStartInfo
                                    {
                                        FileName = host ?? CurrentGame.Path,
                                        Arguments = arguments
                                    },
                                    EnableRaisingEvents = true
                                };
                            }
                        }
                        else
                        {
                            proc = new Process
                            {
                                StartInfo = new ProcessStartInfo
                                {
                                    FileName = CurrentGame.Path,
                                },
                                EnableRaisingEvents = true
                            };
                        }

                        var window = (MainWindow)obj;
                        var prevState = window.WindowState;
                        proc.Exited += (sender, e) =>
                        {
                            Execute.OnUiThread(() =>
                            {
                                if (window.WindowState != WindowState.Minimized) return;
                                window.WindowState = prevState;
                                window.Topmost = true;
                                window.Topmost = false;
                            }, MainWindow.SynchronizationContext);
                        };
                        proc.Start();
                        window.WindowState = WindowState.Minimized;
                    }
                    catch (Exception e)
                    {
                        //if (!(e is System.ComponentModel.Win32Exception))
                        {
                            MessageBox.Show(e.Message, "错误信息", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        var window = (MainWindow)obj;
                        var prevState = window.WindowState;
                        if (window.WindowState == WindowState.Minimized)
                        {
                            window.WindowState = prevState;
                            window.Topmost = true;
                            window.Topmost = false;
                        }
                    }
                });
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return new DelegateCommand(async obj =>
                {
                    try
                    {
                        var running = IsScanRunning;
                        await StopScanTask();
                        GameSearchString = "";
                        var newName = new DirectoryInfo(CurrentGame.MetaDirectory).Name + " - " +
                                      DateTime.Now.ToString("yyyyMMddHHmmss");
                        var newPath = Path.Combine(Config.BackupDirectory, newName);
                        _currentMachine.Games.Remove(CurrentGame);
                        if (_currentMachine.VisibleGames.Contains(CurrentGame))
                            _currentMachine.VisibleGames.Remove(CurrentGame);
                        if (_currentMachine.SearchedGames.Contains(CurrentGame))
                            _currentMachine.SearchedGames.Remove(CurrentGame);
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(CurrentGame.Path,
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                            Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                        new DirectoryInfo(CurrentGame.MetaDirectory).Attributes &= ~FileAttributes.Hidden;
                        //CurrentGame.IsDeleted = true;
                        Directory.Move(CurrentGame.MetaDirectory, newPath);
                        CurrentGame = null;
                        App.GameListLoader.SaveCache();
                        App.Config.SaveConfig();
                        if (running) RefreshConsole.Execute(null);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                });
            }
        }

        public ICommand SyncCommand
        {
            get
            {
                return new DelegateCommand(async obj =>
                {
                    await StopScanTask();

                    var list = await App.GameListLoader.LoadConsoles(true);
                    ConsoleMachines = list;
                    SearchedConsoleMachines = list;
                    CurrentMachine = null;
                    CurrentGame = null;
                    //CurrentMachine = ConsoleMachines.FirstOrDefault(k => k.Path == CurrentMachine?.Path);
                    //CurrentGame = CurrentMachine?.Games.FirstOrDefault(k => k.Path == CurrentGame?.Path);
                    //if (CurrentMachine != null) StartScanTask();
                });
            }
        }

        public ICommand SelectConsoleCommand
        {
            get
            {
                return new DelegateCommand(async obj =>
                {
                    await StopScanTask();

                    var identity = (string)obj;
                    CurrentMachine = ConsoleMachines.First(k => k.Identity == identity);
                    CurrentGame = null;

                    StartScanTask();
                });
            }
        }

        public ICommand AddNewGameConsole
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    if (obj is object[] objects)
                    {
                        var mainWindow = (MainWindow)objects[0];
                        var machine = objects.Length > 1 ? objects[1] : null;
                        var window = new AddGameConsoleWindow((ConsoleMachine)machine)
                        {
                            Owner = mainWindow
                        };
                        window.ShowDialog();
                    }
                });
            }
        }

        public ICommand ClearConsoleSearchBoxCommand
        {
            get
            {
                return new DelegateCommand(obj => { ConsoleSearchString = ""; });
            }
        }

        public ICommand ClearGameSearchBoxCommand
        {
            get
            {
                return new DelegateCommand(obj => { GameSearchString = ""; });
            }
        }

        public ICommand ConfigCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    var mainWindow = (MainWindow)obj;
                    var oldPath = Path.GetFullPath(Config.GameDirectory.EndsWith("\\")
                        ? Config.GameDirectory
                        : Config.GameDirectory + "\\");
                    var window = new ConfigWindow()
                    {
                        Owner = mainWindow
                    };
                    window.ShowDialog();
                    var newPath = Path.GetFullPath(Config.GameDirectory.EndsWith("\\")
                        ? Config.GameDirectory
                        : Config.GameDirectory + "\\");
                    if (oldPath != newPath)
                    {
                        var result = MessageBox.Show($"你已改变游戏游戏目录：\r\n从 {oldPath}\r\n至 {newPath}\r\n是否自动迁移？", "", MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            //var di = new DirectoryInfo(oldPath);
                            //foreach (var dir in di.EnumerateDirectories())
                            //{
                            //    FileSystem.MoveDirectory(dir.FullName, Path.Combine(newPath, dir.Name),
                            //        UIOption.AllDialogs, UICancelOption.DoNothing);
                            //}
                            FileSystem.MoveDirectory(oldPath, newPath, UIOption.AllDialogs, UICancelOption.DoNothing);
                        }

                        SyncCommand.Execute(null);
                    }
                });
            }
        }
        public void SetConfig(Config config)
        {
            Config = config;
        }

        public void StartScanTask()
        {
            if (CurrentMachine.SearchedGames == null)
                CurrentMachine.SearchedGames = CurrentMachine.Games;
            if (CurrentMachine.SearchedGames.Count < 100)
            {
                CurrentMachine.VisibleGames = CurrentMachine.SearchedGames;
            }
            else
            {
                CurrentMachine.VisibleGames = new ObservableCollection<Game>();
                _syncTask = Task.Run(() =>
                {
                    var id = CurrentMachine.Identity;
                    foreach (var game in CurrentMachine.SearchedGames)
                    {
                        if (id != CurrentMachine.Identity || _cts.IsCancellationRequested)
                            return;
                        Execute.OnUiThread(() => { CurrentMachine.VisibleGames.Add(game); },
                            MainWindow.SynchronizationContext);
                        Thread.Sleep(10);
                    }
                });
            }
        }

        public async Task StopScanTask()
        {
            _cts?.Cancel();
            if (_syncTask != null)
                await Task.Run(() =>
                {
                    if (_syncTask != null) Task.WaitAll(_syncTask);
                });
            _cts = new CancellationTokenSource();
        }
    }
}
