using TicTacToe.Game;

namespace TicTacToe.Model
{
    public interface IGameplaySettings
    {
        GameMode Mode { get; }
        GameTypes.Player HumanPlayer { get; }

        GameTypes.Difficulty Difficulty {get;}
    }
    class GameplaySettingsProvider : IGameplaySettings
    {

        private readonly Setting<GameMode> mode;
        private readonly Setting<GameTypes.Player> humanPlayer;
        private readonly Setting<GameTypes.Difficulty> difficulty;

        public GameplaySettingsProvider()
        {
            mode = SettingsFacade.GameMode;
            humanPlayer = SettingsFacade.HumanPlayer;
            difficulty = SettingsFacade.Difficulty;
        }
        public GameMode Mode => mode.Value;

        public GameTypes.Player HumanPlayer => humanPlayer.Value;

        public GameTypes.Difficulty Difficulty => difficulty.Value;
    }
}