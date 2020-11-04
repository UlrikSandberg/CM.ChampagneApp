using System;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.SharedPages.BaseInitPage
{
	public partial class BaseInitPage : ContentPage
    {
        public BaseInitPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

        }
              
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			var viewModel = (BaseInitPageModel)this.BindingContext;
            if (viewModel.ValidateUser.CanExecute(null))
            {
                viewModel.ValidateUser.Execute(null);
            }
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			var viewModel = (BaseInitPageModel)this.BindingContext;
            if (viewModel.ValidateUser.CanExecute(null))
            {
                viewModel.ValidateUser.Execute(null);
            }
		}
    }
}
