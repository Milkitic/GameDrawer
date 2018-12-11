using RomExplorer.IO;
using RomExplorer.Model;
using RomExplorer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RomExplorer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel;
        private readonly OptionContainer _consoleOption = new OptionContainer();
        private readonly OptionContainer _gameOption = new OptionContainer();
        private GameListLoader _loader;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _loader = new GameListLoader();
            var list = _loader.LoadCache();

            ViewModel = (MainWindowViewModel)DataContext;
            ViewModel.ConsoleMachines = new ObservableCollection<ConsoleMachine>(list);
        }

        private void BtnConsole_Click(object sender, RoutedEventArgs e)
        {
            _consoleOption.Add(sender);
            _consoleOption.Switch(sender);
            var id = (Guid)((ToggleButton)sender).Tag;
            ViewModel.CurrentMachine = ViewModel.ConsoleMachines.First(k => k.Guid == id);
        }

        private void BtnGame_Click(object sender, RoutedEventArgs e)
        {
            _gameOption.Add(sender);
            _gameOption.Switch(sender);
            var id = (Guid)((ToggleButton)sender).Tag;
            ViewModel.CurrentGame = ViewModel.CurrentMachine.Games.First(k => k.Guid == id);
        }

        private void BtnSync_Click(object sender, RoutedEventArgs e)
        {
            var list = _loader.Search();
            ViewModel.ConsoleMachines = new ObservableCollection<ConsoleMachine>(list);
            ViewModel.CurrentMachine =
                ViewModel.ConsoleMachines.FirstOrDefault(k => k.DirectoryPath == ViewModel.CurrentMachine?.DirectoryPath);
            ViewModel.CurrentGame =
                ViewModel.CurrentMachine?.Games.FirstOrDefault(k => k.RomPath == ViewModel.CurrentGame?.RomPath);

        }
    }
}
