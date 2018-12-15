using RomExplorer.Model;
using RomExplorer.ViewModel;
using System;
using System.Collections.Generic;
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

namespace RomExplorer.Windows
{
    /// <summary>
    /// ConsoleConfigWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConsoleConfigWindow : Window
    {
        private readonly FileModelBase _consoleMachine;
        internal ConsoleConfigWindowViewModel ViewModel { get; private set; }

        public ConsoleConfigWindow(FileModelBase consoleMachine)
        {
            _consoleMachine = consoleMachine;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = (ConsoleConfigWindowViewModel)DataContext;
            ViewModel.FileModelBase = _consoleMachine;
            ViewModel.GameConsoleConfig =
                App.Config.GameConsoleConfigs.FirstOrDefault(k => k.Identity == _consoleMachine.Identity)
                ?? new GameConsoleConfig(_consoleMachine.Identity);
            ViewModel.Name = _consoleMachine.NameWithoutExtension;
            ViewModel.IconPath = _consoleMachine.IconPath;
            ViewModel.HostApplication = ViewModel.GameConsoleConfig.HostApplication;
            ViewModel.StartupArguments = ViewModel.GameConsoleConfig.StartupArguments;
            ViewModel.UseWhiteList = ViewModel.GameConsoleConfig.UseWhiteList;
            ViewModel.ExtensionFilter = ViewModel.GameConsoleConfig.ExtensionFilter;
            ViewModel.Description = _consoleMachine.Description;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(ViewModel?.UseWhiteList);
        }
    }
}
