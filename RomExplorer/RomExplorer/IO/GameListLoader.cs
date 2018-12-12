using Newtonsoft.Json;
using RomExplorer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RomExplorer.IO
{
    internal static class GameListLoader
    {
        public static string CachePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GameListCache.json");
        public static string GamePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Games");

        static GameListLoader()
        {
            if (!Directory.Exists(GamePath))
                Directory.CreateDirectory(GamePath);
        }

        public static List<ConsoleMachine> LoadCache()
        {
            List<ConsoleMachine> consoleMachines;
            if (File.Exists(CachePath))
            {
                try
                {
                    consoleMachines = JsonConvert.DeserializeObject<List<ConsoleMachine>>(File.ReadAllText(CachePath));
                }
                catch
                {
                    consoleMachines = Search();
                }
            }
            else
            {
                consoleMachines = Search();
            }

            return consoleMachines;
        }

        public static void SaveCache(this List<ConsoleMachine> consoleMachines)
        {
            File.WriteAllText(CachePath, JsonConvert.SerializeObject(consoleMachines));
        }

        public static List<ConsoleMachine> Search()
        {
            List<ConsoleMachine> consoleMachines = new List<ConsoleMachine>();
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

            SaveCache(consoleMachines);
            return consoleMachines;
        }

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
        }
    }
}
