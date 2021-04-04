using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Game;
using TicTacToe.Properties;

namespace TicTacToe.Model
{
    public class GameSettings
    {
        private static GameSettings _instance;
        public static GameSettings Instance => _instance ??= new GameSettings();
        public static bool SinglePlayer => Settings.Default.SinglePlayer;
        public static GameTypes.Player HumanPlayer => Settings.Default.HumanPlayer == 'X' ? GameTypes.Player.X: GameTypes.Player.O;

        public static System.Drawing.Color XColor => Settings.Default.XColor;
        public static System.Drawing.Color OColor => Settings.Default.OColor;

    }
}
