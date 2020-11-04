using System;
using UIKit;
using CM.ChampagneApp.UI;
using Xamarin.Forms;
using CM.ChampagneApp.UI.Dependency;

[assembly: Dependency(typeof(CM.ChampagneApp.iOS.IOSDisplaySettings))]
namespace CM.ChampagneApp.iOS
{
    public class IOSDisplaySettings : IDisplaySettings
    {
        private CoreGraphics.CGRect bounds;

        public IOSDisplaySettings()
        {
            bounds = UIScreen.MainScreen.Bounds;
        }

        public int Height => (int)bounds.Height;

        public int Width => (int)bounds.Width;

    }
}

