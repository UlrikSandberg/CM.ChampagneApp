using System;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.TastingPages.CreateTastingPage
{
    public partial class CreateTastingPage : BaseContentPage
    {
        public CreateTastingPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            
            InitializeComponent();
            Content.TranslationY = -(App.DisplaySettings.Width * 0.45);
            Content.Margin = new Thickness(0, 0, 0, -(App.DisplaySettings.Width * 0.45));
        }

        void Handle_OnReviewEntered(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            var ViewModel = (CreateTastingPageModel)this.BindingContext;
            if(ViewModel.ReviewEntered.CanExecute(args.TextEntered))
            {
                ViewModel.ReviewEntered.Execute(args.TextEntered);
            }
        }

        void Handle_OnCurrencyPicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.CurrencyPickedEventArgs args)
        {
            var ViewModel = (CreateTastingPageModel)this.BindingContext;
            if (ViewModel.CurrencySelected.CanExecute(args.ChosenCurrency))
            {
                ViewModel.CurrencySelected.Execute(args.ChosenCurrency);
            }
        }

        void Handle_OnPriceEntered(object sender, CM.ChampagneApp.UI.CustomEventArgs.PriceEnteredEventArgs args)
        {
            var ViewModel = (CreateTastingPageModel)this.BindingContext;
            if (ViewModel.PriceEntered.CanExecute(args.EnteredPrice))
            {
                ViewModel.PriceEntered.Execute(args.EnteredPrice);
            }
        }

        void Handle_OnDeleteClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (CreateTastingPageModel)this.BindingContext;
            if (ViewModel.DeleteReview.CanExecute(null))
            {
                ViewModel.DeleteReview.Execute(null);
            }
        }

        void Handle_OnChosenRating(object sender, CM.ChampagneApp.UI.CustomEventArgs.SelectedRatingEventArgs args)
        {
            var ViewModel = (CreateTastingPageModel)this.BindingContext;
            if(ViewModel.RatingChanged.CanExecute(args))
            {
                ViewModel.RatingChanged.Execute(args);
            }
        }

		void Handle_OnDidDisappear(object sender, System.EventArgs e)
		{
            var viewModel = GetBindingContext();

            if(viewModel == null)
            {
                return;
            }

			if(viewModel.UploadingIndicatorDissappeared.CanExecute(null))
			{
				viewModel.UploadingIndicatorDissappeared.Execute(null);
			}
		}

        private CreateTastingPageModel GetBindingContext()
        {
            if(this.BindingContext == null)
            {
                return null;
            }

            return (CreateTastingPageModel)this.BindingContext;
        }
    }
}
