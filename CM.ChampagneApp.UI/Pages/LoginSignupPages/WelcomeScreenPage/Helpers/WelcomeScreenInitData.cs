using System;
namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.WelcomeScreenPage.Helpers
{
    public class WelcomeScreenInitData
    {
        public string Email { get; private set; }
		public string Username { get; private set; }
		public string Password { get; private set; }

		public WelcomeScreenInitData(string email, string username, string password)
        {
            Password = password;
			Username = username;
			Email = email;
		}
    }
}
