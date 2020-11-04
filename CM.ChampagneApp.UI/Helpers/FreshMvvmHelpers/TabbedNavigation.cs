using System;
using FreshMvvm;

using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers
{
	public class TabbedNavigation : FreshTabbedNavigationContainer
    {
		public TabbedNavigation(string navigationServiceName) : base(navigationServiceName)
		{
		}
      
		protected override Page CreateContainerPage(Page page)
		{
			var container = new NavigationPage(page);
			container.BarTextColor = Color.White;

			return container;
		}
	}
}

