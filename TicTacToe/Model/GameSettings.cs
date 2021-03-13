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
        public bool SinglePlayer => Settings.Default.SinglePlayer;
        public GameTypes.Turn HumanPlayer => Settings.Default.HumanPlayer == 'X' ? GameTypes.Turn.XTurn : GameTypes.Turn.OTurn;
    }
}
