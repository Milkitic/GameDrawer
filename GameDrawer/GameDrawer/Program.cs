using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Windows;

namespace GameDrawer
{
    class Program
    {
        public static string RunningAsAdministrator { get; set; } = "";

        [DllImport("user32.dll")]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        /// <summary>  
        /// 应用程序的主入口点。  
        /// </summary>  
        [STAThread]
        static void Main(string[] args)
        {
            CheckProcess();
            CheckRole();
            RunApplication();
        }

        private static void CheckRole()
        {
            if (IsAdministrator)
            {
                MessageBox.Show("请勿将此程序以管理员权限运行。", "Game Drawer", MessageBoxButton.OK, MessageBoxImage.Warning);
                RunningAsAdministrator = "（当前为管理员模式，不当操作可能会丢失用户文件或损害系统。）";
            }
        }

        private static void CheckProcess()
        {
            var currentProcess = Process.GetCurrentProcess();
            var path = currentProcess.MainModule.FileName;
            var processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (var process in processes)
            {
                try
                {
                    if (process.MainModule.FileName != path || process.Id == currentProcess.Id)
                        continue;
                    IntPtr handle = process.MainWindowHandle;
                    SwitchToThisWindow(handle, true);
                    Environment.Exit(0);
                }
                catch
                {
                    // ignored
                }
            }

        }

        private static bool IsAdministrator
        {
            get
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        private static void RunApplication()
        {
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}

