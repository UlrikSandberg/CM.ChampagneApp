using System;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Pages.BrandPages.BrandListPage
{
    public partial class BrandListPage : ContentPage
    {
        public BrandListPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        void Handle_OnBrandChosen(object sender, CM.ChampagneApp.UI.CustomEventArgs.BrandChosenEventArgs args)
        {
            var ViewModel = (BrandListPageModel)this.BindingContext;
            if(ViewModel.BrandChosen.CanExecute(args.BrandId))
            {
                ViewModel.BrandChosen.Execute(args.BrandId);
            }
        }

		void Handle_OnSubmissionEntered(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
		{
            var viewModel = (BrandListPageModel)this.BindingContext;
			if(viewModel.SubmissionEntered.CanExecute(args.TextEntered))
			{
                SuccesIndicator.IsVisible = true;
				viewModel.SubmissionEntered.Execute(args.TextEntered);
			}
		}

		void Handle_OnTryAgain(object sender, System.EventArgs e)
		{
            var viewModel = GetBindingContext();
            if (viewModel != null)
            {
                if (viewModel.Reconnect.CanExecute(null))
                {
                    viewModel.Reconnect.Execute(null);
                }
            }
		}
        void Handle_OnDidDisappear(object sender, System.EventArgs e)
        {
            SuccesIndicator.IsVisible = false;

            //Clear for text
        }

        private BrandListPageModel GetBindingContext()
        {
            if(this.BindingContext != null)
            {
                return (BrandListPageModel)this.BindingContext;
            }
            return null;
        }

        void Handle_OnItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var viewModel = (BrandListPageModel)this.BindingContext;

            if(viewModel != null)
            {
                var brand = (UIBrand)e.Item;

                if(viewModel.BrandChosen.CanExecute(brand.Id))
                {
                    viewModel.BrandChosen.Execute(brand.Id);
                }
            }
        }
    }
}
