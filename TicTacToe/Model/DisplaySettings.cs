using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TicTacToe.Game;

namespace TicTacToe.Model
{
    public interface IDisplaySettings
    {
        Color BackgroundColor { get; }
        Color SquareColor { get; }
        Color ButtonColor { get; }
        Color FontColor { get; }
        Color XColor { get; }
        Color OColor { get; }

        Color GetPlayerColor(GameTypes.Player player);
    }

    class DisplaySettingsProvider : IDisplaySettings
    {
        private readonly Color backgroundColor;
        private readonly Color squareColor;
        private readonly Color xColor;
        private readonly Color oColor;
        private readonly Color buttonColor;
        private readonly Color fontColor;

        public DisplaySettingsProvider()
        {
            backgroundColor = GameSettingsAdapter.BackgroundColor;
            squareColor = GameSettingsAdapter.SquareColor;
            xColor = GameSettingsAdapter.XColor;
            oColor = GameSettingsAdapter.OColor;
            buttonColor = GameSettingsAdapter.ButtonColor;
            fontColor = GameSettingsAdapter.FontColor;
        }
        public Color BackgroundColor => backgroundColor;

        public Color SquareColor => squareColor;

        public Color XColor => xColor;

        public Color OColor => oColor;

        public Color ButtonColor => buttonColor;

        public Color FontColor => fontColor;

        public Color GetPlayerColor(GameTypes.Player player) =>
            player switch
            {
                GameTypes.Player.X => XColor,
                GameTypes.Player.O => OColor,
                _ => throw new NotImplementedException(),
            };
    }


}
