using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TicTacToe.Model;

namespace TicTacToe.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableObject _currentPageViewModel;
        private List<ObservableObject> _pageViewModels;

        public List<ObservableObject> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<ObservableObject>();

                return _pageViewModels;
            }
        }

        public ObservableObject CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                _currentPageViewModel = value;
                RaisePropertyChanged(nameof(CurrentPageViewModel));
            }
        }



        public MainViewModel()
        {
            // Add available pages and set page
            PageViewModels.Add(ViewModelLocator.MainMenu);
            PageViewModels.Add(ViewModelLocator.Game);
            PageViewModels.Add(ViewModelLocator.Settings);

            //todo

            CurrentPageViewModel = PageViewModels[0];

            EventMediator.Subscribe(nameof(ViewModelLocator.MainMenu.GoToGameCommand), OnGoGameScreen);
            EventMediator.Subscribe(nameof(ViewModelLocator.Game.GoToMenuCommand), OnGoMenuScreen);
            EventMediator.Subscribe(nameof(ViewModelLocator.MainMenu.GoToExitCommand), OnGoExit);
            EventMediator.Subscribe(nameof(ViewModelLocator.MainMenu.GoToSettingsCommand), OnGoSettingsScreen);
            EventMediator.Subscribe(nameof(ViewModelLocator.Settings.GoToMenuCommand), OnGoMenuScreen);
        }


        private void ChangeViewModel(ObservableObject viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        private void OnGoGameScreen(object obj)
        {
            ChangeViewModel(PageViewModels[1]);
        }

        private void OnGoMenuScreen(object obj)
        {
            ChangeViewModel(PageViewModels[0]);
        }

        private void OnGoSettingsScreen(object obj)
        {
            ChangeViewModel(PageViewModels[2]);
        }

        private void OnGoExit(object obj)
        {
            Application curApp = Application.Current;
            curApp.Shutdown();
        }
    }
}
