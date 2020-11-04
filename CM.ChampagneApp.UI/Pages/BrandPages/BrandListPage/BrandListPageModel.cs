using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.UI.PageModelInitData;
//using CM.ChampagneApp.Business.Models;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Services;
using FreshMvvm;
using Xamarin.Forms;
using System.Collections.Generic;
using CM.ChampagneApp.DTO.Builders;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Elements.Lists.InfiniteListView;
using System.Collections;
using CM.ChampagneApp.UI.Elements.Helpers.ElementEnum;
using CM.ChampagneApp.UI.Pages.BrandPages.BrandProfilePage;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Helpers;

namespace CM.ChampagneApp.UI.Pages.BrandPages.BrandListPage
{
	public class BrandListPageModel : BasePageModel
	{
        //Means the sorting is ascending as default
        public bool SortingOption { get; set; } = true;

        public ICommand BrandChosen { get; set; }
		public ICommand SubmissionEntered { get; set; }
		public ICommand Refresh { get; set; }
        public ICommand SortOptionChanged { get; set; }

        //Animate Submission
        public bool SuccessIndicatorIsVisible { get; set; } = false;
        public bool SuccessIndicatorIsLoading { get; set; } = false;
        public bool SuccessIndicatorIsDoneUploadingWithSucces { get; set; } = false;
        public SuccesStateEnum SubmissionSuccesState { get; set; } = SuccesStateEnum.Default;

        //InfiniteListView request next page.
        public ICommand DownloadNextPage { get; set; }
        public ICommand SubmissionButtonClicked { get; set; }
        public ICommand ReconnectCommand { get; set; }

        //Required to run and communicate with the InfiniteListView.
        public PagingService<UIBrand> PageService { get; set; }

        //Data-Services to get respective data from backend-API
        private readonly IUIBrandDataService _uIBrandDataService;
		private readonly IUIUserDataService _userDataService;

        //Paging-Endpoint
        private async Task<IEnumerable<UIBrand>> PagingEndPoint(int page, int pageSize)
        {
            return await _uIBrandDataService.GetBrandsPaged(page, pageSize, false, SortingOption);
        }

        public BrandListPageModel(IUIBrandDataService uIBrandDataService, IUIUserDataService userDataService, IEventCollector ec) : base(ec)
        {
            //Dependencies
            _userDataService = userDataService;
			_uIBrandDataService = uIBrandDataService;

            //InfiniteListView setup
            PageService = new PagingService<UIBrand>(PagingEndPoint, collectionViewData: new CollectionViewDataResolver<UIBrand>());

			//Commands         
			BrandChosen = new Command<Guid>(async (x) => await OnBrandChosen(x));
			SubmissionEntered = new Command<string>(async (x) => await OnSubmissionEntered(x));
			Refresh = new Command(async () => await OnRefresh());
            SortOptionChanged = new Command(async () => await SortingOptionChanged());
            DownloadNextPage = new Command(async () => await DownloadBrands());
            ReconnectCommand = new Command(async () => await DownloadBrands());
		}

        protected override Task ViewDidAppearInitial()
        {
            DownloadBrands().RunForget();
            return base.ViewDidAppearInitial();
        }

        protected override async Task OnReconnect()
		{
			await base.OnReconnect();

            PageService.IsOutOfService = false;

            if(PageService.ShouldReload())
            {
                await DownloadBrands();
            }
        }

        private async Task OnRefresh()
        {
            await PageService.Refresh();
        }

        private async Task DownloadBrands()
        {
            await PageService.FetchEntitiesPagedAsync();
        }

        private async Task OnBrandChosen(Guid brandId)
        {
            await CoreMethods.PushPageModel<BrandProfilePageModel>(new BrandProfileInitData(brandId));
        }

        private async Task OnSubmissionEntered(string submission)
        {
            System.Diagnostics.Debug.WriteLine("Upload: " + submission);

            //Animate
            SuccessIndicatorIsVisible = true;
            SuccessIndicatorIsLoading = true;
            await Task.Delay(1000);
            SuccessIndicatorIsDoneUploadingWithSucces = true;

            SubmissionSuccesState = SuccesStateEnum.Succes;

            var builder = new FeedbackAndBugSubmission.FeedbackAndBugSubmissionBuilder();

            builder.SetContent(submission).SetImage(null).SetMayBeContacted(false).SetSubmissionType(DTO.Builders.Helpers.SubmissionType.Feedback);

            await _userDataService.SubmitBugAndFeedback(builder.Build());
        }

        private async Task SortingOptionChanged()
        {
            if(PageService.IsLoadingEntities)
            {
                return;
            }

            //If all brands has been downloaded, there is no need to download again just flip the array
            if(PageService.AllEntitiesHasBeenDownloaded)
            {
                //Flip the array
                var flippedList = new ObservableCollection<UIBrand>();

                for(int i = PageService.itemSource.Count - 1; i > -1; i--)
                {
                    flippedList.Add(PageService.itemSource[i]);
                }

                PageService.itemSource.Clear();
                foreach(var entry in flippedList)
                {
                    PageService.itemSource.Add(entry);
                }

                if(SortingOption)
                {
                    SortingOption = false;
                }
                else
                {
                    SortingOption = true;
                }
                return;
            }

            if (SortingOption)
            {
                SortingOption = false;
                PageService.SetPage(0);
                PageService.AllEntitiesHasBeenDownloaded = false;
                PageService.IsLoadingEntities = false;
                PageService.IsRefreshing = false;
                PageService.itemSource.Clear();
                await PageService.FetchEntitiesPagedAsync();
            }
            else
            {
                SortingOption = true;
                PageService.SetPage(0);
                PageService.AllEntitiesHasBeenDownloaded = false;
                PageService.IsLoadingEntities = false;
                PageService.IsRefreshing = false;
                PageService.itemSource.Clear();
                await PageService.FetchEntitiesPagedAsync();
            }
        }
    }
}
