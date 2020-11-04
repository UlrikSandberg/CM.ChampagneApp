using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class KeyboardRaisedEventArgs
    {
        public double KeyboardHeight { get; private set; }

		public KeyboardRaisedEventArgs(double keyboardHeight)
        {
            KeyboardHeight = keyboardHeight;
		}
    }
}
