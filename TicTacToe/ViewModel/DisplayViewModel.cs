using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TicTacToe.Model;

namespace TicTacToe.ViewModel
{
    public class DisplayViewModel : ViewModelBase
    {

        private static Color? _backgroundColor;

        public static Color BackgroundColor => _backgroundColor ??= SettingsFactory.GetDisplaySettings().BackgroundColor;

        private static Color? _buttonColor;
        public static Color ButtonColor => _buttonColor ??= SettingsFactory.GetDisplaySettings().ButtonColor;


        private static Color? _fontColor;
        public static Color FontColor => _fontColor ??= SettingsFactory.GetDisplaySettings().FontColor;

        private static Color? _squareColor;

        public static Color SquareColor => _squareColor ??= SettingsFactory.GetDisplaySettings().SquareColor;


        private static Color? _xColor;

        public static Color XColor => _xColor ??= SettingsFactory.GetDisplaySettings().XColor;

        private static Color? _oColor;

        public static Color OColor => _oColor ??= SettingsFactory.GetDisplaySettings().OColor;

    }
}
