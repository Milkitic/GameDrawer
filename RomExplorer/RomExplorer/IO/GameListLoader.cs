using Milkitic.WpfApi;
using Newtonsoft.Json;
using RomExplorer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace RomExplorer.IO
{
    internal class GameListLoader
    {
        private ObservableCollection<ConsoleMachine> _consoleMachines;
        private FileSystemWatcher _fileSystemWatcher;
        public static string CachePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GameListCache.json");
        public static string GamePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Games");

        public ObservableCollection<ConsoleMachine> ConsoleMachines
        {
            get
            {
                if (_consoleMachines != null)
                    return _consoleMachines;

                ObservableCollection<ConsoleMachine> list;
                try
                {
                    list = LoadConsoles(false);
                }
                catch (Exception e)
                {
                    list = LoadConsoles(true);
                }

                return list;
            }
            set => _consoleMachines = value;
        }

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
                if (di.Parent?.FullName == new DirectoryInfo(GamePath).FullName)
                {
                    if (_consoleMachines.All(k => k.Path != path))
                    {
                        var console = _consoleMachines.FirstOrDefault(k => k.Path == oldPath);
                        if (console != null)
                        {
                            console.NameWithoutExtension = name;
                            console.CommitChanges();
                            //Execute.InitializeWithDispatcher();
                            Execute.OnUiThread(() => { console.Refresh(); }, MainWindow.SynchronizationContext);
                        }
                    }
                }
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

        public ObservableCollection<ConsoleMachine> LoadConsoles(bool refresh)
        {
            if (_consoleMachines != null)
            {
                //_consoleMachines.CollectionChanged -= ConsoleMachines_CollectionChanged;
            }

            ObservableCollection<ConsoleMachine> consoleMachines;
            if (refresh || !File.Exists(CachePath))
            {
                consoleMachines = RefreshConsoles();
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
                    consoleMachines = RefreshConsoles();
                }
            }

            _consoleMachines = consoleMachines;
            SaveCache();
            //consoleMachines.CollectionChanged += ConsoleMachines_CollectionChanged;
            return consoleMachines;
        }

        private void ConsoleMachines_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems)
                {
                    var consoleMachine = (ConsoleMachine)newItem;
                    consoleMachine.Committed += ConsoleMachine_Committed;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var newItem in e.OldItems)
                {
                    var consoleMachine = (ConsoleMachine)newItem;
                    consoleMachine.Committed -= ConsoleMachine_Committed;
                }
            }
        }

        private void ConsoleMachine_Committed(object sender, EventArgs e)
        {
            SaveCache();
        }

        public void SaveCache()
        {
            File.WriteAllText(CachePath, JsonConvert.SerializeObject(_consoleMachines));
        }

        private static ObservableCollection<ConsoleMachine> RefreshConsoles()
        {
            ObservableCollection<ConsoleMachine> consoleMachines
                = new ObservableCollection<ConsoleMachine>();
            var gameDirectoryInfo = new DirectoryInfo(GamePath);
            foreach (var consoleDirectoryInfo in gameDirectoryInfo.EnumerateDirectories())
            {
                //Thread.Sleep(500);
                string consoleDirectoryPath = consoleDirectoryInfo.FullName;
                var console = new ConsoleMachine(consoleDirectoryPath);

                var iconPath = Path.Combine(Config.IconCacheDirectory, "Folder.png");

                if (!File.Exists(iconPath))
                {
                    W32FileInfo.GetLargeIcon(console.Path).ToBitmap().Save(iconPath, ImageFormat.Png);
                }

                consoleMachines.Add(console);
                console.RefreshWithoutCache();
            }

            return consoleMachines;
        }
    }

    public static class GameListExtension
    {
        public static void Refresh(this ConsoleMachine console)
        {
            RefreshWithoutCache(console);

            App.GameListLoader.SaveCache();
        }

        public static void RefreshWithoutCache(this ConsoleMachine console)
        {
            if (!File.Exists(console.DescriptionPath))
            {
                console.InitDescription();
            }

            if (!Directory.Exists(console.RomDirectoryPath))
                Directory.CreateDirectory(console.RomDirectoryPath);
            else
            {
                console.Games.Clear();
                var romDirectoryInfo = new DirectoryInfo(console.RomDirectoryPath);
                var config = App.Config.GameConsoleConfigs.FirstOrDefault(k => k.Identity == console.Identity);
                string[] filter = null;
                bool filterUseWhiteList = false;
                if (config != null)
                {
                    filter = config.ExtensionFilter?.Split('|').Select(k => "." + k.Trim().TrimStart('.')).ToArray() ??
                             new string[0];
                    filterUseWhiteList = config.UseWhiteList;
                }

                foreach (var romFileInfo in romDirectoryInfo.EnumerateFiles())
                {
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
                    if (game.Extension == "exe" || game.Extension == "lnk")
                    {
                        iconPath = Path.Combine(Config.IconCacheDirectory,
                            $"{game.Identity.Replace("/", "").Replace("\\", "")}.png");
                    }
                    else
                        iconPath = Path.Combine(Config.IconCacheDirectory, game.Extension + ".png");

                    if (!File.Exists(iconPath))
                    {
                        Icon.ExtractAssociatedIcon(game.Path)?.ToBitmap().Save(iconPath, ImageFormat.Png);
                    }

                    game.Length = romFileInfo.Length;
                    console.Games.Add(game);
                }
            }

            if (!Directory.Exists(console.EmulatorDirectoryPath))
                Directory.CreateDirectory(console.EmulatorDirectoryPath);
        }

        public static void Refresh(this Game game)
        {
            return;
            if (!Directory.Exists(game.MetaDirectory))
                Directory.CreateDirectory(game.MetaDirectory);
            if (!Directory.Exists(game.ScreenShotDirectory))
                Directory.CreateDirectory(game.ScreenShotDirectory);

            if (!File.Exists(game.DescriptionPath))
            {
                game.InitDescription();
            }

            game.Path = game.Path;
        }
    }
}
