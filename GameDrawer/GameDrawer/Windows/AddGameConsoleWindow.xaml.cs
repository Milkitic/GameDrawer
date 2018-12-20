using System;
using System.Windows;
using GameDrawer.Model;
using GameDrawer.Pages;
using GameDrawer.ViewModel;

namespace GameDrawer.Windows
{
    /// <summary>
    /// AddGameConsoleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddGameConsoleWindow : Window
    {
        private readonly ConsoleMachine _console;

        public AddViewModel ViewModel { get; set; }

        public AddGameConsoleWindow(ConsoleMachine console)
        {
            _console = console;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = (AddViewModel)DataContext;
            ViewModel.ConsoleMachine = _console;
            if (_console == null)
            {
                MainFrame.Navigate(new AddConsolePage(this));
            }
            else
            {
                MainFrame.Navigate(new AddGamePage(this, _console));
            }

            Height = MinHeight;
        }

        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            this.HideMinimizeAndMaximizeButtons();
        }
    }
}
