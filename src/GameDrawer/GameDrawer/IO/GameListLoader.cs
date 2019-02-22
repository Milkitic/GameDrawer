using GameDrawer.Model;
using GameDrawer.Windows;
using Milky.WpfApi;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GameDrawer.IO
{
    internal class GameListLoader
    {
        private ObservableCollection<ConsoleMachine> _consoleMachines;
        private FileSystemWatcher _fileSystemWatcher;
        public static string CachePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GameListCache.json");
        public static string GamePath => App.Config.GameDirectory;

        public static GameListProperties GameListProperties { get; } = new GameListProperties();
        public const int NotifyDelay = 300;

        public GameListLoader()
        {
            if (!Directory.Exists(GamePath))
                Directory.CreateDirectory(GamePath);
            _fileSystemWatcher = new FileSystemWatcher(GamePath)
            {
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName
            };

            _fileSystemWatcher.Created += FileSystemWatcher_Created;
            _fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            _fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        public async Task<ObservableCollection<ConsoleMachine>> GetConsoleMachines()
        {
            if (_consoleMachines != null)
                return _consoleMachines;

            ObservableCollection<ConsoleMachine> list;
            try
            {
                list = await LoadConsoles(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                list = null;
            }

            return list;
        }

        public void AddConsoleMachine(ConsoleMachine consoleMachine)
        {
            _consoleMachines.Add(consoleMachine);
        }

        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            var path = e.FullPath;
            var name = e.Name;
            var oldPath = e.OldFullPath;
            var oldName = e.OldName;
            FileAttributes attr = File.GetAttributes(path);

            if (attr.HasFlag(FileAttributes.Directory))
            {
                var di = new DirectoryInfo(path);
                if (di.Parent?.FullName != new DirectoryInfo(GamePath).FullName) return;
                if (_consoleMachines.Any(k => k.Path == path)) return;
                var console = _consoleMachines.FirstOrDefault(k => k.Path == oldPath);
                if (console == null) return;
                console.NameWithoutExtension = name;
                console.CommitChanges();
                Execute.OnUiThread(async () => { await console.Refresh(); },
                    MainWindow.SynchronizationContext);
            }
            else
            {
            }
        }

        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
        }

        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
        }

        public async Task<ObservableCollection<ConsoleMachine>> LoadConsoles(bool refresh)
        {
            ObservableCollection<ConsoleMachine> consoleMachines;
            if (refresh || !File.Exists(CachePath))
            {
                consoleMachines = await RefreshConsolesAsync().ConfigureAwait(false);
            }
            else
            {
                try
                {
                    consoleMachines =
                        JsonConvert.DeserializeObject<ObservableCollection<ConsoleMachine>>(
                            File.ReadAllText(CachePath));
                }
                catch
                {
                    consoleMachines = await RefreshConsolesAsync().ConfigureAwait(false);
                }
            }

            _consoleMachines = consoleMachines;
            SaveCache();
            return consoleMachines;
        }

        public void SaveCache()
        {
            File.WriteAllText(CachePath, JsonConvert.SerializeObject(_consoleMachines, Formatting.Indented));
        }

        private static async Task<ObservableCollection<ConsoleMachine>> RefreshConsolesAsync()
        {
            ObservableCollection<ConsoleMachine> consoleMachines
                = new ObservableCollection<ConsoleMachine>();
            if (!Directory.Exists(GamePath))
            {
                Directory.CreateDirectory(GamePath);
            }
            
            var gameDirectoryInfo = new DirectoryInfo(GamePath);

            await StopRunningScanAsync();
            GameListProperties.SyncTask = Task.Run(async () =>
            {
                foreach (var consoleDirectoryInfo in gameDirectoryInfo.EnumerateDirectories())
                {
                    if (GameListProperties.SyncCts.IsCancellationRequested) //break thread here
                    {
                        break;
                    }

                    string consoleDirectoryPath = consoleDirectoryInfo.FullName;
                    var console = new ConsoleMachine(consoleDirectoryPath);

                    var iconPath = Path.Combine(Config.IconCacheDirectory, "Folder.png");

                    if (!File.Exists(iconPath))
                    {
                        W32FileInfo.GetLargeIcon(console.Path).ToBitmap().Save(iconPath, ImageFormat.Png);
                    }

                    consoleMachines.Add(console);
                    await console.RefreshWithoutCache();
                }
            });
            bool notify = true;
            var notifyTask = new Task(() =>
            {
                Thread.Sleep(NotifyDelay);
                if (notify)
                    GameListProperties.NotifySyncChanged();
            });
            notifyTask.Start();
            await GameListProperties.SyncTask.ConfigureAwait(false);

            notify = false;
            GameListProperties.NotifySyncChanged();
            return consoleMachines;
        }

        private static async Task StopRunningScanAsync()
        {
            GameListProperties.SyncCts?.Cancel();
            if (GameListProperties.SyncTask != null)
                await Task.Run(() =>
                {
                    if (GameListProperties.SyncTask != null) Task.WaitAll(GameListProperties.SyncTask);
                });
            GameListProperties.SyncCts = new CancellationTokenSource();
        }
    }

    public static class GameListExtension
    {
        public static GameListExtensionProperties GameListExtensionProperties { get; } =
            new GameListExtensionProperties();

        public static async Task Refresh(this ConsoleMachine console)
        {
            await RefreshWithoutCache(console);

            App.GameListLoader.SaveCache();
        }

        public static async Task RefreshWithoutCache(this ConsoleMachine console)
        {
            await StopRunningRefreshAsync();
            GameListExtensionProperties.RefreshTask = Task.Run(() =>
            {
                if (!File.Exists(console.DescriptionPath))
                {
                    console.InitDescription();
                }

                if (!Directory.Exists(console.RomDirectoryPath))
                    Directory.CreateDirectory(console.RomDirectoryPath);
                else
                {
                    Execute.OnUiThread(() =>
                    {
                        console.Games.Clear();
                    }, MainWindow.SynchronizationContext);

                    var romDirectoryInfo = new DirectoryInfo(console.RomDirectoryPath);
                    var config = App.Config.GameConsoleConfigs.FirstOrDefault(k => k.Identity == console.Identity);
                    string[] filter = null;
                    bool filterUseWhiteList = false;
                    if (config != null)
                    {
                        filter =
                            config.ExtensionFilter?.Split('|').Select(k => "." + k.Trim().TrimStart('.')).ToArray() ??
                            new string[0];
                        filterUseWhiteList = config.UseWhiteList;
                    }

                    foreach (var romFileInfo in romDirectoryInfo.EnumerateFiles())
                    {
                        if (GameListExtensionProperties.RefreshCts.IsCancellationRequested) //break thread here
                        {
                            break;
                        }

                        if (config != null)
                        {
                            if (filterUseWhiteList)
                            {
                                if (!filter.Contains(romFileInfo.Extension))
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (filter.Contains(romFileInfo.Extension))
                                    continue;
                            }
                        }

                        var romPath = romFileInfo.FullName;
                        var game = new Game(romPath);

                        if (!Directory.Exists(game.MetaDirectory))
                        {
                            Directory.CreateDirectory(game.MetaDirectory);
                            var metaDir = new DirectoryInfo(game.MetaDirectory);
                            metaDir.Attributes = metaDir.Attributes | FileAttributes.Hidden;
                        }

                        if (!Directory.Exists(game.ScreenShotDirectory))
                            Directory.CreateDirectory(game.ScreenShotDirectory);

                        if (!File.Exists(game.DescriptionPath))
                        {
                            game.InitDescription();
                        }

                        string iconPath;
                        if (Game.SpecialExtensions.Contains(game.Extension))
                        {
                            var iconName = $"{game.Identity.Replace("/", "").Replace("\\", "").Replace(" ", "")}.png";
                            iconPath = Path.Combine(Config.IconCacheDirectory, iconName);
                        }
                        else
                            iconPath = Path.Combine(Config.IconCacheDirectory, game.Extension + ".png");


                        if (!File.Exists(iconPath))
                        {
                            Icon.ExtractAssociatedIcon(game.Path)?.ToBitmap().Save(iconPath, ImageFormat.Png);
                        }

                        game.Length = romFileInfo.Length;
                        Execute.OnUiThread(() =>
                        {
                            console.Games.Add(game);
                        }, MainWindow.SynchronizationContext);
                    }
                }

                if (!Directory.Exists(console.EmulatorDirectoryPath))
                    Directory.CreateDirectory(console.EmulatorDirectoryPath);
            });
            bool notify = true;
            var notifyTask = new Task(() =>
            {
                Thread.Sleep(GameListLoader.NotifyDelay);
                if (notify)
                    GameListExtensionProperties.NotifyRefreshChanged();
            });
            notifyTask.Start();
            await GameListExtensionProperties.RefreshTask.ConfigureAwait(false);

            notify = false;
            GameListExtensionProperties.NotifyRefreshChanged();
        }

        private static async Task StopRunningRefreshAsync()
        {
            GameListExtensionProperties.RefreshCts?.Cancel();
            if (GameListExtensionProperties.RefreshTask != null)
                await Task.Run(() =>
                {
                    if (GameListExtensionProperties.RefreshTask != null)
                        Task.WaitAll(GameListExtensionProperties.RefreshTask);
                });
            GameListExtensionProperties.RefreshCts = new CancellationTokenSource();
        }

        public static void Refresh(this Game game)
        {
            return;
            //if (!Directory.Exists(game.MetaDirectory))
            //    Directory.CreateDirectory(game.MetaDirectory);
            //if (!Directory.Exists(game.ScreenShotDirectory))
            //    Directory.CreateDirectory(game.ScreenShotDirectory);

            //if (!File.Exists(game.DescriptionPath))
            //{
            //    game.InitDescription();
            //}

            //game.Path = game.Path;
        }
    }
}
