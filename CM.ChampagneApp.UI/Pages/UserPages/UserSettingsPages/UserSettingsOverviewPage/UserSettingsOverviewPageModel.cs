using System;
using System.Collections.Generic;
using System.Windows.Input;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.UIReadModels;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Caching;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.BugAndFeedbackPage;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.PushNotificationsSettingsPage;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.EmailNotificationsSettingsPage;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.ManageSubscriptionPage;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.ChangePasswordPage;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.ChangeEmailPage;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.EditProfilePage;
using CM.ChampagneApp.UI.Pages.SharedPages.WebViewPage;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.Helpers;
using CM.ChampagneApp.UI.Pages.SharedPages.Helpers;
using CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.UserSettingsOverviewPage
{
	public class UserSettingsOverviewPageModel : BasePageModel
    {
        public ICommand SocialBtnClicked { get; set; }

        public IList<SettingsGroup> groupedSettings { get; set; }

		private readonly IUIAuthenticationService authenticationService;
		private readonly IUIUserDataService userDataService;
        private readonly ICache cache;

        public UserSettingsOverviewPageModel(IUIAuthenticationService authenticationService, IUIUserDataService userDataService, IEventCollector ec, ICache cache) : base(ec)
        {
            this.userDataService = userDataService;
            this.cache = cache;
            this.authenticationService = authenticationService;
			NavigateBack = new Command(async () => await OnNavigateBack());
            SocialBtnClicked = new Command<string>(async (x) => await OnSocialBtnClicked(x));


            var list = new List<SettingsGroup>();

            //***** Section 1 *****
            var AccountSettings = new SettingsGroup("Account", true);
            var Account1 = new SettingsListModel
            {
                Title = "Edit Profile",
                IsUrl = false,
                Command = new Command(async () => await NavigateToEditProfile())
            };
            AccountSettings.Add(Account1);
            var Account2 = new SettingsListModel
            {
                Title = "Manage Password",
                IsUrl = false,
                Command = new Command(async () => await NavigateToChangePassword())
                    
            };
            AccountSettings.Add(Account2);
			var account3 = new SettingsListModel
			{
				Title = "Manage subscription",
				IsUrl = false,
				Command = new Command(async () => await NavigateToManageSubscription())
			};
			//AccountSettings.Add(account3);
			var account4 = new SettingsListModel
			{
				Title = "Manage Email",
				IsUrl = false,
				Command = new Command(async () => await NavigateToChangeEmail())
			};
			AccountSettings.Add(account4);

            //***** Section 1 END *****

            //***** Section 2 START *****

			var notificationSection = new SettingsGroup("Notification", true);
			var notification1 = new SettingsListModel
			{
				Title = "Push Notifications",
				IsUrl = false,
				Command = new Command(async () => await NavigateToPushNotifications())
			};
			notificationSection.Add(notification1);

			var notification2 = new SettingsListModel
			{
				Title = "Email Notifications",
				IsUrl = false,
				Command = new Command(async () => await NavigateToEmailNotifications())
			};
			notificationSection.Add(notification2);


            //***** Section 2 END *****

            //***** Section 3 START *****

            var CompanySection = new SettingsGroup("Company", true);
            var Company1 = new SettingsListModel
            {
                Title = "About",
                IsUrl = true,
                Url = "https://champagnemoments.eu/?utm_source=Champagne%20App&utm_medium=AppLink&utm_campaign=AppLink",
                Command = null
            };
            CompanySection.Add(Company1);
            var Company2 = new SettingsListModel
            {
                Title = "Report a bug or give feedback",
                IsUrl = false,
				Command = new Command(async () => await NavigateToReportBugAndGiveFeedback()),
                Url = null
            };
            CompanySection.Add(Company2);
            /*var Company3 = new SettingsListModel
            {
                Title = "App Survey",
                IsUrl = true,
                Url = "file:///Users/ulriksandberg/websites/AlgoVisualization/www.cs.usfca.edu/_galles/visualization/Algorithms.html",
                Command = null
            };
            CompanySection.Add(Company3);*/
            var Company4 = new SettingsListModel
            {
                Title = "FAQ",
                IsUrl = true,
                Url = "https://champagnemoments.eu/faq?utm_source=Champagne%20App&utm_medium=AppLink&utm_campaign=AppLink",
                Command = null
            };
            CompanySection.Add(Company4);
            var Company5 = new SettingsListModel
            {
                Title = "Terms Of Use",
                IsUrl = true,
                Url = "https://champagnemoments.eu/terms-of-use-app/?utm_source=Champagne%20App&utm_medium=AppLink&utm_campaign=AppLink",
                Command = null
            };
            CompanySection.Add(Company5);
            var company6 = new SettingsListModel
            {
                Title = "Privacy Policy",
                IsUrl = true,
                Url = "https://champagnemoments.eu/privacy-policy-app/?utm_source=Champagne%20App&utm_medium=AppLink&utm_campaign=AppLink",
                Command = null
            };
            CompanySection.Add(company6);

            //***** Section 3 END *****

            //***** Section 4 START *****

            var SignOutSection = new SettingsGroup(null, false);
            var SignOut1 = new SettingsListModel
            {
                Title = "Sign out",
                IsUrl = false,
                Command = new Command(OnSignOut)

            };
            SignOutSection.Add(SignOut1);


            //***** Section 4 END

            list.Add(AccountSettings);
			list.Add(notificationSection);
            list.Add(CompanySection);
            list.Add(SignOutSection);
            groupedSettings = list;

			DownloadCurrentUserSettings().RunForget();
         
        }

		protected override async Task Reload()
		{
			await base.Reload();

			await DownloadCurrentUserSettings();
		}

		private async Task DownloadCurrentUserSettings()
		{
			var result = await userDataService.GetCurrentUserSetting();

			if(result != null)
			{
				if(!result.IsEmailVerified)
				{
					var notificationSection = groupedSettings[0];
					var emailNotifications = notificationSection[2];
					emailNotifications.IsBadgeVisible = true;
					emailNotifications.BadgeValue = "1";
				}
				else
				{
					var notificationSection = groupedSettings[0];
                    var emailNotifications = notificationSection[2];
                    emailNotifications.IsBadgeVisible = false;
                    emailNotifications.BadgeValue = "1";
				}
			}
		}

        private async Task OnSocialBtnClicked(string name)
        {
            var webViewInitData = new WebViewInitData(name);
            await CoreMethods.PushPageModel<WebViewPageModel>(webViewInitData);
        }
      
        //Do not change the method to type Task nor async ***WARNING return type must be void***
        //Changing to Task makes the authentication container bug. Where the buttons are not shown
        //***** If this is changed makes sure to test it out --> SignOut and confirm that the buttons are not gone *****
        private void OnSignOut()
        {
            cache.EmptyAll();
			authenticationService.DeregisterInstallationId();
            //Sign out user
			App.AccountService.SignOut();         
		    CoreMethods.PopToRoot(false);
            CoreMethods.SwitchOutRootNavigation(PageContainerIdentifiers.AuthenticationContainer);
			App.NavigationContainerManager.DisposeMainContainer();
        }

        private async Task NavigateToEditProfile()
        {
            await CoreMethods.PushPageModel<EditProfilePageModel>();
        }

        private async Task NavigateToReportBugAndGiveFeedback()
		{
			await CoreMethods.PushPageModel<BugAndFeedbackPageModel>();
		}

        private async Task NavigateToPushNotifications()
		{
			await CoreMethods.PushPageModel<PushNotificationsSettingsPageModel>();
		}

        private async Task NavigateToEmailNotifications()
		{
			await CoreMethods.PushPageModel<EmailNotificationsSettingsPageModel>();
		}

        private async Task NavigateToManageSubscription()
		{
			await CoreMethods.PushPageModel<ManageSubscriptionPageModel>();
		}

        private async Task NavigateToChangePassword()
        {
            await CoreMethods.PushPageModel<ChangePasswordPageModel>();
        }

        private async Task NavigateToChangeEmail()
		{
			await CoreMethods.PushPageModel<ChangeEmailPageModel>();
		}
    }
}
