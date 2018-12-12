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
        private readonly ConsoleMachine _consoleMachine;
        internal ConsoleConfigWindowViewModel ViewModel { get; private set; }

        public ConsoleConfigWindow(ConsoleMachine consoleMachine)
        {
            _consoleMachine = consoleMachine;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = (ConsoleConfigWindowViewModel)DataContext;
            ViewModel.ConsoleMachine = _consoleMachine;
            ViewModel.GameConsoleConfig =
                App.Config.GameConsoleConfigs.FirstOrDefault(k => k.Identity == _consoleMachine.Identity)
                ?? new GameConsoleConfig(_consoleMachine.Identity);
            ViewModel.Name = _consoleMachine.NameWithoutExtension;
            ViewModel.IconPath = _consoleMachine.IconPath;
            ViewModel.HostApplication = ViewModel.GameConsoleConfig.HostApplication;
            ViewModel.StartupArguments = ViewModel.GameConsoleConfig.StartupArguments;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
