using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TicTacToe.ViewModel
{
    static class PlayerShapeFactory
    {
        private static readonly int PADDING = 10;
        private static readonly int THICKNESS = 5;
        private static readonly int WIDTH = 200;

        public static Shape[] CreateX() => new Shape[] {
                new Line()
                {
                    X1 = PADDING,
                    Y1 = PADDING,
                    X2 = WIDTH - PADDING,
                    Y2 = WIDTH - PADDING,
                    Stroke=new SolidColorBrush(DisplayViewModel.XColor),
                    StrokeThickness=THICKNESS,
                },
                new Line()
                {
                    X1 = WIDTH - PADDING,
                    Y1 = PADDING,
                    X2 = PADDING,
                    Y2 = WIDTH - PADDING,
                    Stroke=new SolidColorBrush(DisplayViewModel.XColor),
                    StrokeThickness=THICKNESS,
                }};


        public static Shape[] CreateO() => new Shape[] {
                new Ellipse()
                {
                    Width = WIDTH - 2*PADDING,
                    Height= WIDTH - 2*PADDING,
                    Margin = new System.Windows.Thickness() { Top = PADDING, Left = PADDING },
                    Stroke = new SolidColorBrush(DisplayViewModel.OColor),
                    StrokeThickness = THICKNESS,
                }, };



    }
}
