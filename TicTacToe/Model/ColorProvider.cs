using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TicTacToe.Game;

namespace TicTacToe.Model
{
    class ColorProvider
    {
        private static ColorProvider instance;
        public static ColorProvider Instance => instance ??= new ColorProvider();


        private Color? xColor;
        public Color XColorWinMed => xColor ??= ToWinMedColor(GameSettings.XColor);
        private Color? oColor;
        public Color OColorWinMed => oColor ??= ToWinMedColor(GameSettings.OColor);
        public Color GetPlayerColor(GameTypes.Player turn) =>
            turn switch
            {
                GameTypes.Player.X => XColorWinMed,
                GameTypes.Player.O => OColorWinMed,
                _ => throw new NotImplementedException(),
            };

        private static Color ToWinMedColor(System.Drawing.Color color) => Color.FromArgb(color.A, color.R, color.G, color.B);

    }
}
