using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.UserPages.ChooseSubscriptionPage
{
    public partial class ChooseSubscriptionPage : ContentPage
    {
        public ChooseSubscriptionPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
		}      

        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
		{
			var viewModel = (ChooseSubscriptionPageModel)this.BindingContext;
			if(viewModel.NavigateBack.CanExecute(null))
			{
				viewModel.NavigateBack.Execute(null);
			}
		}

		void Handle_OnSegmentSelected(object sender, Xamarin.Forms.ValueChangedEventArgs e)
		{
			var viewModel = (ChooseSubscriptionPageModel)this.BindingContext;
			if(viewModel.SegmentChanged.CanExecute(e.NewValue))
			{
				viewModel.SegmentChanged.Execute(e.NewValue);
			}
		}
    }
}
