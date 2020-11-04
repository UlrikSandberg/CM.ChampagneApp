using System;
using System.IO;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Dependency
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
        Task<Stream> GetPhotoTakenStreamAsync();
    }
}
