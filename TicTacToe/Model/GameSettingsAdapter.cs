using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TicTacToe.Game;
using TicTacToe.Properties;

namespace TicTacToe.Model
{

    public class GameSettingsAdapter
    {
        public static bool SinglePlayer => Settings.Default.SinglePlayer;

        public static GameTypes.Player HumanPlayer => 
            Settings.Default.HumanPlayer == 'X' ? GameTypes.Player.X: GameTypes.Player.O;

        public static Color XColor => ToWinMedColor(Settings.Default.XColor);

        public static Color OColor => ToWinMedColor(Settings.Default.OColor);

        public static Color SquareColor => ToWinMedColor(Settings.Default.SquareColor);

        public static Color BackgroundColor => ToWinMedColor(Settings.Default.BackgroundColor);

        public static Color ButtonColor => ToWinMedColor(Settings.Default.ButtonColor);

        public static Color FontColor => ToWinMedColor(Settings.Default.FontColor);

        private static Color ToWinMedColor(System.Drawing.Color color) =>
            Color.FromArgb(color.A, color.R, color.G, color.B);
    }
}
