using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.Pages.FollowingFollowersPages.FollowingPage;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.FollowingFollowersPages.FollowingPage
{
    public partial class FollowingPage : ContentPage
    {
        private FollowingPageModel _viewModel;

        public FollowingPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as FollowingPageModel;
        }

        void Handle_OnCardClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.FollowClickedEventArgs args)
        {
            if(_viewModel.CardClicked.CanExecute(args.FollowModel))
            {
                _viewModel.CardClicked.Execute(args.FollowModel);
            }
        }

        void Handle_OnFollowButtonClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.FollowClickedEventArgs args)
        {
            if(_viewModel.FollowButtonClicked.CanExecute(args.FollowModel))
            {
                _viewModel.FollowButtonClicked.Execute(args.FollowModel);
            }
        }

        void Handle_OnSegmentSelected(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0:
                    CarouselView.Position = 0;
                    break;
                case 1:
                    CarouselView.Position = 1;
                    break;
                case 2:
                    CarouselView.Position = 2;
                    break;
            }
        }

        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0:
                    SegmentControl.SetSelectedTabIndex(0);
                    break;
                case 1:
                    SegmentControl.SetSelectedTabIndex(1);
                    break;
                case 2:
                    SegmentControl.SetSelectedTabIndex(2);
                    break;
            }

            if (_viewModel.PageSelected.CanExecute(e.NewValue))
            {
                _viewModel.PageSelected.Execute(e.NewValue);
            }
        }

        void Handle_OnEmptyStateClicked(object sender, System.EventArgs e)
        {
            if(_viewModel.EmptyStateButtonClicked.CanExecute(null))
            {
                _viewModel.EmptyStateButtonClicked.Execute(null);
            }
        }
    }
}
