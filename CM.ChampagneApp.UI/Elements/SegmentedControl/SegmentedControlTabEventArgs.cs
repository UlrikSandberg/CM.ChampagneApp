using System;
namespace CM.ChampagneApp.UI.Elements.SegmentedControl
{
    public class SegmentedControlTabEventArgs
    {
        public int TabIndex { get; private set; }

		public SegmentedControlTabEventArgs(int tabIndex)
        {
            TabIndex = tabIndex;
		}
    }
}
