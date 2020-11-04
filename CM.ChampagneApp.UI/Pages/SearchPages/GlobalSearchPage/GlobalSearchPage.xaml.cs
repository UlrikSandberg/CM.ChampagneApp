using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.Elements.Helpers.ElementEnum;
using CM.ChampagneApp.UI.UIFacade.Models.UIBrandModels;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.SearchPages.GlobalSearchPage
{
    public partial class GlobalSearchPage : ContentPage
    {
        private GlobalSearchPageModel _viewModel;

        public GlobalSearchPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            if(App.IsOnIphoneX)
            {
                TopStack.Padding = new Thickness(0, 20, 0, 0);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _viewModel = (GlobalSearchPageModel)this.BindingContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void Handle_OnDelayedTextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if(_viewModel.StartSearch.CanExecute(e.NewTextValue))
            {
                _viewModel.StartSearch.Execute(e.NewTextValue);
            }
        }

        public void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if(_viewModel.TextDidChange.CanExecute(e.NewTextValue))
            {
                _viewModel.TextDidChange.Execute(e.NewTextValue);
            }
        }

        void Handle_OnSegmentSelected(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0:
                    CarouselView.Position = 0;
                    EnableFilterButton(false);
                    break;
                case 1:
                    CarouselView.Position = 1;
                    EnableFilterButton(true);
                    break;
                case 2:
                    CarouselView.Position = 2;
                    EnableFilterButton(false);
                    break;
            }
        }

        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0:
                    SegmentControl.SetSelectedTabIndex(0);
                    EnableFilterButton(false);
                    break;
                case 1:
                    SegmentControl.SetSelectedTabIndex(1);
                    EnableFilterButton(true);
                    break;
                case 2:
                    SegmentControl.SetSelectedTabIndex(2);
                    EnableFilterButton(false);
                    break;
            }

            if(_viewModel.PageChanged.CanExecute(e.NewValue))
            {
                _viewModel.PageChanged.Execute(e.NewValue);
            }
        }

        void Handle_OnChampagneItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var champagne = (UIChampagneSearchModel)e.Item;

            if(_viewModel.ChampagneSelected.CanExecute(champagne.Id))
            {
                _viewModel.ChampagneSelected.Execute(champagne.Id);
            }
        }

        void Handle_OnChampagneRequestNextPage(object sender, System.EventArgs e)
        {
            if(_viewModel.ChampagneRequestNextPage.CanExecute(null))
            {
                _viewModel.ChampagneRequestNextPage.Execute(null);
            }
        }

        void Handle_OnChampagneCurrentRenderedObjectChanged(object sender, CM.ChampagneApp.UI.Elements.Lists.InfiniteListView.PherificalObjects visibleObjects)
        {
            var appear = (UIChampagneSearchModel)visibleObjects.DidAppear;
            var dissappear = (UIChampagneSearchModel)visibleObjects.DidDissappear;

            if(visibleObjects.ScrollDirection.Equals(ScrollDirection.Up))
            {
                if(_viewModel.TopVisibleChampagne.CanExecute(appear))
                {
                    _viewModel.TopVisibleChampagne.Execute(appear);
                }
            }
            else
            {
                if(_viewModel.TopVisibleChampagne.CanExecute(dissappear))
                {
                    _viewModel.TopVisibleChampagne.Execute(dissappear);
                }
            }
        }

        void Handle_OnReconnect(object sender, System.EventArgs e)
        {
            if(_viewModel.ChampagneReconnect.CanExecute(null))
            {
                _viewModel.ChampagneReconnect.Execute(null);
            }
        }

        void Handle_OnBrandItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var brand = (UIBrandSearchModel)e.Item;

            if(_viewModel.BrandSelected.CanExecute(brand.Id))
            {
                _viewModel.BrandSelected.Execute(brand.Id);
            }
        }

        void Handle_OnBrandRequestNextPage(object sender, System.EventArgs e)
        {
            if(_viewModel.BrandRequestNextPage.CanExecute(null))
            {
                _viewModel.BrandRequestNextPage.Execute(null);
            }
        }

        void Handle_OnBrandCurrentRenderedObjectChanged(object sender, CM.ChampagneApp.UI.Elements.Lists.InfiniteListView.PherificalObjects visibleObjects)
        {
            var appear = (UIBrandSearchModel)visibleObjects.DidAppear;
            var dissappear = (UIBrandSearchModel)visibleObjects.DidDissappear;

            if(visibleObjects.ScrollDirection.Equals(ScrollDirection.Up))
            {
                if(_viewModel.TopVisibleBrand.CanExecute(appear))
                {
                    _viewModel.TopVisibleBrand.Execute(appear);
                }
            }
            else
            {
                if(_viewModel.TopVisibleBrand.CanExecute(dissappear))
                {
                    _viewModel.TopVisibleBrand.Execute(dissappear);
                }
            }
        }

        void Handle_OnRequestNextPage(object sender, System.EventArgs e)
        {
            if(_viewModel.UserRequestNextPage.CanExecute(null))
            {
                _viewModel.UserRequestNextPage.Execute(null);
            }
        }

        void Handle_OnUserItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            var model = (UIUserSearchModel)e.Item;

            if(_viewModel.UserSelected.CanExecute(model.Id))
            {
                _viewModel.UserSelected.Execute(model.Id);
            }
        }

        void Handle_OnUserReconnect(object sender, System.EventArgs e)
        {
            if(_viewModel.UserReconnect.CanExecute(null))
            {
                _viewModel.UserReconnect.Execute(null);
            }
        }

        private void EnableFilterButton(bool isVisible)
        {
            FilterButtonContainer.IsVisible = isVisible;
        }
    }
}
