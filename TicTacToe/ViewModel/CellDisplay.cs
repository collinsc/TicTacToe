using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace TicTacToe.ViewModel
{
    public class CellDisplay : ObservableObject
    {

        public CellDisplay(int row, int col)
        {
            
            Index = (row, col);
            Image = null;
            Selectable = true;
        }

 

        public ImageSource Image
        {
            get => image;
            set
            {
                image = value;
                RaisePropertyChanged(nameof(Image));
            }
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

        private ICommand _clickSquare;
        private ImageSource image;
        private bool suppressIndexNotify;
        private int row;
        private int col;
        private bool selectable;

        public ICommand ClickCommand => _clickSquare ??= new RelayCommand(() =>
            {
                var fun = ViewModelLocator.Instance.Game.ClickSquareCommand;
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
