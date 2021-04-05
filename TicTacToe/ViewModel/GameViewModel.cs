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
using static TicTacToe.Game.GameTypes;

using System.Collections.Generic;

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
            EventMediator.Subscribe(nameof(ViewModelLocator.MainMenu.GoToGameCommand), StartGame);
        }

        private void StartGame(object obj)
        {
            IGameService instance = Interfaces.ServiceFactory.CreateInstanceRandomPlayer();
            IGameplaySettings settings = new GameplaySettingsProvider();
            Session = new GameService(instance, settings);
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

        internal void PopulateDesignTime()
        {
            CellList.DesignGame();
            WinStateCoordinates = new ObservableCollection<LineDisplay>();
            var lines = new LineDisplay[]
            {
                LineDisplay.CreateLineDisplay(EndCondition.DiagonalMajor, Player.X),
                LineDisplay.CreateLineDisplay(EndCondition.Column1, Player.O)
            };

            UpdateWinCoordinates(lines);
            this.RaisePropertyChanged(nameof(IsGameOver));
        }

        public ICommand GameEndedCommand => _gameEndedCommand ??= new RelayCommand(() =>
        {
            EventMediator.Notify(nameof(GameEndedCommand), "");
        });


        private void UpdateCells()
        {
            foreach (var cell in Cells)
            {
                if (cell.Selectable && !Session.IsValidMove(cell.Index))
                    cell.SetCellState(Session.GetCellState(cell.Index));
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
            // currently engine supports one win condition
            var lines = Session.WinningPlayer.HasValue?
                new LineDisplay[] { LineDisplay.CreateLineDisplay(Session.EndState.Value, Session.WinningPlayer.Value) } :
                null;
            UpdateWinCoordinates(lines);
            this.RaisePropertyChanged(nameof(IsGameOver));
            EventMediator.Notify(nameof(GameEndedCommand));
        }

        public bool IsGameOver => Session.IsGameOver;

        private void UpdateWinCoordinates(IEnumerable<LineDisplay> lines)
        {
            WinStateCoordinates.Clear();
            if (lines != null)
            {
                foreach (var line in lines)
                    WinStateCoordinates.Add(line);
            }
            this.RaisePropertyChanged(nameof(WinStateCoordinates));
        }

    }
}
