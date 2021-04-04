using TicTacToe.Game;

namespace TicTacToe.Model
{
    public interface IGameplaySettings
    {
        bool SinglePlayer { get; }
        GameTypes.Player HumanPlayer { get; }
    }
    class GameplaySettingsProvider : IGameplaySettings
    {
        private readonly bool singlePlayer;
        private readonly GameTypes.Player humanPlayer;

        public GameplaySettingsProvider()
        {
            singlePlayer = GameSettingsAdapter.SinglePlayer;
            humanPlayer = GameSettingsAdapter.HumanPlayer;
        }
        public bool SinglePlayer => singlePlayer;

        public GameTypes.Player HumanPlayer => humanPlayer;
    }
}