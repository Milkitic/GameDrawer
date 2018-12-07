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
        public string IconPath { get; set; }
        public string Description { get; set; } = "这个文件暂时还没有介绍，请帮忙一同编辑吧！";

        public List<Game> Games { get; set; } = new List<Game>();
    }
}
