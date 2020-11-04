using System;
using CM.ChampagneApp.DTO.Models.POSTModels;
using CM.ChampagneApp.UI;
using CM.ChampagneApp.UI.Dependency;
using IdentityModel.Client;
using Xamarin.Auth;
using Xamarin.Forms;


[assembly: Dependency(typeof(CM.ChampagneApp.iOS.IOSImplementations.IOSAuthService))]
namespace CM.ChampagneApp.iOS.IOSImplementations
{
	public class IOSAuthService : IAccountService
    {
        public IOSAuthService()
        {
        }
        //App account store identifiers
		private const string AccountStoreAppName = "CMApp";
		private const string AccountStoreDeviceInstallationId = "ASDeviceInstallationId";

        //Properties identifiers
		private const string DeviceInstallationProperty = "DeviceInstallationId";
        
		public string GetDeviceInstallationId()
		{
			var deviceAccounts = AccountStore.Create().FindAccountsForService(AccountStoreDeviceInstallationId);

			Account deviceInstallationAccount = null;

			foreach(var acc in deviceAccounts)
			{
				deviceInstallationAccount = acc;
				break;
			}

			if(deviceInstallationAccount != null)
			{
				return deviceInstallationAccount.Properties[DeviceInstallationProperty];
			}
			return null;
		}

		public void RegisterDeviceInstallationId(string installationId)
		{
			var currentAccounts = AccountStore.Create().FindAccountsForService(AccountStoreDeviceInstallationId);

			Account deviceInstallationAccount = null;

			foreach(var acc in currentAccounts)
			{
				deviceInstallationAccount = acc;
				break;
			}

            //Existing deviceInstallationAccount exist replace
			if(deviceInstallationAccount != null)
			{
                //Replace installationId
				deviceInstallationAccount.Properties.Remove(DeviceInstallationProperty);
				deviceInstallationAccount.Properties.Add(DeviceInstallationProperty, installationId);

				//Save changes
				AccountStore.Create().Save(deviceInstallationAccount, AccountStoreDeviceInstallationId);
 
			}
			else //No existing deviceInstallationAccount exist
			{
                //Create new account
				deviceInstallationAccount = new Account();

                //Add device installationProperty
				deviceInstallationAccount.Properties.Add(DeviceInstallationProperty, installationId);
                //Save new account
				AccountStore.Create().Save(deviceInstallationAccount, AccountStoreDeviceInstallationId);
			}         
		}

		public void SaveCredentials(string email, string password, TokenResponse token)
		{
			var account = new Account
			{
				Username = email
			};

			account.Properties.Add("Password", password);

			if (token.AccessToken != null)
			{
				account.Properties.Add("AccessToken", token.AccessToken);
			}
			else
			{
				account.Properties.Add("AccessToken", "");
			}

			if (token.RefreshToken != null)
            {
                account.Properties.Add("RefreshToken", token.RefreshToken);
            }
            else
            {
                account.Properties.Add("RefreshToken", "");
            }

            if (token.IdentityToken != null)
            {
                account.Properties.Add("IdentityToken", token.IdentityToken);
            }
            else
            {
                account.Properties.Add("IdentityToken", "");
            }

			account.Properties.Add("IsLoggedIn", "true");

			account.Properties.Add("ExpiresIn", DateTime.Now.AddSeconds(token.ExpiresIn).ToString());


            //Clear account store before adding new
			ClearAccountStore(AccountStoreAppName);
			AccountStore.Create().Save(account, AccountStoreAppName);

		}

		public void SaveCredentialsFromInitToken(string email, string password, CreateUserResponseModel token)
		{
         
			var account = new Account
            {
                Username = email
            };

            account.Properties.Add("Password", password);

            if (token.AccessToken != null)
            {
                account.Properties.Add("AccessToken", token.AccessToken);
            }
            else
            {
                account.Properties.Add("AccessToken", "");
            }

            if (token.RefreshToken != null)
            {
                account.Properties.Add("RefreshToken", token.RefreshToken);
            }
            else
            {
                account.Properties.Add("RefreshToken", "");
            }

            if (token.IdentityToken != null)
            {
                account.Properties.Add("IdentityToken", token.IdentityToken);
            }
            else
            {
                account.Properties.Add("IdentityToken", "");
            }

            account.Properties.Add("IsLoggedIn", "true");

            account.Properties.Add("ExpiresIn", DateTime.Now.AddSeconds(token.ExpiresIn).ToString());

            //Clear account store before saving new account
			ClearAccountStore(AccountStoreAppName);
			AccountStore.Create().Save(account, AccountStoreAppName);
		}

