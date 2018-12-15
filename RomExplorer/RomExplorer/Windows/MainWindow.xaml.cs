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
        private CancellationTokenSource _cts;
        private Task _syncTask;

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
            SynchronizationContext = SynchronizationContext.Current;

        }

        private async void BtnConsole_Click(object sender, RoutedEventArgs e)
        {
            await StopScanTask();

            _consoleOption.Add(sender);
            _consoleOption.Switch(sender);
            var identity = (string)((ToggleButton)sender).Tag;
            ViewModel.CurrentMachine = ViewModel.ConsoleMachines.First(k => k.Identity == identity);

            if (ViewModel.CurrentMachine.Games.Count < 100)
            {
                ViewModel.CurrentMachine.VisibleGames = ViewModel.CurrentMachine.Games;
            }
            else
            {
                ViewModel.CurrentMachine.VisibleGames = new ObservableCollection<Game>();
                StartScanTask();
            }
            ViewModel.CurrentGame = null;
        }

        private void StartScanTask()
        {
            _syncTask = Task.Run(() =>
            {
                var id = ViewModel.CurrentMachine.Identity;
                foreach (var game in ViewModel.CurrentMachine.Games)
                {
                    if (id != ViewModel.CurrentMachine.Identity || _cts.IsCancellationRequested)
                        return;
                    Execute.OnUiThread(() => { ViewModel.CurrentMachine.VisibleGames.Add(game); },
                        SynchronizationContext);
                    Thread.Sleep(10);
                }
            });
        }

        private async Task StopScanTask()
        {
            _cts?.Cancel();
            await Task.Run(() =>
            {
                if (_syncTask != null) Task.WaitAll(_syncTask);
            });
            _cts = new CancellationTokenSource();
        }

        private void BtnGame_Click(object sender, RoutedEventArgs e)
        {
            _gameOption.Add(sender);
            _gameOption.Switch(sender);
            var identity = (string)((ToggleButton)sender).Tag;
            ViewModel.CurrentGame = ViewModel.CurrentMachine.Games.First(k => k.Identity == identity);
        }

        private void BtnSync_Click(object sender, RoutedEventArgs e)
        {
            var list = App.GameListLoader.LoadConsoles(true);
            ViewModel.ConsoleMachines = list;
            ViewModel.CurrentMachine =
                ViewModel.ConsoleMachines.FirstOrDefault(k => k.Path == ViewModel.CurrentMachine?.Path);
            ViewModel.CurrentGame =
                ViewModel.CurrentMachine?.Games.FirstOrDefault(k => k.Path == ViewModel.CurrentGame?.Path);
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

        private void BtnGame_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewModel.RunCommand.Execute(null);
        }
    }
}
