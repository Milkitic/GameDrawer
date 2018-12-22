using GameDrawer.Model;
using Milkitic.WpfApi;
using Milkitic.WpfApi.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameDrawer.ViewModel
{
    class ConfigWindowViewModel : ViewModelBase
    {
        public void SetConfig(Config config)
        {
            Config = config;
            GameDirectory = Config.GameDirectory;
            AutoStartup = Config.AutoStartup;
        }

        public Config Config { get; private set; }

        public string GameDirectory { get; set; }
        public bool AutoStartup { get; set; }

        public ICommand SaveCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    Config.GameDirectory = GameDirectory;
                    Config.AutoStartup = AutoStartup;
                });
            }
        }
    }
}