		public void UpdateTokens(TokenResponse token)
		{

			//Find current user
            var currentAccounts = AccountStore.Create().FindAccountsForService("CMApp");

            Account account = null;
            foreach (Account acc in currentAccounts)
            {
                account = acc;
                break;
            }
            if (account == null)
            {
				return;
            }

			account.Properties.Remove("AccessToken");
            account.Properties.Remove("RefreshToken");
            account.Properties.Remove("IdentityToken");
            account.Properties.Remove("ExpiresIn");

            if (token.AccessToken != null)
            {
                account.Properties.Add("AccessToken", token.AccessToken);
            }
            else
            {
                account.Properties.Add("AccessToken", "");
            }

            if (token.RefreshToken != null)
            {
                account.Properties.Add("RefreshToken", token.RefreshToken);
            }
            else
            {
                account.Properties.Add("RefreshToken", "");
            }

            if (token.IdentityToken != null)
            {
                account.Properties.Add("IdentityToken", token.IdentityToken);
            }
            else
            {
                account.Properties.Add("IdentityToken", "");
            }

            account.Properties.Add("ExpiresIn", DateTime.Now.AddSeconds(token.ExpiresIn).ToString());
            
            AccountStore.Create().Save(account, "CMApp");
		}


		public void SignOut()
		{
			//Find current user
			var currentAccounts = AccountStore.Create().FindAccountsForService("CMApp");

			Account account = null;
			foreach(Account acc in currentAccounts)
			{
				account = acc;
				break;
			}
			if (account != null)
			{
				account.Properties.Remove("IsLoggedIn");
				account.Properties.Add("IsLoggedIn", "false");
			}

			AccountStore.Create().Save(account, "CMApp");
		}

        public string Username()
		{
			var currentAccounts = AccountStore.Create().FindAccountsForService("CMApp");

            foreach (Account acc in currentAccounts)
            {
				return acc.Username;
            }

            return null;
		}

        public void SetUserProfileCreated(bool IsCreated)
		{
			var currentAccounts = AccountStore.Create().FindAccountsForService("CMApp");

			Account account = null;
            foreach(Account acc in currentAccounts)
			{
				account = acc;
				break;
			}

            if(account != null)
			{
                account.Properties.Remove("IsUserProfileCreated");
				account.Properties.Add("IsUserProfileCreated", IsCreated.ToString());
			}

			AccountStore.Create().Save(account, "CMApp");
		}
       

        public Account CurrentUser()
		{
			var currentAccounts = AccountStore.Create().FindAccountsForService("CMApp");

            foreach(Account acc in currentAccounts)
			{
				return acc;
			}

			return null;
		}

        public void DeleteCurrentUser()
		{
			var currentAccount = AccountStore.Create().FindAccountsForService("CMApp");

			foreach (Account acc in currentAccount)
            {
				AccountStore.Create().Delete(acc, "CMApp");
				return;
            }         
		}

        private void ClearAccountStore(string appName)
        {
			var accounts = AccountStore.Create().FindAccountsForService(appName);

            foreach(Account acc in accounts)
            {
                AccountStore.Create().Delete(acc, appName);            
            }
        }

		public void UpdateEmail(string email)
		{
			var currentAccounts = AccountStore.Create().FindAccountsForService("CMApp");

            Account account = null;
            foreach (Account acc in currentAccounts)
            {
                account = acc;
                break;
            }

            if (account != null)
            {
				account.Username = email;
            }

            AccountStore.Create().Save(account, "CMApp");
		}
	}
}
