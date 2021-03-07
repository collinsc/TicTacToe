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
using TicTacToe.Models;
using TicTacToe.View;

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
        public ImageSource Image { get; 
            set; }
        public  int Row { get; set; }
        public int Col { get; set;  }

        private ICommand _clickSquare;

        public ICommand Click => _clickSquare ??= new RelayCommand(() =>
            {
                var fun = SimpleIoc.Default.GetInstance<GameViewModel>().ClickSquare;
                if (fun.CanExecute(Index))
                    fun.Execute(Index);
            });

        public (int, int) Index => (Row, Col);
    }
    public class GameViewModel : ViewModelBase
    {
        private ICommand _goToMenu;
        private ICommand _clickSquare;
        private static ImageSource _xImage;
        private static ImageSource XImage => _xImage ??= (_xImage = CreateImage(new Uri("pack://application:,,,/TicTacToe;component/Resources/x-blue.png")));
        private static ImageSource _oImage;
        private static ImageSource OImage => _oImage ??= (_oImage = CreateImage(new Uri("pack://application:,,,/TicTacToe;component/Resources/o-blue.png")));

        public InternalGameService Service { get; private set; }

        public ObservableCollection<CellDisplay> Cells { get; private set; }
        public ObservableCollection<Line> Lines { get; private set; }

        public GameViewModel() : base()
        {
            NavigationMediator.Subscribe("GoToGame", StartGame);

        }

        private void StartGame(object obj)
        {
            Service = new InternalGameService();
            Cells = new ObservableCollection<CellDisplay>() 
            { 
                new CellDisplay { Image = null, Row=0, Col=0 },
                new CellDisplay { Image = null, Row=0, Col=1 },
                new CellDisplay { Image = null, Row=0, Col=2 }, 
                new CellDisplay { Image = null, Row=1, Col=0 },
                new CellDisplay { Image = null, Row=1, Col=1 },
                new CellDisplay { Image = null, Row=1, Col=2 },
                new CellDisplay { Image = null, Row=2, Col=0 },
                new CellDisplay { Image = null, Row=2, Col=1 },
                new CellDisplay { Image = null, Row=2, Col=2 }
            };
            Lines = new ObservableCollection<Line>();
        }

        public ICommand GoToMenu => _goToMenu ??= new RelayCommand(() =>
                                                  {
                                                      NavigationMediator.Notify(nameof(GoToMenu), "");
                                                  });


        public ICommand ClickSquare => _clickSquare ??= new RelayCommand<(int, int)>
                    (
                        (tup) =>
                        {
                            if (Service.TakeMove(tup))
                            {
                                var cell = Cells.FirstOrDefault(c => c.Index == tup);
                                ActivePlayer player = Service.CurrentPlayer;
                                if (cell.Image == null)
                                {
                                    cell.Image = player == ActivePlayer.X ? XImage : OImage;
                                    cell.RaisePropertyChanged("Image");
                                    this.RaisePropertyChanged(nameof(Cells));
                                }
                                if (Service.IsGameOver)
                                {
                                    UpdateWinStates(Service.WinStates, player);
                                    this.RaisePropertyChanged(nameof(Lines));
                                    this.RaisePropertyChanged(nameof(IsGameOver));
                                }
                            }
                        },
                        (tup) =>
                        {
                            return !Service.IsGameOver && Service.IsValidMove(tup);
                        }
                    );

        public bool IsGameOver => Service.IsGameOver;

        public bool IsGameAfoot => !IsGameOver;

        private void UpdateWinStates(InternalGameService.WinState[] winStates, ActivePlayer player)
        {
            int edgOfst = 25;
            int sqSize = 200;
            (double, double, double, double) GetRow(int row) => ( -edgOfst + 0 * sqSize, edgOfst + 3 * sqSize, (row + 0.5) * sqSize, (row + 0.5) * sqSize );
            (double, double, double, double) GetCol(int col) => ( (col + 0.5) * sqSize, (col + 0.5) * sqSize, -edgOfst + 0 * sqSize, edgOfst + 3 * sqSize );

            (double x1, double x2, double y1, double y2) GetLineIndexes(InternalGameService.WinState state) =>
                state switch
                {
                    InternalGameService.WinState.RowA       => GetRow(0),
                    InternalGameService.WinState.RowB       => GetRow(1),
                    InternalGameService.WinState.RowC       => GetRow(2),
                    InternalGameService.WinState.ColA       => GetCol(0),
                    InternalGameService.WinState.ColB       => GetCol(1),
                    InternalGameService.WinState.ColC       => GetCol(2),
                    InternalGameService.WinState.DiagA      =>  ( -edgOfst / 2 + 0 * sqSize, edgOfst / 2 + 3 * sqSize, -edgOfst / 2 + 0 * sqSize, edgOfst / 2 + 3 * sqSize),
                    InternalGameService.WinState.DiagB      => ( edgOfst / 2 + 3 * sqSize, -edgOfst / 2 + 0 * sqSize, -edgOfst / 2 + 0 * sqSize, edgOfst / 2 + 3 * sqSize ),
                    _                                       => (-1, -1, -1, -1)
                };

            foreach (var state in winStates)
            {
                var (x1, x2, y1, y2) = GetLineIndexes(state);
                var line = new Line
                {
                    From = new Point(x1, y1),
                    To = new Point(x2, y2),
                    Brush = (player == ActivePlayer.X) ? new SolidColorBrush(Color.FromRgb(0, 168, 243)) : Brushes.White
                };
                Lines.Add(line);
            }
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
