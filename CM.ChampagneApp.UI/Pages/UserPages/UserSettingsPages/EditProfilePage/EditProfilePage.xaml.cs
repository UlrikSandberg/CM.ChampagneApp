using System;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.EditProfilePage
{
    public partial class EditProfilePage : ContentPage
    {
        public EditProfilePage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();

        }

        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (EditProfilePageModel)this.BindingContext;
            if(ViewModel.NavigateBack.CanExecute(null))
            {
                ViewModel.NavigateBack.Execute(null);
            }
        }

        void Handle_OnRightNavIconClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (EditProfilePageModel)this.BindingContext;
            if(ViewModel.EditingDone.CanExecute(null))
            {
                ViewModel.EditingDone.Execute(null);
            }
        }

        void Handle_OnProfileImageChosen(object sender, CM.ChampagneApp.UI.CustomEventArgs.ImageChosenEventArgs args)
        {
            var ViewModel = (EditProfilePageModel)this.BindingContext;
            if(ViewModel.ProfileImgSelected.CanExecute(args.file))
            {
                ViewModel.ProfileImgSelected.Execute(args.file);
            }
        }

        void Handle_OnCoverImageChosen(object sender, CM.ChampagneApp.UI.CustomEventArgs.ImageChosenEventArgs args)
        {
            var ViewModel = (EditProfilePageModel)this.BindingContext;
            if (ViewModel.CoverImgSelected.CanExecute(args.file))
            {
                ViewModel.CoverImgSelected.Execute(args.file);
            }
        }

		void Handle_OnImageChosen(object sender, CM.ChampagneApp.UI.CustomEventArgs.ImageChosenEventArgs args)
		{
			var viewModel = (EditProfilePageModel)this.BindingContext;
			if(viewModel.CellarImgSelected.CanExecute(args.file))
			{
				viewModel.CellarImgSelected.Execute(args.file);
			}
		}

        void Handle_OnEntry1Entered(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            var ViewModel = (EditProfilePageModel)this.BindingContext;
            if(ViewModel.NameEntered.CanExecute(args.TextEntered))
            {
                ViewModel.NameEntered.Execute(args.TextEntered);
            }
        }

        void Handle_OnEntry2Entered(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            var ViewModel = (EditProfilePageModel)this.BindingContext;
            if (ViewModel.ProfileNameEntered.CanExecute(args.TextEntered))
            {
                ViewModel.ProfileNameEntered.Execute(args.TextEntered);
            }
        }

        void Handle_OnEntry3Entered(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            var ViewModel = (EditProfilePageModel)this.BindingContext;
            if (ViewModel.CountryEntered.CanExecute(args.TextEntered))
            {
                ViewModel.CountryEntered.Execute(args.TextEntered);
            }
        }

		void Handle_OnReviewEntered(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
		{
			var viewModel = (EditProfilePageModel)this.BindingContext;
			if(viewModel.BiographyEntered.CanExecute(args.TextEntered))
			{
				viewModel.BiographyEntered.Execute(args.TextEntered);
			}
		}

		void Handle_OnDidDisappear(object sender, System.EventArgs e)
		{
			var viewModel = (EditProfilePageModel)this.BindingContext;
			if(viewModel != null)
			{
				if(viewModel.UploadingIndicatorDissappeared != null)
				{
					if (viewModel.UploadingIndicatorDissappeared.CanExecute(null))
                    {
                        viewModel.UploadingIndicatorDissappeared.Execute(null);
                    }
				}
			}         
		}
    }
}
