using System;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.CustomLayouts
{
    struct LayoutData
    {
        public int VisibleChildCount { get; private set; }

        public Size CellSize { get; private set; }

        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public LayoutData(int visibleChildCount, Size cellSize, int rows, int columns) : this()
        {
            VisibleChildCount = visibleChildCount;
            CellSize = cellSize;
            Rows = rows;
            Columns = columns;
        }

    }
}
