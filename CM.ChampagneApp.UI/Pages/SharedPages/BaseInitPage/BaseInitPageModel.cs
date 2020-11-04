using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.UI.UIFacade.Services;
using FreshMvvm;
using Xamarin.Auth;
using Xamarin.Forms;
using CM.ChampagneApp.DTO.Documents;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers;

namespace CM.ChampagneApp.UI.Pages.SharedPages.BaseInitPage 
{
    public class BaseInitPageModel : FreshBasePageModel
    {
        public ICommand ValidateUser { get; set; }

        private readonly IUIAuthenticationService _uIAuthenticationService;
        private readonly IEventCollector _eventCollector;

        public BaseInitPageModel(IUIAuthenticationService uIAuthenticationService, IEventCollector eventCollector)
        {
            _uIAuthenticationService = uIAuthenticationService;
            _eventCollector = eventCollector;
            ValidateUser = new Command(async () => await OnValidateUser());
        }

        private async Task OnValidateUser()
        {
            //Pushing await to this task force app to continue reload ui - faking viewDidAppear
            await Task.Delay(1);
            //var response1 = await RefreshAccess();
            var user = App.AccountService.CurrentUser();

            //Decrypt the user
            if (user != null)
            {
                //Check if the current user is logged in or the acces tokens has expired
                if (!bool.Parse(user.Properties["IsLoggedIn"]))
                {
                    System.Diagnostics.Debug.WriteLine("User has logged out navigate to login");
                    NavigateToLogin();
                    return;
                }

                //Made by Jesper Start
                var property = user.Properties["ExpiresIn"];

                var parseResult = DateTime.MinValue;
                if (!DateTime.TryParse(property, out parseResult))
                {
                    System.Diagnostics.Debug.WriteLine("Failed to Parse User Property ExpiresIn to DateTime.Navigating to login");
                    NavigateToLogin();
                    return;
                }
                //Made by Jesper End


                if (DateTime.Now.CompareTo(DateTime.Parse(user.Properties["ExpiresIn"])) > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Access token expired : Re-authenticating");
                    var response = await _uIAuthenticationService.RefreshAccess();
                    if (!response.IsSuccesfull)
                    {
                        System.Diagnostics.Debug.WriteLine("Re-Authentication by refresh token failed");
                        //Fallback to password authentication -> Consider encapsulating a appwide
                        //Gracefull fallback re-authentication method which handles re-authenticating
                        //with refresh-token, then password token.
                        System.Diagnostics.Debug.WriteLine("Fall back to password re-authentication");
                        var result = await _uIAuthenticationService.Login(user.Username, user.Properties["Password"]);
                        if (!result.IsSuccesfull)
                        {
                            //Failed to-reauthenticate user with all re-authentication mechanism navigate to login
                            System.Diagnostics.Debug.WriteLine("All re-authentication mechanism failed");
                            System.Diagnostics.Debug.WriteLine(response.Message);
                            NavigateToLogin();
                            return;
                        }

                        System.Diagnostics.Debug.WriteLine("Succesfully re-authenticated with password");
                        //The password authenticatio succeded
                        NavigateToApp();
                        return;

                    }
                    System.Diagnostics.Debug.WriteLine("Token has been renewed, authenticating user to app access");
                    NavigateToApp();
                    return;
                }

                System.Diagnostics.Debug.WriteLine("User allready loged in and no reason to re-authenticate");
                //The user still has access to the Application
                NavigateToApp();
                return;
            }

            //There is no current user
            NavigateToLogin();
        }


        private async Task<BaseResponse> RefreshAccess()
        {
            return await _uIAuthenticationService.RefreshAccess();
        }

        private void NavigateToApp()
        {
            _eventCollector.TrackPageView("LoggedIn");
            _uIAuthenticationService.RegisterInstallationId();
            var containerId = App.NavigationContainerManager.CreateMainContainer();
            CoreMethods.SwitchOutRootNavigation(containerId);
        }

        private void NavigateToLogin()
        {
            _eventCollector.TrackPageView("Login");
            CoreMethods.SwitchOutRootNavigation(PageContainerIdentifiers.AuthenticationContainer);
        }
    }
}
