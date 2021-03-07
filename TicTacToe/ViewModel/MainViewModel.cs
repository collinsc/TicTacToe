using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TicTacToe.Models;

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
                RaisePropertyChanged("CurrentPageViewModel");
            }
        }

        public MainViewModel()
        {
            // Add available pages and set page
            PageViewModels.Add(SimpleIoc.Default.GetInstance<MainMenuViewModel>());
            PageViewModels.Add(SimpleIoc.Default.GetInstance<GameViewModel>());

            CurrentPageViewModel = PageViewModels[0];

            NavigationMediator.Subscribe("GoToGame", OnGoGameScreen);
            NavigationMediator.Subscribe("GoToMenu", OnGoMenuScreen);
            NavigationMediator.Subscribe("GoToExit", OnGoExit);
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

        private void OnGoExit(object obj)
        {
            Application curApp = Application.Current;
            curApp.Shutdown();
        }
    }
}
