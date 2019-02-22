using System;
using System.Windows;

namespace Milky.ApplicationUpdater
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
