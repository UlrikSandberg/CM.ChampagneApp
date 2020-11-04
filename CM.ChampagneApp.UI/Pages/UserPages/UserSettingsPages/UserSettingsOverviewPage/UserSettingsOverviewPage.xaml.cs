using System;
using CM.ChampagneApp.UI.Elements.Buttons;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIReadModels;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.Helpers;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.UserSettingsOverviewPage
{
    public partial class UserSettingsOverviewPage : ContentPage
    {
        public UserSettingsOverviewPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

        }



        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
        {
            var PageModel = (UserSettingsOverviewPageModel)this.BindingContext;
            if(PageModel.NavigateBack.CanExecute(null))
            {
                PageModel.NavigateBack.Execute(null);
            }
        }

        void Handle_OnSocialButtonClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.SocialLinkEventArgs args)
        {
            var ViewModel = (UserSettingsOverviewPageModel)this.BindingContext;
            if(ViewModel.SocialBtnClicked.CanExecute(args.URL))
            {
                ViewModel.SocialBtnClicked.Execute(args.URL);
            }
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var ChosenItem = (SettingsListModel)e.Item;

            if (ChosenItem.IsUrl)
            {
                var ViewModel = (UserSettingsOverviewPageModel)this.BindingContext;
                if (ViewModel.SocialBtnClicked.CanExecute(ChosenItem.Url))
                {
                    ViewModel.SocialBtnClicked.Execute(ChosenItem.Url);
                }
            }
            else
            {
                if (ChosenItem.Command != null)
                {
                    if (ChosenItem.Command.CanExecute(null))
                    {
                        ChosenItem.Command.Execute(null);
                    }
                }
            }
        }
    }
}
