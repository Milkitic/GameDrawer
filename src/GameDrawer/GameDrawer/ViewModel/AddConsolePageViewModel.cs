using System.Windows.Input;
using GameDrawer.IO;
using Milky.WpfApi;
using Milky.WpfApi.Commands;

namespace GameDrawer.ViewModel
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
