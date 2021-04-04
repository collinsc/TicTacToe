using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TicTacToe.Model;

namespace TicTacToe.ViewModel
{
    public class MainMenuViewModel : ViewModelBase
    {

        private ICommand _goToGame;
        private ICommand _goToExit;
        private ICommand _goToSettings;

        public ICommand GoToGameCommand
        {
            get
            {
                return _goToGame ??= new RelayCommand(()=>
                {
                    EventMediator.Notify(nameof(GoToGameCommand), "");
                });
            }
        }

        public ICommand GoToExitCommand
        {
            get
            {
                return _goToExit ??= new RelayCommand(() =>
                {
                    EventMediator.Notify(nameof(GoToExitCommand), "");
                });
            }
        }

        public ICommand GoToSettingsCommand
        {
            get
            {
                return _goToSettings ??= new RelayCommand(() =>
                {
                    EventMediator.Notify(nameof(GoToSettingsCommand), "");
                });
            }
        }

    }
}
