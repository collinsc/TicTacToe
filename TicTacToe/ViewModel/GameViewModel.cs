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

    public class GameViewModel : ViewModelBase
    {


        private ICommand _goToMenu;
        private ICommand _clickSquare;
        private ICommand _gameEndedCommand;
        private CellDisplayList cellList;

        public GameService Session { get; set; }

        public ObservableCollection<CellDisplay> Cells => CellList.Cells;

        public CellDisplayList CellList => cellList ??= cellList = new CellDisplayList();
        public ObservableCollection<LineDisplay> WinStateCoordinates { get; private set; }

        public GameViewModel() : base()
        {
            EventMediator.Subscribe(nameof(ViewModelLocator.Instance.MainMenu.GoToGameCommand), StartGame);
        }

        private void StartGame(object obj)
        {
            GameTypes.IGameService instance = ServiceFactory.CreateInstanceRandomPlayer();
            Session = new GameService(instance, GameSettings.Instance);
            CellList.NewGame();
            WinStateCoordinates = new ObservableCollection<LineDisplay>();
            this.RaisePropertyChanged(nameof(IsGameOver));
            Session.Start();
            UpdateCells();
        }

        public ICommand GoToMenuCommand => _goToMenu ??= new RelayCommand(() =>
                    {
                        EventMediator.Notify(nameof(GoToMenuCommand), "");
                    });

        public ICommand GameEndedCommand => _gameEndedCommand ??= new RelayCommand(() =>
        {
            EventMediator.Notify(nameof(GameEndedCommand), "");
        });


        private void UpdateCells()
        {
            foreach (var cell in Cells)
            {
                if (cell.Selectable && !Session.IsValidMove(cell.Index))
                {
                    cell.Image = Session.GetCellState(cell.Index) switch
                    {
                        GameTypes.CellState.X => ImageProvider.Instance.XImage,
                        GameTypes.CellState.O => ImageProvider.Instance.OImage,
                        _ => throw new NotImplementedException()
                    };
                    cell.Selectable = false;
                }
            }
        }

        public ICommand ClickSquareCommand => _clickSquare ??= new RelayCommand<(int, int)>
                    (
                        (tup) =>
                        {
                            Session.TakeTurn(tup);
                            UpdateCells();


                            if (Session.IsGameOver)
                                DoGameOver();
                        },
                        (tup) =>
                        {
                            return !Session.IsGameOver && Session.IsValidMove(tup);
                        }
                    );

        private void DoGameOver()
        {
            var player = Session.CurrentPlayer;
            UpdateWinCoordinates(Session.EndState, player);
            this.RaisePropertyChanged(nameof(IsGameOver));
            EventMediator.Notify(nameof(GameEndedCommand));
        }

        public bool IsGameOver => Session.IsGameOver;

        private void UpdateWinCoordinates(GameTypes.EndCondition winState, GameTypes.Turn player)
        {

            var line = LineDisplay.CreateLineDisplay(Session.EndState, Session.WinningPlayer);
            WinStateCoordinates.Add(line);
            this.RaisePropertyChanged(nameof(WinStateCoordinates));
        }

    }
}
