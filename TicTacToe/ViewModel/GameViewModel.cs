using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using TicTacToe.Model;
using TicTacToe.Game;

namespace TicTacToe.ViewModel
{
    public class Line
    {
        public Point From { get; set; }

        public Point To { get; set; }
        public Brush Brush { get; set; }
    }

    public class CellDisplay : ObservableObject
    {
        public ImageSource Image { get; set; }
        public  int Row { get; set; }
        public int Col { get; set;  }

        private ICommand _clickSquare;

        public ICommand ClickCommand => _clickSquare ??= new RelayCommand(() =>
            {
                var fun = ViewModelLocator.Instance.Game.ClickSquareCommand;
                if (fun.CanExecute(Index))
                    fun.Execute(Index);
            });

        public (int, int) Index => (Row, Col);

        public bool Selectable { get; set; }
    }

    public class GameViewModel : ViewModelBase
    {


        private ICommand _goToMenu;
        private ICommand _clickSquare;
        private static ImageSource _xImage;
        private static ImageSource XImage => _xImage ??= (_xImage = CreateImage(new Uri("pack://application:,,,/TicTacToe;component/Resources/x-blue.png")));
        private static ImageSource _oImage;
        private static ImageSource OImage => _oImage ??= (_oImage = CreateImage(new Uri("pack://application:,,,/TicTacToe;component/Resources/o-blue.png")));

        public GameAdapter Session { get; set; }

        public ObservableCollection<CellDisplay> Cells { get; private set; }
        public ObservableCollection<Line> WinStateCoordinates { get; private set; }

        public GameViewModel() : base()
        {
            NavigationMediator.Subscribe(nameof(ViewModelLocator.Instance.MainMenu.GoToGameCommand), StartGame);
        }

        private void StartGame(object obj)
        {
            GameTypes.IGameService instance = ServiceFactory.CreateInstanceRandomPlayer();
            Session = new GameAdapter(instance);
            Cells = new ObservableCollection<CellDisplay>() 
            { 
                new CellDisplay { Image = null, Row=0, Col=0, Selectable = true },
                new CellDisplay { Image = null, Row=0, Col=1, Selectable = true },
                new CellDisplay { Image = null, Row=0, Col=2, Selectable = true }, 
                new CellDisplay { Image = null, Row=1, Col=0, Selectable = true },
                new CellDisplay { Image = null, Row=1, Col=1, Selectable = true },
                new CellDisplay { Image = null, Row=1, Col=2, Selectable = true },
                new CellDisplay { Image = null, Row=2, Col=0, Selectable = true },
                new CellDisplay { Image = null, Row=2, Col=1, Selectable = true },
                new CellDisplay { Image = null, Row=2, Col=2, Selectable = true }
            };
            WinStateCoordinates = new ObservableCollection<Line>();

            this.RaisePropertyChanged(nameof(IsGameOver));
        }

        public ICommand GoToMenuCommand => _goToMenu ??= new RelayCommand(() =>
                    {
                        NavigationMediator.Notify(nameof(GoToMenuCommand), "");
                    });


        public ICommand ClickSquareCommand => _clickSquare ??= new RelayCommand<(int, int)>
                    (
                        (tup) =>
                        {
                            Session.TakeTurn(tup);
                            foreach (var cell in Cells)
                            {
                                if (cell.Selectable && cell.Image == null && !Session.IsValidMove(cell.Index))
                                {
                                    cell.Image = Session.GetCellState(cell.Index) switch
                                    {
                                        Game.GameTypes.CellState.X => XImage,
                                        Game.GameTypes.CellState.O => OImage,
                                        _ => throw new NotImplementedException()
                                    };
                                    cell.Selectable = false;
                                    cell.RaisePropertyChanged("Image");
                                    this.RaisePropertyChanged(nameof(Cells));
                                }
                            }

                            if (Session.IsGameOver)
                            {
                                var player = Session.CurrentPlayer;
                                UpdateWinCoordinates(Session.EndState, player);
                                this.RaisePropertyChanged(nameof(WinStateCoordinates));
                                this.RaisePropertyChanged(nameof(IsGameOver));
                            }
                        },
                        (tup) =>
                        {
                            return !Session.IsGameOver && Session.IsValidMove(tup);
                        }
                    );

        public bool IsGameOver => Session.IsGameOver;





        private void UpdateWinCoordinates(Game.GameTypes.EndCondition winState, Game.GameTypes.Turn player)
        {
            int edgOfst = 25;
            int sqSize = 200;
            (double, double, double, double) GetRow(int row) => (-edgOfst + 0 * sqSize, edgOfst + 3 * sqSize, (row + 0.5) * sqSize, (row + 0.5) * sqSize);
            (double, double, double, double) GetCol(int col) => ((col + 0.5) * sqSize, (col + 0.5) * sqSize, -edgOfst + 0 * sqSize, edgOfst + 3 * sqSize);

            (double x1, double x2, double y1, double y2) GetLineIndexes(Game.GameTypes.EndCondition state) =>
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
            var line = new Line
            {
                From = new Point(x1, y1),
                To = new Point(x2, y2),
                Brush = (player == Session.CurrentPlayer) ? new SolidColorBrush(Color.FromRgb(0, 168, 243)) : Brushes.White
            };
            WinStateCoordinates.Add(line);
        }


        private static ImageSource CreateImage(Uri uri)
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.DecodePixelWidth = 175;
            image.UriSource = uri;
            image.CacheOption = BitmapCacheOption.None;
            image.EndInit();
            return image;
        }
    }
}
