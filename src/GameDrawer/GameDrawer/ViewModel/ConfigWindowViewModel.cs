using GameDrawer.Model;
using GameDrawer.Windows;
using Milky.ApplicationUpdater;
using Milky.WpfApi;
using Milky.WpfApi.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameDrawer.ViewModel
{
    class ConfigWindowViewModel : ViewModelBase
    {
        private Config _config;
        private string _gameDirectory;
        private bool _autoStartup;
        private UpdaterViewModel _updaterViewModel;

        public void SetConfig(Config config)
        {
            Config = config;
            GameDirectory = Config.GameDirectory;
            AutoStartup = Config.AutoStartup;
        }

        public Config Config
        {
            get => _config;
            private set
            {
                _config = value;
                OnPropertyChanged();
            }
        }

        public string GameDirectory
        {
            get => _gameDirectory;
            set
            {
                _gameDirectory = value;
                OnPropertyChanged();
            }
        }

        public bool AutoStartup
        {
            get => _autoStartup;
            set
            {
                _autoStartup = value;
                OnPropertyChanged();
            }
        }

        public UpdaterViewModel UpdaterViewModel
        {
            get => _updaterViewModel;
            set
            {
                _updaterViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand CheckUpdateCommand
        {
            get
            {
                return new DelegateCommand(async obj =>
                {
                    await InnerCheckUpdateAsync();
                    if (Config != null)
                    {
                        Config.LastUpdateCheck = DateTime.Now;
                        Config.SaveConfig();
                    }
                });
            }
        }

        private async Task InnerCheckUpdateAsync()
        {
            bool? hasUpdate = await App.Updater.CheckUpdateAsync();

            if (hasUpdate == true && App.Updater.UpdaterViewModel.NewRelease.NewVerString != App.Config.IgnoredVersion)
            {
                OpenNewVersionWindow();
            }
        }

        private static void OpenNewVersionWindow()
        {
            NewVersionWindow newVersionWindow = new NewVersionWindow(
                App.Updater.UpdaterViewModel.NewRelease,
                updateCallback: () =>
                {
                    UpdateWindow updateWindow = new UpdateWindow(App.Updater.UpdaterViewModel.NewRelease);
                    updateWindow.Show();
                    MainWindow.CurrentInstance.Close();
                },
                laterCallback: () => { },
                skipCallback: () =>
                {
                    App.Config.IgnoredVersion = App.Updater.UpdaterViewModel.NewRelease.NewVerString;
                    App.Config.SaveConfig();
                })
            {
                Owner = MainWindow.CurrentInstance
            };
            newVersionWindow.ShowDialog();
        }

        public static async Task CheckUpdateAsync()
        {
            await new ConfigWindowViewModel().InnerCheckUpdateAsync();
        }

        public ICommand LinkGithubCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    Process.Start("https://github.com/Milkitic/GameDrawer");
                });
            }
        }

        public ICommand LinkIssueCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    //Process.Start("https://github.com/Milkitic/GameDrawer/issues/new");
                });
            }
        }

        public ICommand OpenNewVersionWindowCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    OpenNewVersionWindow();
                });
            }
        }
    }
}
