using System;
using System.Collections.Generic;
using System.IO;

namespace RomExplorer.Model
{
    public class ConsoleMachine
    {
        public ConsoleMachine(string directoryPath)
        {
            DirectoryPath = directoryPath;
        }

        public Guid Guid { get; set; } = Guid.NewGuid();
        public string Name => new DirectoryInfo(DirectoryPath).Name;
        public string DirectoryPath { get; set; }
        public string IconPath => Path.Combine(DirectoryPath, "icon.png"); //view property
        public string Description { get; set; } = "暂无主机介绍";

        public string DescriptionPath => Path.Combine(DirectoryPath, "description.txt");
        public string RomDirectoryPath => Path.Combine(DirectoryPath, "Rom");
        public string EmulatorDirectoryPath => Path.Combine(DirectoryPath, "Emu");

        public List<Game> Games { get; set; } = new List<Game>();

        public void SetDescription(string desccription)
        {
            Description = desccription;
            File.WriteAllText(DescriptionPath, Description);
        }

        public static string DefaultDescription => "这个文件暂时还没有介绍，请帮忙一同编辑吧！";
    }
}
