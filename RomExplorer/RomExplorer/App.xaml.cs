using RomExplorer.IO;
using RomExplorer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RomExplorer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static Config Config { get; set; }
        internal static GameListLoader GameListLoader { get; set; }

        static App()
        {
            Config = new Config();

            GameListLoader = new GameListLoader();
        }
    }
}
