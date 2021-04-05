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
        private static DisplaySettingsProvider _displaySettings;
        private static DisplaySettingsProvider DisplaySettings => _displaySettings ??= new DisplaySettingsProvider();

        public static Color BackgroundColor => DisplaySettings.BackgroundColor;

        public static Color ButtonColor => DisplaySettings.ButtonColor;

        public static Color FontColor => DisplaySettings.FontColor;

        public static Color SquareColor =>  DisplaySettings.SquareColor;

        public static Color XColor =>  DisplaySettings.XColor;

        public static Color OColor => DisplaySettings.OColor;

    }
}
