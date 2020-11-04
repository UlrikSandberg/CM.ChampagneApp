using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using CM.ChampagneApp.UI.Pages.SearchPages.FilterSearchPage;
using CM.ChampagneApp.UI.CustomEventArgs;

namespace CM.ChampagneApp.UI.Pages.SearchPages.FilterSearchResultPage
{
    public partial class FilterSearchResultPage : ContentPage
    {
        private FilterSearchResultPageModel _viewModel;

        public FilterSearchResultPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as FilterSearchResultPageModel;
        }

        void Handle_OnChosenChampagne(object sender, ModelClickedEventArgs<UIUserCellarChampagneModel> args)
        {
            if(_viewModel.ChampagneChosen.CanExecute(args.SelectedModel))
            {
                _viewModel.ChampagneChosen.Execute(args.SelectedModel);
            }
        }

        void Handle_OnEmptyStateClicked(object sender, System.EventArgs e)
        {
            if(_viewModel.EmptyStateClicked.CanExecute(null))
            {
                _viewModel.EmptyStateClicked.Execute(null);
            }
        }

        void Handle_OnChosenPersonalNotes(object sender, ModelClickedEventArgs<UIUserCellarChampagneModel> args)
        {
            //This is not an option here
        }
    }
}
