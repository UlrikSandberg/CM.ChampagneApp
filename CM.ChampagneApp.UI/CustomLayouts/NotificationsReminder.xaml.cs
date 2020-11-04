using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.CustomLayouts
{
    public partial class NotificationsReminder : ContentView
    {
        public NotificationsReminder()
        {
            InitializeComponent();
        }

        public ICommand AllowNotificationsCommand
		{
			get { return (ICommand)GetValue(AllowNotificationsCommandProperty); }
			set { SetValue(AllowNotificationsCommandProperty, value); }
		}

		public static BindableProperty AllowNotificationsCommandProperty =
			BindableProperty.Create(nameof(AllowNotificationsCommand), typeof(ICommand), typeof(NotificationsReminder));

		public ICommand NotNowCommand
        {
			get { return (ICommand)GetValue(NotNowCommandProperty); }
			set { SetValue(NotNowCommandProperty, value); }
        }

		public static BindableProperty NotNowCommandProperty =
            BindableProperty.Create(nameof(NotNowCommand), typeof(ICommand), typeof(NotificationsReminder));

		public ICommand PrivacyPolicyCommand
        {
			get { return (ICommand)GetValue(PrivacyPolicyCommandProperty); }
			set { SetValue(PrivacyPolicyCommandProperty, value); }
        }

		public static BindableProperty PrivacyPolicyCommandProperty =
			BindableProperty.Create(nameof(PrivacyPolicyCommand), typeof(ICommand), typeof(NotificationsReminder));
    }
}
