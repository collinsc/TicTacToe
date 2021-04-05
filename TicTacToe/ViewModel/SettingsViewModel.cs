﻿using GalaSoft.MvvmLight;
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
    public class SettingsViewModel : ViewModelBase
    {
        private ICommand _goToMenu;



        public int ModeIndex
        {
            get => (int)SettingsFacade.GameMode.Value;
            set
            {
                SettingsFacade.SetGameMode((GameMode)value);
                RaisePropertyChanged(nameof(ModeIndex));
            }
        }

        public ICommand GoToMenuCommand
        {
            get
            {
                return _goToMenu ??= new RelayCommand(() =>
                {
                    EventMediator.Notify(nameof(GoToMenuCommand), "");
                });
            }
        }
    }
}
