using System.IO;

namespace RomExplorer.Model
{
    public class Game
    {
        public Game(string romPath)
        {
            RomPath = romPath;
        }

        public string Name => Path.GetFileNameWithoutExtension(RomPath); //view property
        public string RomPath { get; set; } //view property
        public string AddonDirectory => RomPath + ".addons";
        public string Description { get; set; } //view property
        public string IconPath => Path.Combine(AddonDirectory, "icon.png"); //view property
        public string ScreenShotDirectory => Path.Combine(AddonDirectory, "screenshots");

        public static string DefaultDescription => "这个文件暂时还没有介绍，请帮忙一同编辑吧！";
    }
}
