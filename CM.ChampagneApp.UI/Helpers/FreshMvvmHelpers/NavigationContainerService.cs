using System;
using CM.ChampagneApp.UI.Dependency;
using Xamarin.Forms;
using System.Collections.Generic;
using CM.ChampagneApp.UI.Pages.Discover;
using CM.ChampagneApp.UI.Pages.BrandPages.BrandListPage;
using CM.ChampagneApp.UI.Pages.SearchPages.GlobalSearchPage;
using CM.ChampagneApp.UI.Pages.NotificationsOverviewPage;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper;

[assembly: Dependency(typeof(CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers.NavigationContainerService))]
namespace CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers
{
	public class NavigationContainerService : INavigationContainerService
    {
		private TabbedNavigation currentTabbedNavigation = null;
		private Guid currentNavigationContainerId = Guid.Empty;

        public NavigationContainerService()
        {
        }

		public void DisposeMainContainer()
		{
			var list = currentTabbedNavigation.TabbedPages;

			var tabs = new List<Page>();

            foreach(var tab in list)
			{
				tabs.Add(tab);
			}

			var notificationPageModel = tabs[3].BindingContext;

			var pageModel = (NotificationsOverviewPageModel)notificationPageModel;
            pageModel.UnsubscribeAll();
                     
			currentTabbedNavigation = null;
            currentNavigationContainerId = Guid.Empty;
		}

		string INavigationContainerService.CreateMainContainer(bool isSignUpOrLogin)
		{
			var containerId = Guid.NewGuid();
			          
			currentTabbedNavigation = new TabbedNavigation(containerId.ToString());

			currentTabbedNavigation.AddTab<DiscoverPageModel>("Discover", "Home");
            currentTabbedNavigation.AddTab<BrandListPageModel>("Brands", "TabBarBrands");
            currentTabbedNavigation.AddTab<GlobalSearchPageModel>("Find", "TabBarSearch");
            currentTabbedNavigation.AddTab<NotificationsOverviewPageModel>("Notifications", "AlertIcon", new NotificationsOverviewInitData(isSignUpOrLogin));
            currentTabbedNavigation.AddTab<ProfilePageModel>("Profile", "User", new ProfilePageInitData(Guid.Empty,true));

            currentNavigationContainerId = containerId;

			return containerId.ToString();
		}
	}
}
