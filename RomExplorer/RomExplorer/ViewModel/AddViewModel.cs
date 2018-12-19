using Milkitic.WpfApi;
using RomExplorer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomExplorer.ViewModel
{
    public class AddViewModel : ViewModelBase
    {
        private ConsoleMachine _consoleMachine;

        public ConsoleMachine ConsoleMachine
        {
            get => _consoleMachine;
            set
            {
                _consoleMachine = value;
                OnPropertyChanged();
            }
        }
    }
}
