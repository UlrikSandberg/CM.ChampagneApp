using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneLightroomPage
{
    public partial class ChampagneLightroomPage : ContentPage
    {
        public ChampagneLightroomPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

		void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
		{
			var viewModel = (ChampagneLightroomPageModel)this.BindingContext;
			if(viewModel.NavigateBack.CanExecute(null))
			{
				viewModel.NavigateBack.Execute(null);
			}
		}
    }
}
