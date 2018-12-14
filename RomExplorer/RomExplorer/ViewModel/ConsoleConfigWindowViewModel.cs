using Microsoft.Win32;
using Milkitic.WpfApi;
using Milkitic.WpfApi.Commands;
using RomExplorer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RomExplorer.ViewModel
{
    internal class ConsoleConfigWindowViewModel : ViewModelBase
    {
        private ConsoleMachine _consoleMachine;
        private GameConsoleConfig _gameConsoleConfig;
        private string _iconPath;
        private string _name;
        private string _hostApplication;
        private string _startupArguments;

        public ConsoleMachine ConsoleMachine
        {
            get => _consoleMachine;
            set
            {
                _consoleMachine = value;
                OnPropertyChanged();
            }
        }

        public GameConsoleConfig GameConsoleConfig
        {
            get => _gameConsoleConfig;
            set
            {
                _gameConsoleConfig = value;
                OnPropertyChanged();
            }
        }

        public string IconPath
        {
            get => _iconPath;
            set
            {
                _iconPath = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string HostApplication
        {
            get => _hostApplication;
            set
            {
                _hostApplication = value;
                OnPropertyChanged();
            }
        }

        public string StartupArguments
        {
            get => _startupArguments;
            set
            {
                _startupArguments = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    _consoleMachine.SetIcon(IconPath);
                    _consoleMachine.NameWithoutExtension = Name;
                    _consoleMachine.CommitChanges();
                    _gameConsoleConfig.Identity = _consoleMachine.Identity;
                    if (!string.IsNullOrEmpty(HostApplication) || !string.IsNullOrEmpty(StartupArguments))
                    {
                        _gameConsoleConfig.HostApplication = HostApplication;
                        _gameConsoleConfig.StartupArguments = StartupArguments;
                    }

                    _gameConsoleConfig.CommitChanges();
                });
            }
        }

        public ICommand BrowseIconCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    OpenFileDialog fbd = new OpenFileDialog
                    {
                        Title = @"请选择一个图片",
                        Filter = @"所有支持的图片类型|*.jpg;*.png;*.bmp;*.jpeg"
                    };
                    var result = fbd.ShowDialog();
                    if (result == true)
                    {
                        IconPath = fbd.FileName;
                    }
                });
            }
        }
    }
}
