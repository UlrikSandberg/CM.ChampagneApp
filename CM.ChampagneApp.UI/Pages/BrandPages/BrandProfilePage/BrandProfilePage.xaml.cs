using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.BrandPages.BrandProfilePage
{
    public partial class BrandProfilePage : BaseContentPage
    {
        public BrandProfilePage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
           
            InitializeComponent();

            Content.TranslationY = -((App.DisplaySettings.Width - 40) / 6) - 20;
            Content.Margin = new Thickness(0, 0, 0, -((App.DisplaySettings.Width - 40) / 6) - 20);

        }

        void Handle_OnSocialButtonClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.SocialLinkEventArgs args)
        {
            var ViewModel = (BrandProfilePageModel)this.BindingContext;
            if(ViewModel.SocialIconClicked.CanExecute(args.URL))
            {
                ViewModel.SocialIconClicked.Execute(args.URL);
            }
        }

        void Handle_OnItemClicked(object sender, LinkPageEventArgs args)
        {
            var ViewModel = (BrandProfilePageModel)this.BindingContext;
            if(ViewModel.ClickedPage.CanExecute(args.SelectedPageLink))
            {
                ViewModel.ClickedPage.Execute(args.SelectedPageLink);
            }
        }
    }
}
