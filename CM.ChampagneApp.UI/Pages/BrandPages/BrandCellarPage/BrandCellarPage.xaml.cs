using System;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.BrandPages.BrandCellarPage
{
    public partial class BrandCellarPage : BaseContentPage
    {
        private BrandCellarPageModel _viewModel;

        public BrandCellarPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as BrandCellarPageModel;
        }

		void Handle_OnChampagneClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ChampagneClickedEventArgs args)
		{
            if (_viewModel.ChampagneClicked.CanExecute(args.SelectedChampagne))
                _viewModel.ChampagneClicked.Execute(args.SelectedChampagne);
        }

        void Handle_OnVintageArchiveClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ChampagneEditionCardClickedEventArgs args)
        {
            if (_viewModel.VintageArchiveClicked.CanExecute(args.ChampagneId))
                _viewModel.VintageArchiveClicked.Execute(args.ChampagneId);
        }
    }
}
