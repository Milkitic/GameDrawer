using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomExplorer
{
    public class Config
    {
        public string GamePath { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Games");

    }
}
