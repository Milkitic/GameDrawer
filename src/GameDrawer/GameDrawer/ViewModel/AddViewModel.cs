﻿using GameDrawer.Model;
using Milky.WpfApi;

namespace GameDrawer.ViewModel
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
