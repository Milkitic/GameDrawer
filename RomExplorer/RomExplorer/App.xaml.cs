using RomExplorer.IO;
using RomExplorer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
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
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; ;

            SetAlignment();
            Config = Config.LoadOrCreateConfig();
            GameListLoader = new GameListLoader();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (!e.IsTerminating) return;
            MessageBox.Show(
                string.Format("发生严重错误，即将退出。。。详情请查看error.log。{0}{1}", Environment.NewLine,
                    (e.ExceptionObject as Exception)?.Message), "Osu Player", MessageBoxButton.OK,
                MessageBoxImage.Error);
            File.AppendAllText("error.log",
                string.Format(@"===================={0}===================={1}{2}{3}{4}", DateTime.Now,
                    Environment.NewLine, e.ExceptionObject, Environment.NewLine, Environment.NewLine));
            Environment.Exit(1);
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
