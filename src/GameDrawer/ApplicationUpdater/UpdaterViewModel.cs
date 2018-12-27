using Milkitic.ApplicationUpdater.Github;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Milkitic.WpfApi;

namespace Milkitic.ApplicationUpdater
{
    public class UpdaterViewModel : ViewModelBase
    {
        private bool _isRunningChecking;
        private bool _hasNewVersion;
        private bool _hasNoNewVersion;
        private Release _newRelease;

        public bool IsRunningChecking
        {
            get => _isRunningChecking;
            set
            {
                _isRunningChecking = value;
                OnPropertyChanged();
            }
        }

        public Release NewRelease
        {
            get => _newRelease;
            set
            {
                _newRelease = value;
                HasNewVersion = value != null;
                HasNoNewVersion = value == null;
                OnPropertyChanged();
            }
        }

        public bool HasNewVersion
        {
            get => _hasNewVersion;
            private set
            {
                _hasNewVersion = value;
                OnPropertyChanged();
            }
        }
        public bool HasNoNewVersion
        {
            get => _hasNoNewVersion;
            private set
            {
                _hasNoNewVersion = value;
                OnPropertyChanged();
            }
        }

        public string CurrentVersion => UpdaterConfig.CurrentVersion;
    }
}
