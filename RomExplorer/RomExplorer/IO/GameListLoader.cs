using Milkitic.WpfApi;
using Newtonsoft.Json;
using RomExplorer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

                if (!File.Exists(console.DescriptionPath))
                {
                    console.InitDescription();
                }

                consoleMachines.Add(console);

                if (!Directory.Exists(console.RomDirectoryPath))
                    Directory.CreateDirectory(console.RomDirectoryPath);
                else
                {
                    var romDirectoryInfo = new DirectoryInfo(console.RomDirectoryPath);
                    foreach (var romFileInfo in romDirectoryInfo.EnumerateFiles())
                    {
                        //Thread.Sleep(50);
                        var romPath = romFileInfo.FullName;
                        var game = new Game(romPath);

                        if (!Directory.Exists(game.MetaDirectory))
                            Directory.CreateDirectory(game.MetaDirectory);
                        if (!Directory.Exists(game.ScreenShotDirectory))
                            Directory.CreateDirectory(game.ScreenShotDirectory);

                        if (!File.Exists(game.DescriptionPath))
                        {
                            game.InitDescription();
                        }

                        console.Games.Add(game);
                    }
                }

                if (!Directory.Exists(console.EmulatorDirectoryPath))
                    Directory.CreateDirectory(console.EmulatorDirectoryPath);
            }

            return consoleMachines;
        }
    }

    public static class GameListExtension
    {
        public static void Refresh(this ConsoleMachine console)
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
                foreach (var romFileInfo in romDirectoryInfo.EnumerateFiles())
                {
                    //Thread.Sleep(50);
                    var romPath = romFileInfo.FullName;
                    var game = new Game(romPath);

                    if (!Directory.Exists(game.MetaDirectory))
                        Directory.CreateDirectory(game.MetaDirectory);
                    if (!Directory.Exists(game.ScreenShotDirectory))
                        Directory.CreateDirectory(game.ScreenShotDirectory);

                    if (!File.Exists(game.DescriptionPath))
                    {
                        game.InitDescription();
                    }

                    console.Games.Add(game);
                }
            }

            if (!Directory.Exists(console.EmulatorDirectoryPath))
                Directory.CreateDirectory(console.EmulatorDirectoryPath);

            App.GameListLoader.SaveCache();
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
