using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.Discover
{
    public partial class DiscoverPage : ContentPage
    {
        private DiscoverPageModel _viewModel;

        public DiscoverPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();

            if(App.IsOnIphoneX)
            {
                Navbar.Padding = new Thickness(0, 20, 0,0);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as DiscoverPageModel;
        }

        protected override void OnAppearing()
		{
			base.OnAppearing();
		}

		void Handle_OnVintageArchiveChampagneClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ChampagneEditionCardClickedEventArgs args)
		{
			if(_viewModel.VintageArchiveItemClicked.CanExecute(args.ChampagneId))
			{
				_viewModel.VintageArchiveItemClicked.Execute(args.ChampagneId);
			}
        }

        void Handle_OnChosenChampagne(object sender, CM.ChampagneApp.UI.CustomEventArgs.ChampagneClickedEventArgs args)
        {
            if (_viewModel.ClickedChampagne.CanExecute(args.SelectedChampagne))
                _viewModel.ClickedChampagne.Execute(args.SelectedChampagne);
        }
    }
}
