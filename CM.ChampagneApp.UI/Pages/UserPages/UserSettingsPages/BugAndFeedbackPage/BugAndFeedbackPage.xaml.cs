using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.BugAndFeedbackPage
{
    public partial class BugAndFeedbackPage : ContentPage
    {
        public BugAndFeedbackPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

		void Handle_OnBugActionClicked(object sender, System.EventArgs e)
		{
			UploadingIndicator.Text = "Thank you for submitting a bug!";
			BugUI.IsVisible = true;
			FeedbackUI.IsVisible = false;
		}

		void Handle_OnFeedbackActionClicked(object sender, System.EventArgs e)
		{
			UploadingIndicator.Text = "Thank you for your suggestion!";
			BugUI.IsVisible = false;
			FeedbackUI.IsVisible = true;
		}

		void Handle_SubmitBug(object sender, System.EventArgs e)
		{
			//AnimateSubmission();
			var viewModel = (BugAndFeedbackPageModel)this.BindingContext;
			if(viewModel.SubmitBug.CanExecute(null))
			{
				viewModel.SubmitBug.Execute(null);
			}
		}

		void Handle_SubmitFeedback(object sender, System.EventArgs e)
		{
			//AnimateSubmission();
			var viewModel = (BugAndFeedbackPageModel)this.BindingContext;
			if(viewModel.SubmitFeedback.CanExecute(null))
			{
				viewModel.SubmitFeedback.Execute(null);
			}
		}

		void Handle_OnBugDescriptionEntered(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
		{
			var viewModel = (BugAndFeedbackPageModel)this.BindingContext;
			if(viewModel.BugDescriptionEntered.CanExecute(args.TextEntered))
			{
				viewModel.BugDescriptionEntered.Execute(args.TextEntered);
			}
		}

		void Handle_OnBugImageChosen(object sender, CM.ChampagneApp.UI.CustomEventArgs.ImageChosenEventArgs args)
		{

			var viewModel = (BugAndFeedbackPageModel)this.BindingContext;
			if(viewModel.BugImageUploaded.CanExecute(args.file))
			{
				viewModel.BugImageUploaded.Execute(args.file);
			}
		}

		void Handle_OnFeedbackDescriptionEntered(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
		{
			var viewModel = (BugAndFeedbackPageModel)this.BindingContext;
			if(viewModel.FeedbackDescriptionEntered.CanExecute(args.TextEntered))
			{
				viewModel.FeedbackDescriptionEntered.Execute(args.TextEntered);
			}
		}

		void Handle_OnDidDisappear(object sender, System.EventArgs e)
		{
			UploadingIndicator.IsVisible = false;
			var viewModel = (BugAndFeedbackPageModel)this.BindingContext;
			if(viewModel.SubmitCompleted.CanExecute(null))
			{
				viewModel.SubmitCompleted.Execute(null);
			}
		}

        private async Task AnimateSubmission()
		{
			UploadingIndicator.IsVisible = true;
			UploadingIndicator.IsLoading = true;
			await Task.Delay(2000);
			UploadingIndicator.IsDoneUploadingWithSucces = true;
		}

		void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
		{
			var viewModel = (BugAndFeedbackPageModel)this.BindingContext;
			if(viewModel.NavigateBack.CanExecute(null))
			{
				viewModel.NavigateBack.Execute(null);
			}
		}
    }
}
