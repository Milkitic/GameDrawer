using Milkitic.WpfApi;
using Milkitic.WpfApi.Commands;
using RomExplorer.IO;
using RomExplorer.Model;
using RomExplorer.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RomExplorer.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<ConsoleMachine> _consoleMachines;
        private ConsoleMachine _currentMachine;
        private Game _currentGame;
        
        public ObservableCollection<ConsoleMachine> ConsoleMachines
        {
            get => _consoleMachines;
            set
            {
                _consoleMachines = value;
                OnPropertyChanged();
            }
        }

        public ConsoleMachine CurrentMachine
        {
            get => _currentMachine;
            set
            {
                _currentMachine = value;
                OnPropertyChanged();
            }
        }

        public Game CurrentGame
        {
            get => _currentGame;
            set
            {
                _currentGame = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeEditabilityCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    if (CurrentMachine == null) return;
                    if (CurrentMachine.DescriptionEditable)
                    {
                        CurrentGame?.CommitChanges();
                        CurrentMachine.DescriptionEditable = false;
                    }
                    else
                    {
                        CurrentMachine.DescriptionEditable = true;
                    }
                });
            }
        }

        public ICommand ConsoleConfigCommand
        {
            get
            {
                return new DelegateCommand(obj =>
                {
                    if (CurrentMachine == null) return;
                    var window = new ConsoleConfigWindow(CurrentMachine);

                    Execute.OnUiThread(() =>
                    {
                        try
                        {
                            window.ShowDialog();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            window.Close();
                        }
                    });

                    if (window.DialogResult == true)
                    {
                        CurrentMachine.Refresh();
                    }
                });
            }
        }
    }
}
