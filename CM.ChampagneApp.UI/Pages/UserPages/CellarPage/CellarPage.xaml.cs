using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.UserPages.CellarPage
{
    public partial class CellarPage : ContentPage
    {
        private CellarPageModel _viewModel;

        public CellarPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _viewModel = this.BindingContext as CellarPageModel;
        }

        void Handle_OnChosenChampagne(object sender, ModelClickedEventArgs<UIUserCellarChampagneModel> args)
        {
            if(_viewModel.Champagne_ProfileSelected.CanExecute(args.SelectedModel))
            {
                _viewModel.Champagne_ProfileSelected.Execute(args.SelectedModel);
            }
        }

        void Handle_OnChosenPersonalNotes(object sender, ModelClickedEventArgs<UIUserCellarChampagneModel> args)
        {
           if(_viewModel.Champagne_PersonalNotesSelected.CanExecute(args.SelectedModel))
            {
                _viewModel.Champagne_PersonalNotesSelected.Execute(args.SelectedModel);
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
