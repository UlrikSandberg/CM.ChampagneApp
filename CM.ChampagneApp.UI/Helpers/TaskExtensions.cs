using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;

namespace CM.ChampagneApp.UI.Helpers
{
    public static class TaskExtensions
    {
        public static void RunForget(this Task t)
        {
            t.ContinueWith((tResult) => 
                {
                    var ex = t.Exception.Flatten();
                    Debug.WriteLine(ex.Message + ":\n " + ex.StackTrace);
                    Crashes.TrackError(ex);

                },
                TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
