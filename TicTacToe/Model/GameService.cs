using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Game;
using static TicTacToe.Game.GameTypes;

namespace TicTacToe.Model
{
    public class GameService
    {
        public GameService(IGameService implementation, GameSettings settings)
        {
            Instance = implementation;
            Settings = settings;
        }

        public void Start()
        {
            if (GameSettings.SinglePlayer && GameSettings.HumanPlayer != Instance.ActivePlayer)
                Instance.TakeAITurn();
        }

        private IGameService Instance { get; }

        private GameSettings Settings { get; }

        public Player? CurrentPlayer => Instance.ActivePlayer;

        public bool IsGameOver => Instance.IsOver;

        public EndCondition? EndState => Instance.EndCondition.EndCondition;

        public void TakeTurn((int, int) index)
        {
            if (GameSettings.SinglePlayer)
            {
                Instance.TakeTurn(index.Item1, index.Item2);
                if (!Instance.IsOver)
                    Instance.TakeAITurn();
            }
            else
                Instance.TakeTurn(index.Item1, index.Item2);

        }

        public bool IsValidMove((int, int) index) => Instance.IsLegal(index.Item1, index.Item2);

        public CellState GetCellState((int, int) index) => Instance.GetCellState(index.Item1, index.Item2);

        public Player? WinningPlayer => Instance.WinningPlayer;
    }
}
