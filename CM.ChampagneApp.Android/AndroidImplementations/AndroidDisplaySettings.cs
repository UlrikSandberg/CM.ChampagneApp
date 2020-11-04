using System;
using CM.ChampagneApp.UI.Dependency;
using Xamarin.Forms;
using CM.ChampagneApp.Android.AndroidImplementations;

[assembly: Dependency(typeof(CM.ChampagneApp.Android.AndroidImplementations.AndroidDisplaySettings))]
namespace CM.ChampagneApp.Android.AndroidImplementations
{
    public class AndroidDisplaySettings : IDisplaySettings
    {
        public int Height => throw new NotImplementedException();

        public int Width => throw new NotImplementedException();
        
    }
}