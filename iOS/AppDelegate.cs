using System;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using FFImageLoading.Transformations;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Lottie.Forms.iOS.Renderers;
using CarouselView.FormsPlugin.iOS;
using System.Threading.Tasks;
using UserNotifications;
using CM.ChampagneApp.UI.Helpers;
using Microsoft.AppCenter.Distribute;
using Microsoft.AppCenter.Crashes;
using CM.ChampagneApp.iOS.IOSImplementations;

namespace CM.ChampagneApp.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
		NSDictionary launchOptions;
		private bool isRunning = false;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //Logging everything to appcenter: https://forums.xamarin.com/discussion/91543/catch-all-exceptions-to-avoid-crash
            AppDomain.CurrentDomain.UnhandledException += async (sender, e) =>
            {
                Crashes.TrackError((Exception)e.ExceptionObject, new Dictionary<string, string> { { "sender", sender.GetType().FullName } });
                Console.WriteLine(((Exception)e.ExceptionObject).Message);
            };

            TaskScheduler.UnobservedTaskException += async (sender, e) => 
                {
                    Crashes.TrackError(e.Exception.Flatten(), new Dictionary<string, string> { { "sender", sender.GetType().FullName } });
                    Console.WriteLine(e.Exception.Message + " - " + e.Exception.StackTrace); 
                };

            //Generate hardware specifics
            var iosDevice = new iOSDevice();
            App.IsOnIphoneX = iosDevice.deviceHasNotch();

            app.StatusBarStyle = UIStatusBarStyle.LightContent;

            global::Xamarin.Forms.Forms.Init();
            Distribute.DontCheckForUpdatesInDebug();

            AnimationViewRenderer.Init();
            CarouselView.FormsPlugin.iOS.CarouselViewRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            var ignore = new CircleTransformation();
            var ignore2 = new RoundedTransformation();

            LoadApplication(new App());

            var denimBlack = (Color.FromHex("#0B0E1d")).ToUIColor();
            var DarkGold = Color.FromHex("#A68F68").ToUIColor();
            UITabBar.Appearance.BarTintColor = denimBlack;         
            UITabBar.Appearance.SelectedImageTintColor = DarkGold;
			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            //Intial opening of app, request permission from user/settingsApp that the app is allowed to receive notifications         
            RequestPushPermissionAsync().RunForget();

			launchOptions = options;
			System.Diagnostics.Debug.WriteLine("Base.FinishedLaunching(app, options); --> *****....");
			isRunning = true;


            return base.FinishedLaunching(app, options);
        }

		public override void WillTerminate(UIApplication uiApplication)
		{
			base.WillTerminate(uiApplication);

			isRunning = false;
		}

		//public override [Export("applicationDidEnterBackground:")]
        public override void DidEnterBackground(UIApplication uiApplication)
		{
			isRunning = false;
		}

		public override void OnActivated(UIApplication uiApplication)
		{
			base.OnActivated(uiApplication);

			if(launchOptions != null && launchOptions.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
			{
				var notification = launchOptions[UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary;
				PresentNotification(notification, true);
			}
			launchOptions = null;

			//Inform subscribers that the app has been activated...
			isRunning = true;
			System.Diagnostics.Debug.WriteLine("CM.ChampagneApp.iOS.AppDelegate.cs -> OnActivated(UIApplication uiApplication)");
			MessagingCenter.Send<object>(this, MessagingCenterSubscriptionKeys.AppOnActivated);
		}

		//Initial opening of app request the user to approve that app for receiving notifications. If not inital
		// opening the app will read settings from the settingsApp
		async Task RequestPushPermissionAsync()
		{
			var requestResult = await UNUserNotificationCenter.Current.RequestAuthorizationAsync(
				UNAuthorizationOptions.Alert
				| UNAuthorizationOptions.Badge 
				| UNAuthorizationOptions.Sound);

            //Result 1 is the approved variable from pop up box 
			bool approved = requestResult.Item1;
			NSError error = requestResult.Item2;

            //Error if failure in iOS framework, or architectural failure. 
			if(error == null)
			{
                //If the user did not approve just return
				if(!approved)
				{
					Console.Write("Permission to receive notifications was not granted.");
					return;
				}

                //The user did approved read settings from settingsApp and validate that the app is still allowed to receive push notifications
				var currentSettings = await UNUserNotificationCenter.Current.GetNotificationSettingsAsync();
				if(currentSettings.AuthorizationStatus != UNAuthorizationStatus.Authorized)
				{
					System.Diagnostics.Debug.WriteLine("Permissions were requested in the past but have been revoked (-> settings app)");
                    return;
				}
                //The app is still allowed to receive push notifications, register for push-notifications
				Device.BeginInvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications);
			}
			else
			{
				Console.Write($"Error requesting permissions: {error}.");
			}
		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			if(deviceToken == null)
			{
				//Can happend if the user has restored their device...
				return;
			}

			//Register installationId in accountStore for use later in respect to the userId
            App.AccountService.RegisterDeviceInstallationId(deviceToken.ToString());
		}

		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{       
			System.Diagnostics.Debug.WriteLine($"Failed to register for remote notifications: {error.Description}");
		}

		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			System.Diagnostics.Debug.WriteLine("DidReceiveRemoteNotification() ---> ---> --->");
			PresentNotification(userInfo, isRunning);

			completionHandler(UIBackgroundFetchResult.NoData);
		}

		private void PresentNotification(NSDictionary dict, bool isOnActivated)
		{
			NSDictionary aps = dict.ObjectForKey(new NSString("aps")) as NSDictionary;

			var msg = string.Empty;
			var url = string.Empty;
			var id = string.Empty;
			if(aps.ContainsKey(new NSString("alert")))
			{
				msg = (aps[new NSString("alert")] as NSString).ToString();
			}

			if(string.IsNullOrEmpty(msg))
			{
				msg = "(msg unable to parse)";
			}

			if (aps.ContainsKey(new NSString("contexturl")))
            {
                url = (aps[new NSString("contexturl")] as NSString).ToString();
            }

            if (string.IsNullOrEmpty(url))
            {
                url = "(url unable to parse)";
            }

			if (aps.ContainsKey(new NSString("notificationId")))
            {
				id = (aps[new NSString("notificationId")] as NSString).ToString();
            }

            if (string.IsNullOrEmpty(url))
            {
                id = "(id unable to parse)";
            }

			MessagingCenter.Send<object, NotificationReceivedMessage>(this, MessagingCenterSubscriptionKeys.NotificationReceivedKey, new NotificationReceivedMessage(msg, url, id, isOnActivated));
		}

		public static CGColor ToCGColor(Color color)
        {
            return new CGColor(CGColorSpace.CreateSrgb(), new nfloat[] { (float)color.R, (float)color.G, (float)color.B, (float)color.A });
        }      
    }
}
