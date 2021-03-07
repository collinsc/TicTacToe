using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe.Models
{
    public class InternalGameService
    {
        public enum WinState { RowA, RowB, RowC, ColA, ColB, ColC, DiagA, DiagB}
        private GameState _State;
        private WinState[] _WinStates;

        internal InternalGameService(GameState state)
        {
            _State = state;
            _WinStates = CalculateWinStates();

        }
        public InternalGameService() : this(new GameState())
        {
        }

        public GameState State { get => (GameState)_State.Clone(); }

        public bool IsGameOver => WinStates?.Length > 0;

        public WinState[] WinStates { get =>(WinState[])_WinStates.Clone(); private set => _WinStates = value; }
        public ActivePlayer CurrentPlayer => _State.ActivePlayer;

        public bool TakeMove((int, int) idx)
        {
            if (!IsValidMove(idx))
                return false;

            _State[idx.Item1, idx.Item2] = (Square)_State.ActivePlayer;
            WinStates = CalculateWinStates();


            _State.ActivePlayer = _State.ActivePlayer == ActivePlayer.X ? ActivePlayer.O : ActivePlayer.X;

            return true;
        }

        public WinState[] CalculateWinStates()
        {
            var output = new List<WinState>();

            if (_State[0, 0] != Square.Empty && _State[0, 0] == _State[0, 1] && _State[0, 0] == _State[0, 2])
                output.Add(WinState.RowA);
            if (_State[1, 0] != Square.Empty && _State[1, 0] == _State[1, 1] && _State[1, 0] == _State[1, 2])
                output.Add(WinState.RowB);
            if (_State[2, 0] != Square.Empty && _State[2, 0] == _State[2, 1] && _State[2, 0] == _State[2, 2])
                output.Add(WinState.RowC);
            if (_State[0, 0] != Square.Empty && _State[0, 0] == _State[1, 0] && _State[0, 0] == _State[2, 0])
                output.Add(WinState.ColA);
            if (_State[0, 1] != Square.Empty && _State[0, 1] == _State[1, 1] && _State[0, 1] == _State[2, 1])
                output.Add(WinState.ColB);
            if (_State[0, 2] != Square.Empty && _State[0, 2] == _State[1, 2] && _State[0, 2] == _State[2, 2])
                output.Add(WinState.ColC);
            if (_State[0, 0] != Square.Empty && _State[0, 0] == _State[1, 1] && _State[0, 0] == _State[2, 2])
                output.Add(WinState.DiagA);
            if (_State[2, 0] != Square.Empty && _State[2, 0] == _State[1, 1] && _State[2, 0] == _State[0, 2])
                output.Add(WinState.DiagB);
            return output.ToArray();
        }


        public string GetPhotoPath(Tuple<int, int> idx)
        {
            switch (_State[idx.Item1, idx.Item2])
            {
                case (Square.Empty):
                    return "";
                case (Square.X):
                    return "x.png";
                case (Square.O):
                    return "o.png";
                default:
                    throw new NotImplementedException();
            }
        }

        public bool IsValidMove((int, int) idx)
        {
            return _State[idx.Item1, idx.Item2] == Square.Empty;
        }
    }
}
