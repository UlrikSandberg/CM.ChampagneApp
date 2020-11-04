using System;
using System.Security.Cryptography.X509Certificates;
namespace CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers
{
    public interface INavigationContainerService
    {
		string CreateMainContainer(bool isSignUpOrLogin = false);
		void DisposeMainContainer();
    }
}
