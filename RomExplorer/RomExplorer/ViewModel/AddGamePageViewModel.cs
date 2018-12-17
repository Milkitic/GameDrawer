﻿using Milkitic.WpfApi;
using Milkitic.WpfApi.Commands;
using RomExplorer.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RomExplorer.ViewModel
{
    public class AddGamePageViewModel : ViewModelBase
    {
        private string _path;
        private string _iconPath;
        private bool _useShortcut;
        private string _name;

        public string Path
        {
            get => _path;
            set
            {
                _path = value;
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

        public string IconPath
        {
            get => _iconPath;
            set
            {
                _iconPath = value;
                OnPropertyChanged();
            }
        }

        public bool UseShortcut
        {
            get => _useShortcut;
            set
            {
                _useShortcut = value;
                OnPropertyChanged();
            }
        }

        public ICommand SelectIconPath
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    IconPath = FileExtension.BrowseIcon();
                });
            }
        }

        public ICommand SelectPath
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    Path = FileExtension.Browse();
                });
            }
        }
    }
}
