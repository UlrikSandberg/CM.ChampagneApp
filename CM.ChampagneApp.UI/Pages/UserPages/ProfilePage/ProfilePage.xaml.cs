using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.UserPages.ProfilePage
{
    public partial class ProfilePage : BaseContentPage
    {
        private ProfilePageModel _viewModel;

        public ProfilePage()
        {
            InitializeComponent();

            Content.TranslationY = -((App.DisplaySettings.Width - 40) / 6) - 20;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as ProfilePageModel;
        }

        void Handle_OnItemClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.LinkPageEventArgs args)
        {
            if(_viewModel != null)
            {
                if(_viewModel.PageSelected.CanExecute(args.SelectedPageLink))
                {
                    _viewModel.PageSelected.Execute(args.SelectedPageLink);
                }
            }
        }
    }
}
