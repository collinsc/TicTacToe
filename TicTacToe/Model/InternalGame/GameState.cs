using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class GameState : ICloneable
    {
        public ActivePlayer ActivePlayer { get; set; } 
        private Square[,] _Grid;




        public GameState()
        {
            _Grid = new Square[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    _Grid[i, j] = Square.Empty;
            ActivePlayer = ActivePlayer.X;
        }

        public GameState(GameState state)
        {
            _Grid = (Square[,])state._Grid.Clone();
            ActivePlayer = state.ActivePlayer;
        }

        public Square this[int i, int j]
        {
            get { return _Grid[i, j]; }
            set 
            { 
                _Grid[i, j] = value; 
            }
        }

        public object Clone()
        {
            return new GameState(this);
        }

    }
}
