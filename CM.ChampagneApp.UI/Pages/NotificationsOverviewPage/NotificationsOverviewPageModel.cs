using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Helpers.Routing;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.SharedPages.Helpers;
using CM.ChampagneApp.UI.Pages.SharedPages.WebViewPage;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.PushNotificationsSettingsPage;
using CM.ChampagneApp.UI.UIFacade.Models.UINotifications;
using CM.ChampagneApp.UI.UIFacade.Services;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.NotificationsOverviewPage
{
    public class NotificationsOverviewPageModel : BasePageModel
	{      
		//Toggle of the notification popup should store a string value of type "True", "False";
		private const string NotifiationPopUp = "NotificationPopUpToggle";
		private const string SubscriptionKey = "SetNotificationBadge";
      
		public NotificationReceivedMessage NotificationReceivedWhileSleeping { get; set; } 

		public string Text { get; set; }

		public PagingManager PageManager { get; set; }

		public ObservableCollection<UINotification> Notifications { get; set; }

        //Notification popup
        public ICommand AllowNotifications { get; set; }
		public ICommand NotificationsNotNow { get; set; }
		public ICommand PrivacyPolicy { get; set; }
		public ICommand ViewDidAppear { get; set; }
		public ICommand ViewDidDissapear { get; set; }

		//Notification list
		public ICommand NotificationTapped { get; set; }

		public ICommand Refresh { get; set; }
		public ICommand ScrollToButtom { get; set; }

		private readonly IUINotificationService notificationService;

		public bool IsNotificationsReminderVisible { get; set; } = true;
		private bool DidLaunch { get; set; } = true;
		private bool IsOnScreen { get; set; } = false;

		private readonly IPageRouter pageRouter;

        public NotificationsOverviewPageModel(IUINotificationService notificationService, IPageRouter pageRouter, IEventCollector ec) : base(ec)
        {
			this.pageRouter = pageRouter;
			this.notificationService = notificationService;

			AllowNotifications = new Command(async () => await Handle_AllowNotifications());
			NotificationsNotNow = new Command(Handle_NotNow);
			PrivacyPolicy = new Command(async () => await Handle_PrivacyPolicy());
			NotificationTapped = new Command<UINotification>(async (x) => await Handle_NotificationTapped(x));
			ScrollToButtom = new Command<UINotification>(async (x) => await Handle_ScrolledToButtom(x));
			Refresh = new Command(async () => await OnRefresh());
			ViewDidAppear = new Command(async () => await Handle_ViewDidAppear());
			ViewDidDissapear = new Command(Handle_ViewDidDissapear);

			PageManager = new PagingManager(30);

			//Check if the user has been represented with notification pop-up screen
			if (Application.Current.Properties.ContainsKey(NotifiationPopUp))//If the notificationToggle has been saved read the value
			{
				var value = Application.Current.Properties[NotifiationPopUp];

				var booleanValue = (bool)value;

				IsNotificationsReminderVisible = booleanValue;
			}
			else //First time page launch store the new value
			{
                Application.Current.Properties.Add(NotifiationPopUp, true);
			}

			//Subscribe to messagingCenter to get information on the start up action
			//MessagingCenter.Subscribe<TabBarBadgeService, NotificationBadgeCountMessage>(this, MessagingCenterSubscriptionKeys.SetNotificationBadge, (page, badgeCountMessage) => {
			MessagingCenter.Subscribe<object>(this, MessagingCenterSubscriptionKeys.AppOnActivated, async (page) =>
			{
				await AppActivated();
			});

		    MessagingCenter.Subscribe<object, NotificationReceivedMessage>(this, MessagingCenterSubscriptionKeys.NotificationReceivedKey, async (page, notification) =>
			{
				await Handle_NotificationReceived(notification);
			});         
		}

        public void UnsubscribeAll()
		{
			MessagingCenter.Unsubscribe<object>(this, MessagingCenterSubscriptionKeys.NotificationReceivedKey);
			MessagingCenter.Unsubscribe<object>(this, MessagingCenterSubscriptionKeys.AppOnActivated);         
		}
            
        //Call to get a number of unread notifications newer than the last newest notification seen in app
		private async Task UpdateNotificationsBadgeCounterAsync()
		{
			if(UpdateNotificationInProgress)
			{
				System.Diagnostics.Debug.WriteLine("UpdateNotificationBadgeCounterAsync() --> Aborted because call is already in progress");
                return;
			}

			UpdateNotificationInProgress = true;

			var result = await notificationService.GetLatestNotificationUpdates(false, Guid.Empty);

			if(result != null)
			{
				App.TabbarBadgeService.SetBadge(result.NumberOfUnreadNotifications);
			}
			await Task.Delay(1000);
			UpdateNotificationInProgress = false;
		}

		//Use to get a count and result of new notifications newer than the current newest notification, defined by id.
		private bool UpdateNotificationInProgress = false;
        private async Task UpdateNotificationsBadgeCounterWithResultAsync(Guid id)
		{
			if(UpdateNotificationInProgress)
			{
				System.Diagnostics.Debug.WriteLine("UpdateNotificationBadgeCounterWithResultAsync(Guid id) --> Aborted because call is already in progress");
				return;
			}

			UpdateNotificationInProgress = true;

			if(id.Equals(Guid.Empty))
			{
				return;
			}
            
            var result = await notificationService.GetLatestNotificationUpdates(true, id);

            if (result != null)
            {
                foreach (var notification in result.NewNotifications)
                {
                    Notifications.Insert(0, notification);
                }
                App.TabbarBadgeService.IncrementBadgeValue(result.NumberOfUnreadNotifications);
            }
			await Task.Delay(5000);
			UpdateNotificationInProgress = false;
		}

        //When a notification is received and the app is either already open or has been launched from the notification this will get called
		private async Task Handle_NotificationReceived(NotificationReceivedMessage notification)
		{
            //If the app is launched from notification
            if(!notification.IsRunning)
			{
				await pageRouter.RouteToPath(notification.ContextUrl, CoreMethods);
				if(Notifications != null)
                {
                    if(Notifications.Any())
                    {
						//Opdater markAsRead først...
						Guid id = Guid.Empty;
						var result = Guid.TryParse(notification.NotificationId, out id);
						if(result && !id.Equals(Guid.Empty))
						{
							await notificationService.MarkNotificationAsRead(id);
						}

                        //The app has been launched from notification -> This mean that the App.OnActivated has been called as well.
                        //Consider to have logic that prevent double loading.
                    }
                }
				System.Diagnostics.Debug.WriteLine("App opened from notifications --> Get result");
			}
			else //If app is already open when notification is received or has been running in background
			{
                //Download the new notification if there has been downloaded any, there should since the app is activated. If there is not they will have to reload anyway or they might be out of service and it doesn't matter anyway'
				if(Notifications != null)
				{
					if(Notifications.Any())
					{
                        //Also consider that if the app has been re-opened from background App.OnActivated will be called as well..
						await UpdateNotificationsBadgeCounterWithResultAsync(Notifications.First().Id);
					}
				}

				System.Diagnostics.Debug.WriteLine("Notification received while open --> Count + result");
			}
		}

		private async Task Handle_ViewDidAppear()
		{
			IsOnScreen = true;
            if(App.TabbarBadgeService != null)
			{
				if (!IsNotificationsReminderVisible)
				{
					App.TabbarBadgeService.ClearBadge();
				}
			}

			//Inform backend that you have been notified about notifications up untill the latest in your list
			if (Notifications != null)
			{
				if (Notifications.Any())
				{
					var result = await notificationService.MarkLatestNotificationSeen(Notifications.First().Id);
				}
			}
		}

		private void Handle_ViewDidDissapear()
		{
			IsOnScreen = false;
		}

		private bool AppActivatedCalled = false;
		private async Task AppActivated()
		{
			if(AppActivatedCalled)
			{
                System.Diagnostics.Debug.WriteLine("CM.ChampagneApp.UI.PageModels.NotificationsOverviewPageModel.cs --> AppActivated() -> Aborted since it has already been called");
				return;
			}
			AppActivatedCalled = true;
			System.Diagnostics.Debug.WriteLine("CM.ChampagneApp.UI.PageModels.NotificationsOverviewPageModel.cs --> AppActivated()");
                        
			if (DidLaunch)
			{
				DidLaunch = false;
				await UpdateNotificationsBadgeCounterAsync();            
				System.Diagnostics.Debug.WriteLine(this.GetHashCode() +" :App launch --> Get count of new notifications");
			}
			else
			{
				if (Notifications != null)
				{
					if (Notifications.Any())
					{

						var noti = Notifications.First();
						await UpdateNotificationsBadgeCounterWithResultAsync(noti.Id);
					}
				}
				System.Diagnostics.Debug.WriteLine(this.GetHashCode() + " :App reopened --> Get count and result of new notifications from the last notification in app...");
			}
			await Task.Delay(5000);
			AppActivatedCalled = false;
		}
            
		public override void Init(object initData)
		{
			base.Init(initData);

			if(initData != null)
			{
				var data = (NotificationsOverviewInitData)initData;
				if(data.IsSignUpOrLogin)
				{
                    UpdateNotificationsBadgeCounterAsync().RunForget();
					System.Diagnostics.Debug.WriteLine("Handle user signUp");
                    System.Diagnostics.Debug.WriteLine("User signup or login --> Get count");
				}
			}
            DownloadInitNotifications().RunForget();
		}

		protected override async Task OnReconnect()
		{
			await base.OnReconnect();

			IsOutOfService = false;
			if (!PageManager.IsRefreshingInterrupted)
			{
				PageManager.IsInitialLoadInProgress = true;
				await DownloadInitNotifications();
			}
			else
			{
				PageManager.RollBack();
			}
		}

		private async Task DownloadInitNotifications()
		{
			PageManager.IsInitialLoadInProgress = true;
			PageManager.IsLoadingEntities = true;
			if(NotificationReceivedWhileSleeping != null)
			{
				await pageRouter.RouteToPath(NotificationReceivedWhileSleeping.ContextUrl, CoreMethods);
				Guid id = Guid.Empty;
				var parsingResult = Guid.TryParse(NotificationReceivedWhileSleeping.NotificationId, out id);
                if (parsingResult && !id.Equals(Guid.Empty))
                {
                    await notificationService.MarkNotificationAsRead(id);
                }
				NotificationReceivedWhileSleeping = null;
			}
			//UpdateNotifications(false, DateTime.Now);
			var result = await notificationService.GetNotificationsPagedAsync(PageManager.Page, PageManager.PageSize);

			if (result == null)
			{
				PageManager.AllEntitiesHasBeenDownloaded = false;
				PageManager.IsLoadingEntities = false;
				PageManager.IsInitialLoadInProgress = false;
				IsOutOfService = true;

				if (PageManager.IsRefreshing)
				{
					PageManager.IsRefreshing = false;
					PageManager.IsRefreshingInterrupted = true;
				}
			}
			else
			{
				PageManager.IncrementPage();
				PageManager.IsInitialLoadInProgress = false;
				PageManager.IsLoadingEntities = false;

				Notifications = new ObservableCollection<UINotification>(result);

				if (PageManager.IsRefreshing)
				{
					PageManager.IsRefreshing = false;

					if (Notifications != null)
					{
						if (Notifications.Any())
						{
							await notificationService.MarkLatestNotificationSeen(Notifications.First().Id);
						}
					}
				}
			}
		}

		private async Task Handle_ScrolledToButtom(UINotification notification)
		{
			var entityIndex = Notifications.IndexOf(notification);

			if (entityIndex >= 0)
			{
				if (Notifications.Count - 1 == entityIndex)
				{
					//Should request new download since we reach the end of the list.
					await DownloadNotificationsAsync();
				}
			}
		}

		private async Task DownloadNotificationsAsync()
		{
			PageManager.IsOutOfServiceTextVisible = false;
			if (PageManager.AllEntitiesHasBeenDownloaded || PageManager.IsLoadingEntities)
			{
				return;
			}

			PageManager.IsLoadingEntities = true;

			var notifications = await notificationService.GetNotificationsPagedAsync(PageManager.Page, PageManager.PageSize);

			if (notifications == null)
			{
				PageManager.AllEntitiesHasBeenDownloaded = false;
				PageManager.IsLoadingEntities = false;
				PageManager.IsOutOfServiceTextVisible = true;
				return;
			}

			PageManager.IncrementPage();

			if (!notifications.Any())
			{
				PageManager.AllEntitiesHasBeenDownloaded = true;
				PageManager.IsLoadingEntities = false;
				return;
			}

			PageManager.IsLoadingEntities = false;

			foreach (var notification in notifications)
			{
				Notifications.Add(notification);
			}
		}

		private async Task Handle_AllowNotifications()
		{
            Application.Current.Properties[NotifiationPopUp] = false;
			IsNotificationsReminderVisible = false;
			await CoreMethods.PushPageModel<PushNotificationsSettingsPageModel>();
		}

		private void Handle_NotNow()
		{
            Application.Current.Properties[NotifiationPopUp] = false;
			IsNotificationsReminderVisible = false;
		}

		private async Task Handle_PrivacyPolicy()
		{
			await CoreMethods.PushPageModel<WebViewPageModel>(new WebViewInitData("https://champagnemoments.eu/privacy-policy-app/?utm_source=Champagne%20App&utm_medium=AppLink&utm_campaign=AppLink"));
		}

		private async Task Handle_NotificationTapped(UINotification notification)
		{
			//Set notification isRead to true and inform backend that notification has been read.
            if (!notification.IsRead)
			{
				notification.IsRead = true;
				//Evaluate the notification context and navigate user accordingly
				await pageRouter.RouteToPath(notification.ContextUrl, CoreMethods);
				var response = await notificationService.MarkNotificationAsRead(notification.Id);

				if (!response.IsSuccesfull)
				{
					notification.IsRead = false;
				}
			}
			else
			{
				//Evaluate the notification context and navigate user accordingly
				await pageRouter.RouteToPath(notification.ContextUrl, CoreMethods);
			}
		}

		private async Task OnRefresh()
		{
			PageManager.IsRefreshing = true;
			PageManager.AllEntitiesHasBeenDownloaded = false;
			PageManager.SetPage(0);
			await DownloadInitNotifications();
		}     
	}
}