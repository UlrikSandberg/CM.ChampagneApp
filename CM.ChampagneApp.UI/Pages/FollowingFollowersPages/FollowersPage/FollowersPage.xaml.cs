using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.FollowingFollowersPages.FollowersPage
{
    public partial class FollowersPage : ContentPage
    {
        private FollowersPageModel _viewModel;

        public FollowersPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as FollowersPageModel;
        }

        void Handle_OnCardClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.FollowClickedEventArgs args)
        {
            if(_viewModel != null)
            {
                if(_viewModel.CardClicked.CanExecute(args.FollowModel))
                {
                    _viewModel.CardClicked.Execute(args.FollowModel);
                }
            }
        }

        void Handle_OnFollowButtonClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.FollowClickedEventArgs args)
        {
            if(_viewModel != null)
            {
                if(_viewModel.FollowButtonClicked.CanExecute(args.FollowModel))
                {
                    _viewModel.FollowButtonClicked.Execute(args.FollowModel);
                }
            }
        }
    }
}
