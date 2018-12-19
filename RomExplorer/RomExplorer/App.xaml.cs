using RomExplorer.IO;
using RomExplorer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
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
            SetAlignment();
            Config = Config.LoadOrCreateConfig();
            GameListLoader = new GameListLoader();
        }

        private static void SetAlignment()
        {
            var ifLeft = SystemParameters.MenuDropAlignment;

            if (!ifLeft) return;
            var t = typeof(SystemParameters);
            var field = t.GetField("_menuDropAlignment", BindingFlags.NonPublic | BindingFlags.Static);
            field?.SetValue(null, false);
        }

        private void Application_Activated(object sender, EventArgs e)
        {

        }

        private void Application_Deactivated(object sender, EventArgs e)
        {

        }
    }
}
