using Milkitic.WpfApi;
using RomExplorer.IO;
using RomExplorer.Model;
using RomExplorer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        internal MainWindowViewModel ViewModel;
        private readonly OptionContainer _consoleOption = new OptionContainer();
        private readonly OptionContainer _gameOption = new OptionContainer();

        public static SynchronizationContext SynchronizationContext { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var list = App.GameListLoader.ConsoleMachines;

            ViewModel = (MainWindowViewModel)DataContext;
            ViewModel.ConsoleMachines = list;
            ViewModel.SearchedConsoleMachines = list;
            SynchronizationContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// toggle button，暂时用事件处理程序
        /// </summary>
        private async void BtnConsole_Click(object sender, RoutedEventArgs e)
        {
            _consoleOption.Add(sender);
            _consoleOption.Switch(sender);

            await ViewModel.StopScanTask();

            var identity = (string)((ToggleButton)sender).Tag;
            ViewModel.CurrentMachine = ViewModel.ConsoleMachines.FirstOrDefault(k => k.Identity == identity);

            ViewModel.StartScanTask();
            ViewModel.CurrentGame = null;
        }

        /// <summary>
        /// toggle button，暂时用事件处理程序
        /// </summary>
        private void BtnGame_Click(object sender, RoutedEventArgs e)
        {
            _gameOption.Add(sender);
            _gameOption.Switch(sender);
            var identity = (string)((ToggleButton)sender).Tag;
            ViewModel.CurrentGame = ViewModel.CurrentMachine.Games.First(k => k.Identity == identity);
        }

        /// <summary>
        /// toggle button，暂时用事件处理程序
        /// </summary>
        private void BtnGame_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewModel.RunCommand.Execute(this);
        }

        private void NamingBox_LostFocus(object sender, RoutedEventArgs e)
        {
            ApplyNamingChange();
        }


        private void NamingBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ApplyNamingChange();
            }
        }

        private void ApplyNamingChange()
        {
            ViewModel.CurrentGame.NameEditable = false;
            try
            {
                ViewModel.CurrentGame.CommitChanges();
                ViewModel.CurrentGame.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Title, MessageBoxButton.OK, MessageBoxImage.Error);
                ViewModel.CurrentGame.ResetSuspended();
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ViewModel.WindowActivated = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            ViewModel.WindowActivated = false;
        }

        private async void GameSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ViewModel.CurrentMachine == null) return;
            var text = GameSearchBox.Text;
            await ViewModel.StopScanTask();
            if (string.IsNullOrEmpty(text))
            {
                ViewModel.CurrentMachine.SearchedGames = ViewModel.CurrentMachine.Games;
            }
            else
            {
                string[] keywords = text.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var sb = ViewModel.CurrentMachine.Games.Select(k => k);
                ViewModel.CurrentMachine.SearchedGames = new ObservableCollection<Game>(keywords.Aggregate(sb,
                    (current, keyword) =>
                        current.Where(k => k.NameWithoutExtension.ToLower().Contains(keyword.ToLower()))));
            }
            ViewModel.StartScanTask();
        }

        private void ConsoleSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var text = ConsoleSearchBox.Text;
            if (string.IsNullOrEmpty(text))
            {
                ViewModel.SearchedConsoleMachines = ViewModel.ConsoleMachines;
            }
            else
            {
                string[] keywords = text.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var sb = ViewModel.ConsoleMachines.Select(k => k);
                ViewModel.SearchedConsoleMachines = new ObservableCollection<ConsoleMachine>(keywords.Aggregate(sb,
                    (current, keyword) =>
                        current.Where(k => k.NameWithoutExtension.ToLower().Contains(keyword.ToLower()))));
            }
        }
    }
}
