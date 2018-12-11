using System;
using System.IO;

namespace RomExplorer.Model
{
    public class Game
    {
        public Game(string romPath)
        {
            RomPath = romPath;
        }

        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name => Path.GetFileNameWithoutExtension(RomPath); //view property
        public string RomPath { get; set; } //view property
        public string MetaDirectory => RomPath + ".meta";
        public string Description { get; set; } //view property
        public string DescriptionPath => Path.Combine(MetaDirectory, "description.txt");
        public string IconPath => Path.Combine(MetaDirectory, "icon.png"); //view property
        public string ScreenShotDirectory => Path.Combine(MetaDirectory, "screenshots");

        public void SetDescription(string desccription)
        {
            Description = desccription;
            File.WriteAllText(DescriptionPath, Description);
        }

        public static string DefaultDescription => "暂无游戏介绍";
    }
}
