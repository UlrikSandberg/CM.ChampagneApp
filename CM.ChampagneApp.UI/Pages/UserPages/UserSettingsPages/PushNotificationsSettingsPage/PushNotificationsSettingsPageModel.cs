using System;
using FreshMvvm;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using System.Windows.Input;
using Xamarin.Forms;
using CM.ChampagneApp.DTO.Builders;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Helpers;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.PushNotificationsSettingsPage
{
	public class PushNotificationsSettingsPageModel : BasePageModel
    {      
		public UIUserSettings UserSettings { get; set; } 
        
		public bool NotifyFollower { get; set; }
		public bool NotifyComment { get; set; }
		public bool NotifyActivityInThread { get; set; }
		public bool NotifyLikeTasting { get; set; }
		public bool NotifyLikeComment { get; set; }
		public bool ReceiveCMNotifications { get; set; }
		public bool NotifyChampagneTasted { get; set; }
		public bool NotifyBrandNews { get; set; }

		public ICommand DoneEditing { get; set; }
		public ICommand DoneUploading { get; set; }

		public ICommand CMNotifications { get; set; }
		public ICommand NewFollower { get; set; }
		public ICommand NewComment { get; set; }
		public ICommand ActivityInThread { get; set; }
		public ICommand LikeTasting { get; set; }
		public ICommand LikeComment { get; set; }
		public ICommand ChampagneTasted { get; set; }
		public ICommand BrandNews { get; set; }

		private readonly IUIUserDataService userDataService;
      
		public PushNotificationsSettingsPageModel(IUIUserDataService userDataService, IEventCollector ec) : base(ec)
        {
            this.userDataService = userDataService;         
			DoneEditing = new Command(async () => await OnDoneEditing());

			CMNotifications = new Command<bool>(OnCMNotifications);
			NewFollower = new Command<bool>(OnNewFollower);
			NewComment = new Command<bool>(OnNewComment);
			ActivityInThread = new Command<bool>(OnActivityInThread);
			LikeTasting = new Command<bool>(OnLikeTasting);
			LikeComment = new Command<bool>(OnLikeComment);
			ChampagneTasted = new Command<bool>(OnChampagneTasted);
			BrandNews = new Command<bool>(OnBrandNews);

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

            await base.OnReconnect();
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

				ReceiveCMNotifications = UserSettings.NotificationSettings.ReceiveCMNotifications;
				NotifyComment = UserSettings.NotificationSettings.NotifyNewComment;
				NotifyFollower = UserSettings.NotificationSettings.NotifyNewFollower;
				NotifyLikeComment = UserSettings.NotificationSettings.NotifyLikeComment;
				NotifyLikeTasting = UserSettings.NotificationSettings.NotifyLikeTasting;
				NotifyActivityInThread = UserSettings.NotificationSettings.NotifyActivityInThread;
				NotifyChampagneTasted = UserSettings.NotificationSettings.NotifyChampagneTasted;
				NotifyBrandNews = UserSettings.NotificationSettings.NotifyBrandNews;
            }       

			IsDownloadingUserSettings = false;
		} 

        private void OnCMNotifications(bool value)
		{
			ReceiveCMNotifications = value;
		}

        private void OnNewFollower(bool value)
		{
			NotifyFollower = value;
		}

        private void OnNewComment(bool value)
		{
			NotifyComment = value;
		}

        private void OnActivityInThread(bool value)
		{
			NotifyActivityInThread = value;
		}

        private void OnLikeTasting(bool value)
		{
			NotifyLikeTasting = value;
		}

        private void OnLikeComment(bool value)
		{
			NotifyLikeComment = value;
		} 

        private void OnChampagneTasted(bool value)
		{
			NotifyChampagneTasted = value;
		}

        private void OnBrandNews(bool value)
		{
			NotifyBrandNews = value;
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
            if(IsUploadingUserSettings || UserSettings == null)
			{
				return;
			}

			//First check if notifcation settings has changed
			var didChange = (((false || !UserSettings.NotificationSettings.ReceiveCMNotifications.Equals(ReceiveCMNotifications)) 
                                || !UserSettings.NotificationSettings.NotifyNewFollower.Equals(NotifyFollower)) 
                                || !UserSettings.NotificationSettings.NotifyNewComment.Equals(NotifyComment)) 
                                || !UserSettings.NotificationSettings.NotifyActivityInThread.Equals(NotifyActivityInThread);

			didChange |= !UserSettings.NotificationSettings.NotifyLikeTasting.Equals(NotifyLikeTasting);
			didChange |= !UserSettings.NotificationSettings.NotifyLikeComment.Equals(NotifyLikeComment);
			didChange |= !UserSettings.NotificationSettings.NotifyChampagneTasted.Equals(NotifyChampagneTasted);
			didChange |= !UserSettings.NotificationSettings.NotifyBrandNews.Equals(NotifyBrandNews);

			if(didChange)
			{
				IsUploadingUserSettings = true;
				IsUploadingIndicatorVisible = true;

				var updateBuilder = new UserNotificationsSettingsUpdate.UserNotificationsSettingsUpdateBuilder();

				updateBuilder
					.SetReceiveNewLetter(UserSettings.NotificationSettings.ReceiveNewsLetter)
					.SetReceiveCMNotifications(ReceiveCMNotifications)
					.SetNotifyNewFollower(NotifyFollower)
					.SetNotifyNewComment(NotifyComment)
					.SetNotifyActivityInThread(NotifyActivityInThread)
					.SetNotifyLikeTasting(NotifyLikeTasting)
					.SetNotifyLikeComment(NotifyLikeComment)
					.SetNotifyBrandNews(NotifyBrandNews)
					.SetNotifyChampagneTasted(NotifyChampagneTasted);

                //Start animation!

				var response = await userDataService.UpdateUserNotificationSettings(updateBuilder.Build());

				if(!response.IsSuccesfull)
				{
					//Animate error
					await CoreMethods.DisplayAlert("Error: ", response.Message + "\n Try Again", "OK");
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
	}
}
