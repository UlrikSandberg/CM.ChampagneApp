using System;
using System.Windows.Input;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.CustomEventArgs;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Pages.TastingPages.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;

namespace CM.ChampagneApp.UI.Pages.TastingPages.CreateTastingPage
{
	public class CreateTastingPageModel : BasePageModel
    {
        //Initdata and dataservices
        private readonly IUIChampagneDataService _uiChampagneDataService;
        private readonly IUITastingDataService _tastingDataService;
        private CreateTastingInitData _initData;

        //DownloadManager
        public IDownloadManager<UIEditTastingModel> DownloadManager { get; set; }

        public string LoadingMessages { get; set; } = null;

        public double Rating { get; private set; }
		private string Review { get; set; }
		public bool IsTasting { get; set; }
		private Guid ChampagneId { get; set; }

        public ICommand Finished { get; set; }
        public ICommand RatingChanged { get; set; }

        public ICommand ReviewEntered { get; set; }
        public ICommand PriceEntered { get; set; }
        public ICommand CurrencySelected { get; set; }
        public ICommand DeleteReview { get; set; }

		public ICommand UploadingIndicatorDissappeared { get; set; }

		public CreateTastingPageModel(IUIChampagneDataService uiChampagneDataService, IUITastingDataService tastingDataService, IEventCollector ec) : base(ec)
        {
            //Configure Dataservices
			_tastingDataService = tastingDataService;
			_uiChampagneDataService = uiChampagneDataService;

            //Configure downloadmanager
            DownloadManager = new DownloadManager<UIEditTastingModel, UIEditTastingModel, UIEditTastingModel>(() => _tastingDataService.GetCurrentUserTastingForEdit(_initData.ChampagneId), () => 
            {
                if(!DownloadManager.Data.IsTastingNull)
                {
                    //EditTasting = result;
                    Rating = DownloadManager.Data.Rating;
                    Review = DownloadManager.Data.Review;
                    IsTasting = false;
                }
                else
                {
                    IsTasting = true;
                }
            });

            Finished = new Command(async () => await OnFinished());
            RatingChanged = new Command<SelectedRatingEventArgs>(OnRatingChanged);
            ReviewEntered = new Command<string>(OnReviewEntered);
            PriceEntered = new Command<double>(OnPriceEntered);
            CurrencySelected = new Command<CurrencyLibrary.Currencies>(OnCurrencySelected);
            DeleteReview = new Command(async () => await OnDeleteReview());
			UploadingIndicatorDissappeared = new Command(async () => await OnUploadingIndicatorDissappeared());

            HasBackButton = true;
        }

		public override void Init(object initData)
		{
            base.Init(initData);

            _initData = initData as CreateTastingInitData;

            if(_initData != null)
            {
                Rating = _initData.ChosenRating;
                IsTasting = _initData.IsTasting;
                ChampagneId = _initData.ChampagneId;

                DownloadManager.FetchData();
            }
            else
            {
                throw new ArgumentException($"InitData for {nameof(CreateTastingPageModel)} can't be null --> Use initModel: {nameof(CreateTastingInitData)}");
            }
        }

        protected override Task Handle_RightIconClicked()
        {
            OnFinished();
            return base.Handle_RightIconClicked();
        }

        private bool ShouldGoBackWhenComplete { get; set; } = false;
        private async Task OnUploadingIndicatorDissappeared()
		{
			IsUploadingIndicatorVisible = false;
			IsUploadingReview = false;
			IsDoneUploadingReviewWithError = false;
			IsDoneUploadingReviewWithSucces = false;
			if (ShouldGoBackWhenComplete)
			{
				await CoreMethods.PopPageModel();
			}
		}

		public bool IsUploadingIndicatorVisible { get; set; } = false;
		public bool IsUploadingReview { get; set; } = false;
		public bool IsDoneUploadingReviewWithSucces { get; set; } = false;
		public bool IsDoneUploadingReviewWithError { get; set; } = false;
        private async Task OnFinished()
        {
            if(Rating > 0.0)
            {
				IsUploadingReview = true;
				IsUploadingIndicatorVisible = true;
				if(IsTasting)
                {
					LoadingMessages = "Adding Champagne to your personal cellar...";
					await Task.Delay(100);
					var response = await _tastingDataService.CreateTasting(ChampagneId, Review, Rating);
					if(!response.IsSuccesfull)
					{
						IsDoneUploadingReviewWithError = true;
						await CoreMethods.DisplayAlert("Error: ", response.Message + "\n Try Again", "OK");
					}
					else
					{
						ShouldGoBackWhenComplete = true;
						IsDoneUploadingReviewWithSucces = true;
					}
                }
                else
                {
					LoadingMessages = "Please wait while we update your personal tasting notes...";
					await Task.Delay(100);
					var response = await _tastingDataService.EditTasting(DownloadManager.Data.Id, Review, Rating);
					if(!response.IsSuccesfull)
					{

						IsDoneUploadingReviewWithError = true;
						await CoreMethods.DisplayAlert("Error: ", response.Message + "\n Try Again", "OK");
					}
					else
					{
						IsDoneUploadingReviewWithSucces = true;
					}               
                }
            }
			else
			{
				await CoreMethods.DisplayAlert("No stars", "We need your rating to save this tasting.", "OK");
			}
        }

        private void OnReviewEntered(string text)
        {
            System.Diagnostics.Debug.WriteLine("The review text: " + text + ". Has been entered");
			Review = text;
        }

        private void OnPriceEntered(double price)
        {
            System.Diagnostics.Debug.WriteLine("The price: " + price + " has been entered");
        }

        private void OnCurrencySelected(CurrencyLibrary.Currencies currency)
        {
            var currencyLib = new CurrencyLibrary();
            System.Diagnostics.Debug.WriteLine("The currency: "+ currencyLib.CurrencyDic[currency]);
        }

        
        private async Task OnDeleteReview()
        {
			var choice = await CoreMethods.DisplayAlert("Delete tasting?", "This will permanently delete your tasting notes and remove the champagne from your cellar...", "Ok, Delete", "Cancel");
			if (choice)
			{
				LoadingMessages = "Hold on while we remove the champagne from your cellar";
				if (!IsUploadingReview)
				{

					IsUploadingReview = true;
					IsUploadingIndicatorVisible = true;

					var response = await _tastingDataService.DeleteTasting(DownloadManager.Data.Id);

					if (!response.IsSuccesfull)
					{
						IsDoneUploadingReviewWithError = true;
						await CoreMethods.DisplayAlert("Error: ", response.Message + "\n Try Again", "OK");
					}
					else
					{
						ShouldGoBackWhenComplete = true;
						IsDoneUploadingReviewWithSucces = true;
					}               
				}
			}
        }      

        private void OnRatingChanged(SelectedRatingEventArgs args)
        {
            Rating = args.SelectedRating;
        }
	}
}
