using GameDrawer.Model;
using GameDrawer.ViewModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Milkitic.WpfApi;

namespace GameDrawer.Windows
{
    /// <summary>
    /// ConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigWindow : Window
    {
        private Color _color;
        private bool _saved = false;

        public ConfigWindow()
        {
            InitializeComponent();
            ViewModel = (ConfigWindowViewModel)DataContext;
            ViewModel.SetConfig(App.Config);
            ViewModel.UpdaterViewModel = App.Updater.UpdaterViewModel;
        }

        private ConfigWindowViewModel ViewModel { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _color = App.Config.ThemeColor;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!_saved)
                App.Config.ThemeColor = _color;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string validate, oldValidate;
            try
            {
                validate = System.IO.Path.GetFullPath(ViewModel.GameDirectory.EndsWith("\\")
                    ? ViewModel.GameDirectory
                    : ViewModel.GameDirectory + "\\");
                oldValidate = System.IO.Path.GetFullPath(ViewModel.Config.GameDirectory.EndsWith("\\")
                    ? ViewModel.Config.GameDirectory
                    : ViewModel.Config.GameDirectory + "\\");
                if (validate != oldValidate)
                {
                    if (!Directory.Exists(validate))
                    {
                        Directory.CreateDirectory(validate);
                    }
                    else
                    {
                        var di = new DirectoryInfo(validate);
                        if (di.EnumerateDirectories().Any())
                        {
                            var result = MessageBox.Show("目录中已包含子目录，是否将其设置为游戏目录？", "", MessageBoxButton.YesNo,
                                MessageBoxImage.Question);
                            if (result != MessageBoxResult.Yes)
                            {
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (ViewModel.Config.AutoStartup != ViewModel.AutoStartup)
            {
                RegistryKey rKey =
                    Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (ViewModel.AutoStartup)
                {
                    rKey?.SetValue("Milkitic.GameDrawer", Process.GetCurrentProcess().MainModule.FileName);
                }
                else
                {
                    rKey?.DeleteValue("Milkitic.GameDrawer", false);
                }
            }

            ViewModel.Config.GameDirectory = validate;
            ViewModel.Config.AutoStartup = ViewModel.AutoStartup;
            ViewModel.Config.SaveConfig();

            _saved = true;
            DialogResult = true;
            Close();
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }
    }
}
