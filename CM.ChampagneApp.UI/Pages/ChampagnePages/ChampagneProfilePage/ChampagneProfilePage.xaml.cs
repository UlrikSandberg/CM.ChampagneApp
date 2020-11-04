using System;
using System.Collections.Generic;
using System.ComponentModel;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;

namespace CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage
{
    public partial class ChampagneProfilePage : BaseContentPage
    {
        private ChampagneProfilePageModel _viewModel;

        public ChampagneProfilePage()
		{
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            //Offset 45% of screenwidth up the y-axis
            ContentStack.TranslationY = -(App.DisplaySettings.Width * 0.45);
            ContentStack.Margin = new Thickness(0, 0, 0, -(App.DisplaySettings.Width * 0.45));
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as ChampagneProfilePageModel;
        }

        void Handle_OnChosenRating(object sender, CM.ChampagneApp.UI.CustomEventArgs.SelectedRatingEventArgs args)
        {
            if(args.SelectedRating > 0)
            {
                if (_viewModel.StartTastingWithValue.CanExecute(args.SelectedRating))
                    _viewModel.StartTastingWithValue.Execute(args.SelectedRating);
            }
        }

        void Handle_OnVintageArchiveChampagneClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ChampagneEditionCardClickedEventArgs args)
        {
            if (_viewModel.VintageArchiveChampagneClicked.CanExecute(args.ChampagneId))
                _viewModel.VintageArchiveChampagneClicked.Execute(args.ChampagneId);
        }

        void Handle_OnOtherChampagneClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ChampagneClickedEventArgs args)
        {
            if(_viewModel.OtherChampagneClicked.CanExecute(args.SelectedChampagne))
                _viewModel.OtherChampagneClicked.Execute(args.SelectedChampagne);
        }

        void Handle_OnArticleClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.LinkPageEventArgs args)
        {
            if(_viewModel.ArticlesClicked.CanExecute(args.SelectedPageLink))
                _viewModel.ArticlesClicked.Execute(args.SelectedPageLink);
        }

        void Handle_OnCommentBtnClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ReviewClickedEventArgs args)
        {
            if (_viewModel.InspectTastingClicked.CanExecute(args.TastingId))
                _viewModel.InspectTastingClicked.Execute(args.TastingId);
        }

        void Handle_OnLikeBtnClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ReviewClickedEventArgs args)
        {
            if (_viewModel.LikeIconClicked.CanExecute(args.TastingId))
                _viewModel.LikeIconClicked.Execute(args.TastingId);
        }

        void Handle_OnProfileNameClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ProfileNameClickedEventArgs args)
        {
            if (_viewModel.ProfileNameClicked.CanExecute(args.UserId))
                _viewModel.ProfileNameClicked.Execute(args.UserId);
        }

        void Handle_OnInspectReview(object sender, CM.ChampagneApp.UI.CustomEventArgs.ReviewClickedEventArgs args)
        {
            if (_viewModel.InspectTastingClicked.CanExecute(args.TastingId))
                _viewModel.InspectTastingClicked.Execute(args.TastingId);
        }
    }
}
