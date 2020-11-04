using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.SearchPages.FilterSearchPage;
using CM.ChampagneApp.UI.Pages.Top10Pages.Top10ListPage;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;
using CM.ChampagneApp.UI.UIFacade.Services;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.Discover
{
    public class DiscoverPageModel : BasePageModel
    {
        //DataServices
        private readonly IUIChampagneDataService _champagneDataService;

        //PagingService and Vintage archive
        public PagingService<UIChampagneRoot> PagingService { get; set; }
        public VintageArchiveManager<UIChampagneLight> VintageArchive { get; set; }

        //Command
        public ICommand ClickedChampagne { get; set; }
        public ICommand NavigateToTop10List { get; set; }
        public ICommand NavigateToFilterSearch { get; set; }
        public ICommand VintageArchiveItemClicked { get; set; }

        private async Task<IEnumerable<UIChampagneRoot>> ChampagnePagingEndpoint(int page, int pageSize)
        {
            return await _champagneDataService.GetChampagneRoots(page, pageSize);
        }

        public DiscoverPageModel(IUIChampagneDataService champagneDataService, IEventCollector ec) : base(ec)
        {
            _champagneDataService = champagneDataService;

            //Initialize PagingService and vintageArchiveManager
            PagingService = new PagingService<UIChampagneRoot>(ChampagnePagingEndpoint, new CollectionViewDataResolver<UIChampagneRoot>(), 30);
            VintageArchive = new VintageArchiveManager<UIChampagneLight>(_champagneDataService.GetBottlesFromRoot);

            ClickedChampagne = new Command<UIChampagneRoot>(async (x) => await ChampagneChosen(x));
            NavigateToTop10List = new Command(async () => await OnNavigateToTop10List());
            NavigateToFilterSearch = new Command(async () => await OnNavigateToFilterSearch());
            VintageArchiveItemClicked = new Command<Guid>(async (x) => await OnVintageArchiveItemClicked(x));
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            PagingService.FetchEntitiesPagedAsync();
        }

        protected override Task ViewDidAppearInitial()
        {
            MessagingCenter.Send<DiscoverPageModel>(this, MessagingCenterSubscriptionKeys.AppLaunchedReadyForCustomRouting);
            return base.ViewDidAppearInitial();
        }

        private async Task ChampagneChosen(UIChampagneRoot champagne)
        {
			if(champagne.ChampagneIds.Count == 1)
			{
				await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(champagne.ChampagneIds[0]));
			}
			else if(champagne.ChampagneIds.Count > 1)
			{
                await VintageArchive.DownloadVintageArchive(champagne.Id, champagne.FolderContentType);
			}
        }

		private async Task OnVintageArchiveItemClicked(Guid champagneId)
		{
			await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(champagneId));
		}

        private async Task OnNavigateToTop10List()
        {
            await CoreMethods.PushPageModel<Top10ListPageModel>();
        }

        private async Task OnNavigateToFilterSearch()
        {
            await CoreMethods.PushPageModel<FilterSearchPageModel>();
        }
    }
}
