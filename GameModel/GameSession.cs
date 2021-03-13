using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game;
using Microsoft.FSharp;

namespace TicTacToe.Model
{
    public class GameSession
    {
        public GameSession(GameTypes.IGameService ticTacToeGame) => Instance = ticTacToeGame;

        private GameTypes.IGameService Instance { get; }

        public GameTypes.Turn CurrentPlayer => Instance.ActivePlayer;

        public bool IsGameOver { get; set; }

        public object EndState { get; set; }

        public void TakeTurn((int, int) index) => Instance.TakeTurn(index.Item1, index.Item2);

        public bool IsValidMove((int, int) index) => Instance.IsLegal(index.Item1, index.Item2);

        public GameTypes.CellState GetCellState((int, int) index) => Instance.GetCellState(index.Item1, index.Item2);
    }
}
