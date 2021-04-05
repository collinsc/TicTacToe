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
        private readonly Setting<Color> backgroundColor;
        private readonly Setting<Color> squareColor;
        private readonly Setting<Color> xColor;
        private readonly Setting<Color> oColor;
        private readonly Setting<Color> buttonColor;
        private readonly Setting<Color> fontColor;

        public DisplaySettingsProvider()
        {
            backgroundColor = SettingsFacade.BackgroundColor;
            squareColor = SettingsFacade.SquareColor;
            xColor = SettingsFacade.XColor;
            oColor = SettingsFacade.OColor;
            buttonColor = SettingsFacade.ButtonColor;
            fontColor = SettingsFacade.FontColor;
        }
        public Color BackgroundColor => backgroundColor.Value;

        public Color SquareColor => squareColor.Value;

        public Color XColor => xColor.Value;

        public Color OColor => oColor.Value;

        public Color ButtonColor => buttonColor.Value;

        public Color FontColor => fontColor.Value;

        public Color GetPlayerColor(GameTypes.Player player) =>
            player switch
            {
                GameTypes.Player.X => XColor,
                GameTypes.Player.O => OColor,
                _ => throw new NotImplementedException(),
            };
    }


}
