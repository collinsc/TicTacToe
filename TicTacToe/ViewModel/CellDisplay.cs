using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TicTacToe.Game;
using static TicTacToe.Game.GameTypes;

namespace TicTacToe.ViewModel
{
    public class CellDisplay : ObservableObject
    {

        private ICommand _clickSquare;

        private bool suppressIndexNotify;
        private int row;
        private int col;
        private bool selectable;
        private Shape[] shapes;

        public CellDisplay(int row, int col)
        {
            Index = (row, col);
            Selectable = true;
            Shapes = null ;
        }


 
        public Shape[] Shapes
        {
            get => shapes;
            set 
            {
                shapes = value;
                RaisePropertyChanged(nameof(Shapes));

            }
        }

        public void SetCellState(CellState state)
        {
            Shapes = state switch
            {
                CellState.Player p when p.Item == Player.X => PlayerShapeFactory.CreateX(),
                CellState.Player p when p.Item == Player.O => PlayerShapeFactory.CreateO(),
                _  => null
            };
            if (Shapes != null)
                Selectable = false;
        }

        public int Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
                RaisePropertyChanged(nameof(Row));
                if (!suppressIndexNotify)
                    RaisePropertyChanged(nameof(Index));
            }
        }

        public int Col
        {
            get
            {
                return col;
            }
            set
            {
                col = value;
                RaisePropertyChanged(nameof(Col));
                if (!suppressIndexNotify)
                    RaisePropertyChanged(nameof(Index));
            }
        }



        public ICommand ClickCommand => _clickSquare ??= new RelayCommand(() =>
            {
                var fun = ViewModelLocator.Game.ClickSquareCommand;
                if (fun.CanExecute(Index))
                    fun.Execute(Index);
            });

        public (int, int) Index
        {
            get => (Row, Col);
            set
            {
                suppressIndexNotify = true;
                try
                {
                    Row = value.Item1;
                    Col = value.Item2;
                }
                finally { suppressIndexNotify = false; }

                RaisePropertyChanged(nameof(Index));
            }
        }

        

        public bool Selectable
        {
            get => selectable;
            set
            {
                selectable = value;
                RaisePropertyChanged(nameof(Selectable));
            }
        }

    }
}
