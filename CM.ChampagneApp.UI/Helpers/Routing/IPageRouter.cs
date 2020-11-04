using FreshMvvm;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Helpers.Routing
{
    public interface IPageRouter
    {
       Task RouteToPath(string url, IPageModelCoreMethods coreMethods);
    }
}