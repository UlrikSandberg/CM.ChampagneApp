using System;
using System.Collections.Generic;
using Xamarin.Forms;
using CM.ChampagneApp.UI.CustomLayouts;
using System.Windows.Input;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.UIFacade.Models.UICommentModels;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Pages.TastingPages.InspectTastingPage
{
	public partial class InspectTastingPage : BaseContentPage
    {
        private InspectTastingPageModel _viewModel;

        public InspectTastingPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
			InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = BindingContext as InspectTastingPageModel;
        }
       
		void Handle_OnKeyboardRaised(object sender, KeyboardRaisedEventArgs args)
		{
			if(args.KeyboardHeight > 0)
			{
				ClickableBackground.IsVisible = true;
			}
		}

		void Handle_OnKeyboardUnfocused(object sender, System.EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Inspect page keyboard unfocused");
		}

		void Handle_ClickedKeyboardBackground(object sender, System.EventArgs e)
		{
			ClickableBackground.IsVisible = false;
			ChatEntry.Unfocus();
		}

		void Handle_OnProfileNameClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ProfileNameClickedEventArgs args)
		{
            if (_viewModel.ProfileNameClicked.CanExecute(args.UserId))
                _viewModel.ProfileNameClicked.Execute(args.UserId);
		}

		void Handle_OnTastingLikeBtnClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ReviewClickedEventArgs args)
		{
			var viewModel = (InspectTastingPageModel)this.BindingContext;
			if(viewModel.TastingLikeBtnClicked.CanExecute(args.TastingId))
			{
				viewModel.TastingLikeBtnClicked.Execute(args.TastingId);
			}
		}

		void Handle_OnPostComment(object sender, CM.ChampagneApp.UI.CustomEventArgs.PostCommentEventArgs args)
		{
			var viewModel = (InspectTastingPageModel)this.BindingContext;
			if(viewModel.PostComment.CanExecute(args.Content))
			{
				viewModel.PostComment.Execute(args.Content);
			}
		}

		void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
		{
			var viewModel = (InspectTastingPageModel)this.BindingContext;
			if(viewModel.ScrollToBottom.CanExecute((UICommentModel)e.Item))
			{
				viewModel.ScrollToBottom.Execute((UICommentModel)e.Item);
			}
		}

		void Handle_OnLikeComment(object sender, ModelClickedEventArgs<UICommentModel> args)
		{
			var viewModel = (InspectTastingPageModel)this.BindingContext;
			if(viewModel.CommentLikeClicked.CanExecute(args.SelectedModel))
			{
				viewModel.CommentLikeClicked.Execute(args.SelectedModel);
			}
		}

		void Handle_OnEditCommentCancel(object sender, System.EventArgs e)
		{
			var viewModel = (InspectTastingPageModel)this.BindingContext;
			if(viewModel.CancelEditComment.CanExecute(null))
			{
				viewModel.CancelEditComment.Execute(null);
			}
		}

		void Handle_OnUpdateComment(object sender, CM.ChampagneApp.UI.CustomEventArgs.UpdateCommentEventArgs args)
		{
			var viewModel = (InspectTastingPageModel)this.BindingContext;
			if(viewModel.UpdateComment.CanExecute(args))
			{
				viewModel.UpdateComment.Execute(args);
			}
		}
    }
}
