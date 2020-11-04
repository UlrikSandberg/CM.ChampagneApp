using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UINotifications;

namespace CM.ChampagneApp.UI.Pages.NotificationsOverviewPage
{
    public partial class NotificationsOverviewPage : ContentPage
    {
        public NotificationsOverviewPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            if(App.IsOnIphoneX)
            {
                NotificationsReminder.HorizontalOptions = LayoutOptions.Center;
                NotificationsReminder.VerticalOptions = LayoutOptions.Center;
            }
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			var viewModel = (NotificationsOverviewPageModel)this.BindingContext;
			if(viewModel.ViewDidAppear.CanExecute(null))
			{
				viewModel.ViewDidAppear.Execute(null);
			}
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			var viewModel = (NotificationsOverviewPageModel)this.BindingContext;
			if(viewModel.ViewDidDissapear.CanExecute(null))
			{
				viewModel.ViewDidDissapear.Execute(null);
			}
		}

		void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			var viewModel = (NotificationsOverviewPageModel)this.BindingContext;
			if(viewModel.NotificationTapped.CanExecute((UINotification)e.Item))
			{
				viewModel.NotificationTapped.Execute((UINotification)e.Item);
			}
		}      

        void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
		{
			var viewModel = (NotificationsOverviewPageModel)this.BindingContext;
			if(viewModel.ScrollToButtom.CanExecute((UINotification)e.Item))
			{
				viewModel.ScrollToButtom.Execute((UINotification)e.Item);
			}
		}
    }
}
