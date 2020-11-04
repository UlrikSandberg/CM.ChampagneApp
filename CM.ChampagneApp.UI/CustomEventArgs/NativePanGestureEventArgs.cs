using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class NativePanGestureEventArgs
    {
        public double PointOfTouchInViewX { get; private set; }
        public double PointOfTouchInViewY { get; private set; }

        public NativePanGestureEventArgs(double PointInViewX, double PointInViewY)
        {
            PointOfTouchInViewX = PointInViewX;
            PointOfTouchInViewY = PointInViewY;
        }
    }
}
