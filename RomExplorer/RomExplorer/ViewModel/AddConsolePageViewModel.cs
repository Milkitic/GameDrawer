using Milkitic.WpfApi;
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
    public class AddConsolePageViewModel : ViewModelBase
    {
        private string _name;
        private string _iconPath;

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
    }
}
