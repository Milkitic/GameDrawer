using System;
using System.IO;

namespace RomExplorer.Model
{
    public class Config
    {
        public string GamePath { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Games");

    }
}
