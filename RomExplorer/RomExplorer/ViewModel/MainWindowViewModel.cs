using Milkitic.WpfApi;
using Milkitic.WpfApi.Commands;
using RomExplorer.IO;
using RomExplorer.Model;
using RomExplorer.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RomExplorer.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ConsoleMachine> _consoleMachines;
        private ConsoleMachine _currentMachine;
        private Game _currentGame;

        private CancellationTokenSource _cts;
        private Task _syncTask;
        private bool _windowActivated;

        public ObservableCollection<ConsoleMachine> ConsoleMachines
        {
            get => _consoleMachines;
            set
            {
                _consoleMachines = value;
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

        public ICommand ChangeEditabilityCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    if (CurrentMachine == null) return;
                    if (CurrentMachine.DescriptionEditable)
                    {
                        CurrentGame?.CommitChanges();
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
                return new DelegateCommand(obj =>
                {
                    if (CurrentMachine == null) return;
                    var window = new ConsoleConfigWindow(CurrentMachine);

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

                    if (window.DialogResult == true)
                    {
                        if (CurrentMachine != null)
                        {
                            if (!Directory.Exists(CurrentMachine.Path))
                            {
                                ConsoleMachines.Remove(CurrentMachine);
                                CurrentMachine = null;
                            }
                            else
                            {
                                CurrentMachine.Refresh();
                            }
                        }
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
                    CurrentMachine.Refresh();
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
                            if (!string.IsNullOrEmpty(consoleConfig?.HostApplication)) host = consoleConfig?.HostApplication;
                            if (!string.IsNullOrEmpty(consoleConfig?.StartupArguments)) args = consoleConfig?.StartupArguments;
                            if (!string.IsNullOrEmpty(gameConfig?.HostApplication)) host = gameConfig?.HostApplication;
                            if (!string.IsNullOrEmpty(gameConfig?.StartupArguments)) args = gameConfig?.StartupArguments;

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
                                proc = new Process
                                {
                                    StartInfo = new ProcessStartInfo
                                    {
                                        FileName = host ?? CurrentGame.Path,
                                        Arguments = $"{(useQuote ? "\"" : "")}{CurrentGame.Path}{(useQuote ? "\"" : "")} {args}"
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
                        MessageBox.Show(e.Message, "错误信息", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                });
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    try
                    {
                        var newName = new DirectoryInfo(CurrentGame.MetaDirectory).Name + " - " +
                                      DateTime.Now.ToString("yyyyMMddHHmmss");
                        var newPath = Path.Combine(Config.BackupDirectory, newName);
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(CurrentGame.Path,
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                            Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                        Directory.Move(CurrentGame.MetaDirectory, newPath);
                        _currentMachine.Games.Remove(CurrentGame);
                        CurrentGame = null;
                        App.GameListLoader.SaveCache();
                        App.Config.SaveConfig();
                        //RefreshConsole.Execute(null);
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

                    var list = App.GameListLoader.LoadConsoles(true);
                    ConsoleMachines = list;
                    CurrentMachine = ConsoleMachines.FirstOrDefault(k => k.Path == CurrentMachine?.Path);
                    CurrentGame = CurrentMachine?.Games.FirstOrDefault(k => k.Path == CurrentGame?.Path);
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
                    var window = new AddGameConsoleWindow((ConsoleMachine)obj);
                    window.ShowDialog();
                });
            }
        }

        public void StartScanTask()
        {
            if (CurrentMachine.Games.Count < 100)
            {
                CurrentMachine.VisibleGames = CurrentMachine.Games;
            }
            else
            {
                CurrentMachine.VisibleGames = new ObservableCollection<Game>();
                _syncTask = Task.Run(() =>
                {
                    var id = CurrentMachine.Identity;
                    foreach (var game in CurrentMachine.Games)
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
