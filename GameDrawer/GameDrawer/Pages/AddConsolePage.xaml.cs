using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using GameDrawer.IO;
using GameDrawer.Model;
using GameDrawer.ViewModel;
using GameDrawer.Windows;
using Path = System.IO.Path;

namespace GameDrawer.Pages
{
    /// <summary>
    /// AddConsolePage.xaml 的交互逻辑
    /// </summary>
    public partial class AddConsolePage : Page
    {
        private readonly AddGameConsoleWindow _baseWindow;
        public AddConsolePageViewModel ViewModel { get; private set; }

        public AddConsolePage(AddGameConsoleWindow baseWindow)
        {
            _baseWindow = baseWindow;
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = (AddConsolePageViewModel)DataContext;
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            string trueName;
            try
            {
                trueName = FileExtension.ValidateFileName(ViewModel.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var path = Path.Combine(App.Config.GameDirectory, trueName);
            Directory.CreateDirectory(path);
            var console = new ConsoleMachine(path);
            App.GameListLoader.ConsoleMachines.Add(console);
            console.Refresh();
            _baseWindow.Close();
        }
    }
}
