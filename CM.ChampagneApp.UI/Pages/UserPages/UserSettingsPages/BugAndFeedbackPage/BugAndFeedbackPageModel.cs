using System;
using FreshMvvm;
using System.Windows.Input;
using Xamarin.Forms;
using CM.ChampagneApp.DTO.Builders;
using CM.ChampagneApp.UI.UIFacade.Services;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;

namespace CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.BugAndFeedbackPage
{
	public class BugAndFeedbackPageModel : BasePageModel
    {      
		//***** Bug actions *****
		private string EnteredBugDescription = null;
		public ICommand BugDescriptionEntered { get; set; }
		private byte[] UploadedBugImage = null;
		public ICommand BugImageUploaded { get; set; }      
		public ICommand SubmitBug { get; set; }

		private string EnteredFeedbackDescription = null;
		public ICommand FeedbackDescriptionEntered { get; set; }
		public ICommand SubmitFeedback { get; set; }
      
		public ICommand SubmitCompleted { get; set; }

		private readonly IUIUserDataService userDataService;

		public BugAndFeedbackPageModel(IUIUserDataService userDataService, IEventCollector ec) : base(ec)
        {
			this.userDataService = userDataService;
			BugImageUploaded = new Command<byte[]>(OnBugImageUploaded);
			BugDescriptionEntered = new Command<string>(OnBugDescriptionEntered);
			SubmitBug = new Command(async () => await OnSubmitBug());

			FeedbackDescriptionEntered = new Command<string>(OnFeedbackDescriptionEntered);
			SubmitFeedback = new Command(async () => await OnSubmitFeedback());

			SubmitCompleted = new Command(async () => await OnSubmitCompleted());

        }

		public override void Init(object initData)
		{
			base.Init(initData);
		}

        //***** Bug cmds *****

        private void OnBugDescriptionEntered(string bugDescription)
		{
			EnteredBugDescription = bugDescription;
		}

        private void OnBugImageUploaded(byte[] file)
		{
			UploadedBugImage = file;
		}

        private async Task OnSubmitBug()
		{
			if (EnteredBugDescription == null && UploadedBugImage == null)
			{
				await CoreMethods.DisplayAlert("Failure: ", "You must either submit a decription or upload an image", "OK");
			}
			else
			{
				await AnimateSubmission();
				var builder = new FeedbackAndBugSubmission.FeedbackAndBugSubmissionBuilder();

				builder
					.SetSubmissionType(DTO.Builders.Helpers.SubmissionType.Bug)
					.SetContent(EnteredBugDescription)
					.SetImage(UploadedBugImage)
					.SetMayBeContacted(true);

				await userDataService.SubmitBugAndFeedback(builder.Build());
			}
		}


        //***** Feedback cmds *****


        private void OnFeedbackDescriptionEntered(string feedbackDescription)
		{
			EnteredFeedbackDescription = feedbackDescription;
		}

        private async Task OnSubmitFeedback()
		{
			if (EnteredFeedbackDescription == null)
			{
				await CoreMethods.DisplayAlert("Failure: ", "You must submit a decription", "OK");
			}
			else
			{
				await AnimateSubmission();
				var builder = new FeedbackAndBugSubmission.FeedbackAndBugSubmissionBuilder();

				builder
					.SetImage(null)
					.SetContent(EnteredFeedbackDescription)
					.SetSubmissionType(DTO.Builders.Helpers.SubmissionType.Feedback)
					.SetMayBeContacted(true);

				await userDataService.SubmitBugAndFeedback(builder.Build());
			}
		}

        private async Task OnSubmitCompleted()
		{
			UploadingIndicatorIsVisible = false;
			UploadingIndicatorIsLoading = false;
			UploadingIndicatorIsDoneUploadingWithSucces = false;
			await CoreMethods.PopPageModel();
		}

		public bool UploadingIndicatorIsVisible { get; set; } = false;
		public bool UploadingIndicatorIsLoading { get; set; } = false;
		public bool UploadingIndicatorIsDoneUploadingWithSucces { get; set; } = false;
        private async Task AnimateSubmission()
		{
			UploadingIndicatorIsVisible = true;
			UploadingIndicatorIsLoading = true;
			await Task.Delay(2000); //TODO: Remove delays like this
			UploadingIndicatorIsDoneUploadingWithSucces = true;

		}
	}
}
