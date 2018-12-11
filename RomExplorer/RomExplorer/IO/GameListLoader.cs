using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RomExplorer.Model;

namespace RomExplorer.IO
{
    public class GameListLoader
    {
        public List<ConsoleMachine> ConsoleMachines { get; private set; }
        public string CachePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GameListCache.json");
        public string GamePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Games");

        public GameListLoader()
        {
            if (!Directory.Exists(GamePath))
                Directory.CreateDirectory(GamePath);
        }

        public List<ConsoleMachine> LoadCache()
        {
            if (File.Exists(CachePath))
            {
                try
                {
                    ConsoleMachines = JsonConvert.DeserializeObject<List<ConsoleMachine>>(File.ReadAllText(CachePath));
                }
                catch
                {
                    Search();
                }
            }
            else
            {
                Search();
            }

            return ConsoleMachines;
        }

        public void SaveCache()
        {
            File.WriteAllText(CachePath, JsonConvert.SerializeObject(ConsoleMachines));
        }

        public List<ConsoleMachine> Search()
        {
            List<ConsoleMachine> consoleMachines = new List<ConsoleMachine>();
            var gameDirectoryInfo = new DirectoryInfo(GamePath);
            foreach (var consoleDirectoryInfo in gameDirectoryInfo.EnumerateDirectories())
            {
                Thread.Sleep(1000);
                string consoleDirectoryPath = consoleDirectoryInfo.FullName;
                var console = new ConsoleMachine(consoleDirectoryPath);
                
                if (File.Exists(console.DescriptionPath))
                {
                    console.Description = File.ReadAllText(console.DescriptionPath);
                }
                else
                {
                    console.SetDescription(ConsoleMachine.DefaultDescription);
                }

                consoleMachines.Add(console);

                if (!Directory.Exists(console.RomDirectoryPath))
                    Directory.CreateDirectory(console.RomDirectoryPath);
                else
                {
                    var romDirectoryInfo = new DirectoryInfo(console.RomDirectoryPath);
                    foreach (var romFileInfo in romDirectoryInfo.EnumerateFiles())
                    {
                        Thread.Sleep(100);
                        var romPath = romFileInfo.FullName;
                        var game = new Game(romPath);
                        
                        if (!Directory.Exists(game.MetaDirectory))
                            Directory.CreateDirectory(game.MetaDirectory);
                        if (!Directory.Exists(game.ScreenShotDirectory))
                            Directory.CreateDirectory(game.ScreenShotDirectory);
                       
                        if (File.Exists(game.DescriptionPath))
                        {
                            game.Description = File.ReadAllText(game.DescriptionPath);
                        }
                        else
                        {
                            game.SetDescription(Game.DefaultDescription);
                        }
                        
                        console.Games.Add(game);
                    }
                }

                if (!Directory.Exists(console.EmulatorDirectoryPath))
                    Directory.CreateDirectory(console.EmulatorDirectoryPath);
            }

            ConsoleMachines = consoleMachines;
            SaveCache();
            return consoleMachines;
        }
    }
}
