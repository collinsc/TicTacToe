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
        private readonly IGameplaySettings gameSettings;

  
        public GameService(IGameService implementation, IGameplaySettings settings)
        {
            Instance = implementation;
            gameSettings = settings;
        }

        public void Start()
        {
            if (gameSettings.Mode == GameMode.SinglePlayer && gameSettings.HumanPlayer != Instance.ActivePlayer)
                Instance.TakeAITurn(gameSettings.Difficulty);
        }

        private IGameService Instance { get; }

        public Player? CurrentPlayer => Instance.ActivePlayer;

        public bool IsGameOver => Instance.IsOver;

        public EndCondition? EndState => Instance.EndCondition.EndCondition;

        public void TakeTurn((int, int) index)
        {
            if (gameSettings.Mode == GameMode.SinglePlayer)
            {
                Instance.TakeTurn(index.Item1, index.Item2);
                if (!Instance.IsOver)
                    Instance.TakeAITurn(gameSettings.Difficulty);
            }
            else
                Instance.TakeTurn(index.Item1, index.Item2);

        }

        public bool IsValidMove((int, int) index) => Instance.IsLegal(index.Item1, index.Item2);

        public CellState GetCellState((int, int) index) => Instance.GetCellState(index.Item1, index.Item2);

        public Player? WinningPlayer => Instance.WinningPlayer;
    }
}
