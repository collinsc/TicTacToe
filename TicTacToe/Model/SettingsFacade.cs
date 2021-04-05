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



    public class SettingsFacade
    {
        public static Setting<GameMode> GameMode
        {
            get
            {
                return new()
                {
                    Name = "Game Mode",
                    Description = "Number of players.",
                    Value = (GameMode)Settings.Default.GameMode
                };
            }
        }
        public static void SetGameMode(GameMode mode)
        {
            Settings.Default.GameMode = (int)mode;
            Save();
        }

        public static Setting<GameTypes.Difficulty> Difficulty
        {
            get
            {
                return new()
                {
                    Name = "Difficulty",
                    Description = "",
                    Value = (GameTypes.Difficulty)Settings.Default.Difficulty
                };
            }
        }
        public static void SetDifficulty(GameTypes.Difficulty difficulty)
        {
            Settings.Default.Difficulty = (int)difficulty;
            Save();
        }


        public static Setting<GameTypes.Player> HumanPlayer
        {
            get
            {
                return new()
                {
                    Name = "Human Player",
                    Description = "The user player for single player mode.",
                    Value = Settings.Default.HumanPlayer == 'X' ? GameTypes.Player.X : GameTypes.Player.O
                };
            }
        }



        public static Setting<Color> XColor => new() { Value = ToWinMedColor(Settings.Default.XColor) };

        public static Setting<Color> OColor => new() { Value = ToWinMedColor(Settings.Default.OColor) };

        public static Setting<Color> SquareColor => new() { Value = ToWinMedColor(Settings.Default.SquareColor) };

        public static Setting<Color> BackgroundColor => new() { Value = ToWinMedColor(Settings.Default.BackgroundColor) };

        public static Setting<Color> ButtonColor => new() { Value = ToWinMedColor(Settings.Default.ButtonColor) };

        public static Setting<Color> FontColor => new() { Value = ToWinMedColor(Settings.Default.FontColor) };

        private static Color ToWinMedColor(System.Drawing.Color color) =>
            Color.FromArgb(color.A, color.R, color.G, color.B);

        public static void Save() => Settings.Default.Save();

        public static Color GetPlayerColor(GameTypes.Player player) =>
            player switch
            {
                GameTypes.Player.X => XColor.Value,
                GameTypes.Player.O => OColor.Value,
                _ => throw new NotImplementedException(),
            };
    }
}
