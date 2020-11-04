using System;
using FreshMvvm;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using Xamarin.Forms;
using System.Windows.Input;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using CM.ChampagneApp.DTO.Builders;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Helpers;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.EmailNotificationsSettingsPage
{
	public class EmailNotificationsSettingsPageModel : BasePageModel
    {
		public UIUserSettings Settings { get; set; }

		public ICommand DoneEditing { get; set; }
		public ICommand DoneUploading { get; set; }

		public ICommand NewsLetterToggled { get; set; }

		public bool ReceiveNewsLetter { get; set; }

		private readonly IUIUserDataService userDataService;

		public EmailNotificationsSettingsPageModel(IUIUserDataService userDataService, IEventCollector ec) : base(ec)
        {
			this.userDataService = userDataService;
			DoneEditing = new Command(async () => await OnDoneEditing());
			NewsLetterToggled = new Command<bool>(OnNewsLetterToggled);
			DoneUploading = new Command(async () => await OnDoneUploading());
        }

		public override void Init(object initData)
		{
			base.Init(initData);
            DownloadUserSettings().RunForget();
		}

		protected override async Task OnReconnect()
		{
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
				Settings = result;
				ReceiveNewsLetter = Settings.NotificationSettings.ReceiveNewsLetter;
				IsDownloadingUserSettings = false;
			}
		}

		private async Task OnDoneUploading()
		{
			if(IsDoneUploadingUserSettingsWithSucces)
			{
				await CoreMethods.PopPageModel();
			}

			IsUploadingIndicatorVisible = false;
			IsUploadingUserSettings = false;
			IsDoneUploadingUserSettingsWithError = false;
			IsDoneUploadingUserSettingsWithSucces = false;
		}

		public bool IsUploadingIndicatorVisible { get; set; } = false;
        public bool IsUploadingUserSettings { get; set; } = false;
        public bool IsDoneUploadingUserSettingsWithSucces { get; set; } = false;
        public bool IsDoneUploadingUserSettingsWithError { get; set; } = false;
        private async Task OnDoneEditing()
		{
            if(Settings == null)
            {
                return;
            }

			if(!Settings.NotificationSettings.ReceiveNewsLetter.Equals(ReceiveNewsLetter))
			{
				IsUploadingUserSettings = true;
				IsUploadingIndicatorVisible = true;

				var updateBuilder = new UserNotificationsSettingsUpdate.UserNotificationsSettingsUpdateBuilder();

				updateBuilder
					.SetNotifyNewComment(Settings.NotificationSettings.NotifyNewComment)
					.SetReceiveNewLetter(ReceiveNewsLetter)
					.SetNotifyLikeComment(Settings.NotificationSettings.NotifyLikeComment)
					.SetNotifyLikeTasting(Settings.NotificationSettings.NotifyLikeTasting)
					.SetNotifyNewFollower(Settings.NotificationSettings.NotifyNewFollower)
					.SetNotifyActivityInThread(Settings.NotificationSettings.NotifyActivityInThread)
					.SetReceiveCMNotifications(Settings.NotificationSettings.ReceiveCMNotifications);

				var response = await userDataService.UpdateUserNotificationSettings(updateBuilder.Build());

				if(!response.IsSuccesfull)
				{
					IsDoneUploadingUserSettingsWithError = true;
				}
				else
				{
					IsDoneUploadingUserSettingsWithSucces = true;
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


        private void OnNewsLetterToggled(bool value)
		{
			ReceiveNewsLetter = value;
		}      
    }
}
