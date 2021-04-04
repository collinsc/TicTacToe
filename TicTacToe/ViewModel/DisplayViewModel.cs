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

        public Color BackgroundColor => _backgroundColor ??= SettingsFactory.GetDisplaySettings().BackgroundColor;

        private static Color? _buttonColor;
        public Color ButtonColor => _buttonColor ??= SettingsFactory.GetDisplaySettings().ButtonColor;


        private static Color? _fontColor;
        public Color FontColor => _fontColor ??= SettingsFactory.GetDisplaySettings().FontColor;

        private static Color? _squareColor;
        public Color SquareColor => _squareColor ??= SettingsFactory.GetDisplaySettings().SquareColor;

    }
}
