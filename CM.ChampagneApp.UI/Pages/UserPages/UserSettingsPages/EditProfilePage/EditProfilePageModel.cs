using System;
using System.Windows.Input;
//using CM.ChampagneApp.Business.Models;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.DTO.Builders;
using Xamarin.Forms;
using FreshMvvm;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Helpers;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.EditProfilePage
{
	public class EditProfilePageModel : BasePageModel
	{      
		public UIUserSettings UserSettings { get; set; }

		public ICommand EditingDone { get; set; }
		public ICommand ProfileImgSelected { get; set; }
		public ICommand CoverImgSelected { get; set; }
		public ICommand CellarImgSelected { get; set; }
		public ICommand NameEntered { get; set; }
		public ICommand ProfileNameEntered { get; set; }
		public ICommand CountryEntered { get; set; }

		public ICommand BiographyEntered { get; set; }

		public ICommand UploadingIndicatorDissappeared { get; set; }

		private readonly IUIUserDataService userDataService;
		private readonly IBusinessAccountService bussAuthService;

		//Data fields
		private string EditedName { get; set; }
		private string EditedProfileName { get; set; }
		private string EditedBiography { get; set; }

		private byte[] ProfileImage { get; set; }
		private bool ShouldChangeProfileImage { get; set; }
		private byte[] ProfileCoverImage { get; set; }
		private bool ShouldChangeProfileCoverImage { get; set; }
		private byte[] CellarCardImage { get; set; }
		private bool ShouldChangeCellarCardImage { get; set; }

		private  IUICreateUserService createUserService;

		public EditProfilePageModel(IUIUserDataService userDataService, IBusinessAccountService bussAuthService, IUICreateUserService createUserService, IEventCollector ec) : base(ec)
        {
			this.createUserService = createUserService;
			this.bussAuthService = bussAuthService;
			this.userDataService = userDataService;
			EditingDone = new Command(async () => await OnEditingDone());
			ProfileImgSelected = new Command<byte[]>(OnProfileImgSelected);
			CoverImgSelected = new Command<byte[]>(OnCoverImgSelected);
			CellarImgSelected = new Command<byte[]>(OnCellarImgSelected);

			NameEntered = new Command<string>(OnNameEntered);
			ProfileNameEntered = new Command<string>(OnProfileNameEntered);
			CountryEntered = new Command<string>(OnCountryEntered);
			BiographyEntered = new Command<string>(OnBiographyEntered);

			UploadingIndicatorDissappeared = new Command(async () => await OnUploadingIndicatorDissAppeared());

            DownloadUserSettings().RunForget();
        }

		protected override async Task OnReconnect()
		{
			await base.OnReconnect();

			IsOutOfService = false;
			await DownloadUserSettings();
		}

		public bool IsDownloadingUserSettings { get; set; } = true;
		private async Task DownloadUserSettings()
		{
			IsDownloadingUserSettings = true;
			var result = await userDataService.GetCurrentUserSetting();
			if(result == null)
			{
				IsOutOfService = true;
			}
			else
			{
				UserSettings = result;
			}
			IsDownloadingUserSettings = false;
		}
      
		protected async override Task OnNavigateBack()
		{
			if (!IsUploadingUserSettings)
            {
                await CoreMethods.PopPageModel();
            }
		}

		private async Task OnEditingDone()
		{
		    await UpdateUserSettings();
		}

		private void OnProfileImgSelected(byte[] image)
		{
			ProfileImage = image;
			ShouldChangeProfileImage = true;
		}

		private void OnCoverImgSelected(byte[] image)
		{
			ProfileCoverImage = image;
			ShouldChangeProfileCoverImage = true;
		}

        private void OnCellarImgSelected(byte[] image)
		{
			CellarCardImage = image;
			ShouldChangeCellarCardImage = true;
		}

		private void OnNameEntered(string name)
		{
			EditedName = name;
		}

		private void OnProfileNameEntered(string profileName)
		{
			EditedProfileName = profileName;
		}

		private void OnCountryEntered(string country)
		{
			System.Diagnostics.Debug.WriteLine("user entered the following Country: " + country);
		}

		private void OnBiographyEntered(string biography)
		{
			EditedBiography = biography;
		}

        private async Task OnUploadingIndicatorDissAppeared()
		{
			if(IsDoneUploadingUserSettingsWithSucces)
			{
				await CoreMethods.PopPageModel();
			}
			IsUploadingUserSettings = false;
			IsUploadingIndicatorVisible = false;
			IsDoneUploadingUserSettingsWithError = false;
			IsDoneUploadingUserSettingsWithSucces = false;
		}

		public bool IsUploadingIndicatorVisible { get; set; } = false;
		public bool IsUploadingUserSettings { get; set; } = false;
		public bool IsDoneUploadingUserSettingsWithSucces { get; set; } = false;
		public bool IsDoneUploadingUserSettingsWithError { get; set; } = false;
		private async Task UpdateUserSettings()
		{
			var userSettingsBuilder = new UserSettingsUpdate.UserSettingsUpdateBuilder();

			if (!string.IsNullOrEmpty(EditedName))
			{
				if (UserSettings.Name == null)
				{
					userSettingsBuilder.SetName(EditedName);
				}
				else if (!EditedName.Equals(UserSettings.Name))
				{
					userSettingsBuilder.SetName(EditedName);
				}

			}

			if (!string.IsNullOrEmpty(EditedProfileName))
			{
				if (UserSettings.ProfileName == null)
				{
					userSettingsBuilder.SetProfileName(EditedProfileName);
				}
				else if (!EditedProfileName.Equals(UserSettings.ProfileName))
				{
					userSettingsBuilder.SetProfileName(EditedProfileName);
				}
			}

			if (!string.IsNullOrEmpty(EditedBiography))
			{
				if (UserSettings.Biography == null)
				{
					userSettingsBuilder.SetBiography(EditedBiography);
				}
				else if (!EditedBiography.Equals(UserSettings.Biography))
				{
					userSettingsBuilder.SetBiography(EditedBiography);
				}
			}

			if (ShouldChangeProfileImage)
			{
				userSettingsBuilder.SetProfileImage(ProfileImage);
			}

			if (ShouldChangeProfileCoverImage)
			{
				userSettingsBuilder.SetProfileCover(ProfileCoverImage);
			}

			if(ShouldChangeCellarCardImage)
			{
				userSettingsBuilder.SetPersonalCellarCardImg(CellarCardImage);
			}

			var userSettingsUpdate = userSettingsBuilder.Build();

			var userSettingsShouldUpdate = false;

			foreach (var property in userSettingsUpdate.GetType().GetProperties())
			{
				if (property.GetValue(userSettingsUpdate) != null)
				{
					userSettingsShouldUpdate = true;
				}
			}

			if(userSettingsShouldUpdate)
			{
				if (userSettingsUpdate.Name != null)
                {
                    if (!createUserService.ValidateName(userSettingsUpdate.Name))
                    {
						await CoreMethods.DisplayAlert("Error: ", "Username may not contain any special characters", "OK");
						return; 
                    }
                }

				IsUploadingIndicatorVisible = true;
				IsUploadingUserSettings = true;
            
				var response = await userDataService.UpdateUserSettings(userSettingsUpdate);

				if (!response.IsSuccesfull)
				{
					IsDoneUploadingUserSettingsWithError = true;
					await CoreMethods.DisplayAlert("Error: ", response.Message, "OK");               
				}
				else
				{
					IsDoneUploadingUserSettingsWithSucces = true;

					ProfileImage = null;
					ShouldChangeProfileImage = false;
					ProfileCoverImage = null;
					ShouldChangeProfileCoverImage = false;
					CellarCardImage = null;
					ShouldChangeCellarCardImage = false;

					if(userSettingsUpdate.Name != null)
					{
						UserSettings.Name = userSettingsUpdate.Name;
					}
					if(userSettingsUpdate.ProfileName != null)
					{
						UserSettings.ProfileName = userSettingsUpdate.ProfileName;
					}
					if(userSettingsUpdate.Biography != null)
					{
						UserSettings.Biography = userSettingsUpdate.Biography;
					}               
				}
			}
            else
            {
                IsUploadingUserSettings = true;
                IsUploadingIndicatorVisible = true;

                await Task.Delay(200);

                IsDoneUploadingUserSettingsWithSucces = true;
            }
        }
	}
}