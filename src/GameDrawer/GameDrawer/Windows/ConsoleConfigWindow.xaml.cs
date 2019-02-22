using System;
using System.Linq;
using System.Windows;
using GameDrawer.Model;
using GameDrawer.ViewModel;
using Milky.WpfApi;

namespace GameDrawer.Windows
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
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("此操作将删除此目录以及所有子目录与文件至回收站。", "确认信息", MessageBoxButton.YesNo,
                MessageBoxImage.Information);
            if (result != MessageBoxResult.Yes) return;
            Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(ViewModel.FileModelBase.Path,
                Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
                Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            if (ViewModel.FileModelBase is ConsoleMachine) ViewModel.FileModelBase = null;
            BtnOk_Click(sender, e);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(ViewModel?.UseWhiteList);
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }
    }
}
