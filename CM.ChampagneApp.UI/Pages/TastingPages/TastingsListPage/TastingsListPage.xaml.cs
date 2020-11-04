using System;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Pages.TastingPages.TastingsListPage
{
    public partial class TastingsListPage : BaseContentPage
    {
        private TastingsListPageModel _viewModel;

        public TastingsListPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
			//TastingsList.TranslationY = 70;
			//TastingsList.Padding = new Thickness(0, 0, 0, 70);
		}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as TastingsListPageModel;
        }

        void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
		{
			if(_viewModel.ScrollToButtom.CanExecute((UITasting)e.Item))
			{
				_viewModel.ScrollToButtom.Execute((UITasting)e.Item);
			}
		}

        void Handle_OnReviewChosen(object sender, CM.ChampagneApp.UI.CustomEventArgs.ReviewClickedEventArgs args)
        {
            if(_viewModel.InspectReview.CanExecute(args.TastingId))
            {
				_viewModel.InspectReview.Execute(args.TastingId);
            }
        }

        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
        {
            var viewModel = (TastingsListPageModel)this.BindingContext;
            if(viewModel.NavigateBack.CanExecute(null))
            {
                viewModel.NavigateBack.Execute(null);
            }
        }

		public void Handle_OnRightNavIconClicked(object sender, System.EventArgs e)
		{
			//OptionMenu.Opacity = 0;
            //OptionMenu.IsVisible = true;
            //OptionMenu.FadeTo(1, 200);

			if(_viewModel.RightIconClicked.CanExecute(null))
			{
				_viewModel.RightIconClicked.Execute(null);
			}
		}

		public async void Handle_OnBackgroundClicked(object sender, System.EventArgs e)
		{
            if(_viewModel.CancelOptionsView.CanExecute(null))
			{
				_viewModel.CancelOptionsView.Execute(null);
			}
		}

		void Handle_OnProfileNameClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ProfileNameClickedEventArgs args)
		{
			if(_viewModel.ProfileNameClicked.CanExecute(args.UserId))
			{
				_viewModel.ProfileNameClicked.Execute(args.UserId);
			}
		}

		void Handle_OnInspectReview(object sender, CM.ChampagneApp.UI.CustomEventArgs.ReviewClickedEventArgs args)
		{
			if (_viewModel.InspectReview.CanExecute(args.TastingId))
			{
				_viewModel.InspectReview.Execute(args.TastingId);
			}
		}

		void Handle_OnLikeBtnClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ReviewClickedEventArgs args)
		{
			if(_viewModel.LikeBtnClicked.CanExecute(args.TastingId))
			{
				_viewModel.LikeBtnClicked.Execute(args.TastingId);
			}
		}

		void Handle_OnCommentBtnClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ReviewClickedEventArgs args)
		{
			if(_viewModel.CommentBtnClicked.CanExecute(args.TastingId))
			{
				_viewModel.CommentBtnClicked.Execute(args.TastingId);
			}
		}

		void Handle_OnClickedEmptyState(object sender, System.EventArgs e)
		{
			if(_viewModel.EmptyStateClicked.CanExecute(null))
			{
				_viewModel.EmptyStateClicked.Execute(null);
			}
		}
    }
}
