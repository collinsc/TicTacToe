using System.Windows;
using System.Windows.Media;
using TicTacToe.Game;

namespace TicTacToe.ViewModel
{
    public class LineDisplay
    {
        public Point From { get; set; }

        public Point To { get; set; }
        public Brush Brush { get; set; }

        public static LineDisplay CreateLineDisplay(GameTypes.EndCondition winState, GameTypes.Turn winningPlayer)
        {

            int edgOfst = 25;
            int sqSize = 200;
            (double, double, double, double) GetRow(int row) => (-edgOfst + 0 * sqSize, edgOfst + 3 * sqSize, (row + 0.5) * sqSize, (row + 0.5) * sqSize);
            (double, double, double, double) GetCol(int col) => ((col + 0.5) * sqSize, (col + 0.5) * sqSize, -edgOfst + 0 * sqSize, edgOfst + 3 * sqSize);

            (double x1, double x2, double y1, double y2) GetLineIndexes(GameTypes.EndCondition state) =>
                state switch
                {
                    GameTypes.EndCondition.Row1 => GetRow(0),
                    GameTypes.EndCondition.Row2 => GetRow(1),
                    GameTypes.EndCondition.Row3 => GetRow(2),
                    GameTypes.EndCondition.Column1 => GetCol(0),
                    GameTypes.EndCondition.Column2 => GetCol(1),
                    GameTypes.EndCondition.Column3 => GetCol(2),
                    GameTypes.EndCondition.DiagonalMajor => (-edgOfst / 2 + 0 * sqSize, edgOfst / 2 + 3 * sqSize, -edgOfst / 2 + 0 * sqSize, edgOfst / 2 + 3 * sqSize),
                    GameTypes.EndCondition.DiagonalMinor => (edgOfst / 2 + 3 * sqSize, -edgOfst / 2 + 0 * sqSize, -edgOfst / 2 + 0 * sqSize, edgOfst / 2 + 3 * sqSize),
                    _ => (-1, -1, -1, -1)
                };

            var (x1, x2, y1, y2) = GetLineIndexes(winState);
            return new LineDisplay
            {
                From = new Point(x1, y1),
                To = new Point(x2, y2),
                Brush = (winningPlayer == GameTypes.Turn.XTurn) ? new SolidColorBrush(Color.FromRgb(0, 168, 243)) : Brushes.White
            };
        }

    }

}
