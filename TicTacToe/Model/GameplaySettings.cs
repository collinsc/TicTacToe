using TicTacToe.Game;

namespace TicTacToe.Model
{
    public interface IGameplaySettings
    {
        GameMode Mode { get; }
        GameTypes.Player HumanPlayer { get; }
    }
    class GameplaySettingsProvider : IGameplaySettings
    {

        private readonly Setting<GameMode> mode;
        private readonly Setting<GameTypes.Player> humanPlayer;

        public GameplaySettingsProvider()
        {
            mode = SettingsFacade.GameMode;
            humanPlayer = SettingsFacade.HumanPlayer;
        }
        public GameMode Mode => mode.Value;

        public GameTypes.Player HumanPlayer => humanPlayer.Value;
    }
}