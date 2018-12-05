using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void LoadCache()
        {
          
        }

        public void SaveCache()
        {
         
        }

        public List<ConsoleMachine> Search()
        {
            List<ConsoleMachine> consoleMachines = new List<ConsoleMachine>();
            var gameDirectoryInfo = new DirectoryInfo(GamePath);
            foreach (var consoleDirectoryInfo in gameDirectoryInfo.EnumerateDirectories())
            {
                string consoleDirectoryPath = consoleDirectoryInfo.FullName;
                var console = new ConsoleMachine(consoleDirectoryPath);
                consoleMachines.Add(console);

                string romDirectoryPath = Path.Combine(consoleDirectoryPath, "Rom");
                string emulatorDirectoryPath = Path.Combine(consoleDirectoryPath, "Emulator");

                if (!Directory.Exists(romDirectoryPath))
                    Directory.CreateDirectory(romDirectoryPath);
                else
                {
                    var romDirectoryInfo = new DirectoryInfo(romDirectoryPath);
                    foreach (var romFileInfo in romDirectoryInfo.EnumerateFiles())
                    {
                        var romPath = romFileInfo.FullName;
                        var game = new Game(romPath);

                        var descriptionPath = Path.Combine(game.AddonDirectory, "description.txt");
                         if (!Directory.Exists(game.AddonDirectory))
                            Directory.CreateDirectory(game.AddonDirectory);
                        if (!Directory.Exists(game.ScreenShotDirectory))
                            Directory.CreateDirectory(game.ScreenShotDirectory);
                       
                        if (File.Exists(descriptionPath))
                        {
                            game.Description = File.ReadAllText(descriptionPath);
                        }
                        else
                        {
                            game.Description = Game.DefaultDescription;
                            File.WriteAllText(descriptionPath, Game.DefaultDescription);
                        }
                        
                        console.Games.Add(game);
                    }
                }

                if (!Directory.Exists(emulatorDirectoryPath))
                    Directory.CreateDirectory(emulatorDirectoryPath);
            }

            ConsoleMachines = consoleMachines;
            SaveCache();
            return consoleMachines;
        }
    }
}
