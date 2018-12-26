using GameDrawer.Model;
using Microsoft.Win32;
using Milkitic.ApplicationUpdater;
using Milkitic.WpfApi;
using Milkitic.WpfApi.Commands;
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
        private bool _hasNewVersion;

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

        public ICommand SaveCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {

                });
            }
        }

        public ICommand CheckUpdateCommand
        {
            get
            {
                return new DelegateCommand(async obj =>
                {
                    bool? hasUpdate = await App.Updater.CheckUpdateAsync();
                    Config.LastUpdateCheck = DateTime.Now;
                    Config.SaveConfig();
                    if (hasUpdate == true)
                    {
                        NewVersionWindow newVersionWindow = new NewVersionWindow(UpdaterViewModel.NewRelease, () =>
                        {

                        });

                        newVersionWindow.ShowDialog();
                    }
                });
            }
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

        public ICommand OpenUpdateWindowCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {

                });
            }
        }
    }
}
