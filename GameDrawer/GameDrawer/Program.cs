using System;

namespace GameDrawer
{
    class Program
    {
        /// <summary>  
        /// 应用程序的主入口点。  
        /// </summary>  
        [STAThread]
        static void Main(string[] args)
        {
            RunApplication();
        }

        private static void RunApplication()
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}

