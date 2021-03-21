using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Model;

namespace TicTacToe.ViewModel
{
    public class CellDisplayList
    {
        public CellDisplayList()
        {
            EventMediator.Subscribe(nameof(GameViewModel.GameEndedCommand), GameOverCB);
        }

        ~CellDisplayList()
        {
            EventMediator.Unsubscribe(nameof(GameViewModel.GameEndedCommand), GameOverCB);
        }

        public void NewGame()
        {
            Cells = new ObservableCollection<CellDisplay>()
            {
                new CellDisplay(0, 0),
                new CellDisplay(0, 1),
                new CellDisplay(0, 2),
                new CellDisplay(1, 0),
                new CellDisplay(1, 1),
                new CellDisplay(1, 2),
                new CellDisplay(2, 0),
                new CellDisplay(2, 1),
                new CellDisplay(2, 2),
            };
        }

        public void DesignGame()
        {
            Cells = new ObservableCollection<CellDisplay>()
            {
                new CellDisplay(0, 0){ Image = ImageProvider.Instance.OImage, Selectable = false },
                new CellDisplay(0, 1),
                new CellDisplay(0, 2),
                new CellDisplay(1, 0),
                new CellDisplay(1, 1){ Image = ImageProvider.Instance.OImage, Selectable = false },
                new CellDisplay(1, 2),
                new CellDisplay(2, 0),
                new CellDisplay(2, 1){ Image = ImageProvider.Instance.XImage, Selectable = false },
                new CellDisplay(2, 2){ Image = ImageProvider.Instance.OImage, Selectable = false },
            };
        }

        private void GameOverCB(object _)
        {
            var selectable = Cells.Where(c => c.Selectable);
            foreach (CellDisplay cell in selectable)
                cell.Selectable = false;
        }

        public ObservableCollection<CellDisplay> Cells { get; private set; }

    }
}
