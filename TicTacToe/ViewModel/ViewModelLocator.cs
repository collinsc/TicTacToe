/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TicTacToe"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace TicTacToe.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {

        private static ViewModelLocator _instance;
        public static ViewModelLocator Instance => _instance ??= _instance = new ViewModelLocator();

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MainMenuViewModel>();
            SimpleIoc.Default.Register<GameViewModel>();
            SimpleIoc.Default.Register<DisplayViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();


            if (ViewModelBase.IsInDesignModeStatic)
                Game.PopulateDesignTime();

        }

        public static MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();

        public static MainMenuViewModel MainMenu => SimpleIoc.Default.GetInstance<MainMenuViewModel>();
        public static GameViewModel Game => SimpleIoc.Default.GetInstance<GameViewModel>();

        public static DisplayViewModel Display => SimpleIoc.Default.GetInstance<DisplayViewModel>();

        public static SettingsViewModel Settings => SimpleIoc.Default.GetInstance<SettingsViewModel>();


        public static void Cleanup()
        {
            SimpleIoc.Default.Unregister<MainViewModel>();
            SimpleIoc.Default.Unregister<MainMenuViewModel>();
            SimpleIoc.Default.Unregister<GameViewModel>();
            SimpleIoc.Default.Unregister<DisplayViewModel>();
            SimpleIoc.Default.Unregister<SettingsViewModel>();

        }
    }
}