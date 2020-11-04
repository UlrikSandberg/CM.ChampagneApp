using System;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class ImageChosenEventArgs
    {

        public byte[] file { get; private set; }

        public ImageChosenEventArgs(byte[] image)
        {
            file = image;
        }
    }
}
