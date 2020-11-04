using System;
using System.Windows.Input;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.UIReadModels;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Services;
using System.Collections.ObjectModel;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using CM.ChampagneApp.UI.UIFacade.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;
using System.Linq;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage;
using CM.ChampagneApp.UI.Pages.SearchPages.Helpers;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;

namespace CM.ChampagneApp.UI.Pages.SearchPages.FilterSearchResultPage
{
	public class FilterSearchResultPageModel : BasePageModel
    {
        //InitData and dataServices
        private SearchResultsInitData _initData;
        private readonly IUIChampagneDataService _champagneDataService;

        //PagingService and emptyStateModel
        public PagingService<UIUserCellarChampagneModel> PagingService { get; set; }
        public EmptyStateModel EmptyStateModel { get; set; }

		public ICommand EmptyStateClicked { get; set; }
		public ICommand ChampagneChosen { get; set; }

        private async Task<IEnumerable<UIUserCellarChampagneModel>> PagingEndpoint(int page, int pageSize)
        {
            return await _champagneDataService.GetChampagnesByFilterAsync(_initData.FilterSearchQuery, page, pageSize);
        }

        public FilterSearchResultPageModel(IUIChampagneDataService champagneDataService, IEventCollector ec) : base(ec)
        {
            _champagneDataService = champagneDataService;

            //Initialize pagingService
            PagingService = new PagingService<UIUserCellarChampagneModel>(PagingEndpoint, new CollectionViewDataResolver<UIUserCellarChampagneModel>());
            EmptyStateModel = new EmptyStateModel();
            EmptyStateModel.TitleProperty.Value = "No Results...";
            EmptyStateModel.BodyProperty.Value = "Try expanding your search criteria";
            EmptyStateModel.ButtonTextProperty.Value = "Expand Search";

            EmptyStateClicked = new Command(async () => await OnNavigateBack());
			ChampagneChosen = new Command<UIUserCellarChampagneModel>(async (x) => await OnChampagneChosen(x));
        }

		public override void Init(object initData)
		{
            base.Init(initData);

            if(initData != null)
            {
                _initData = initData as SearchResultsInitData;
                //Start Download
                PagingService.FetchEntitiesPagedAsync();
            }
            else
            {
                throw new ArgumentException($"InitData for {nameof(FilterSearchResultPageModel)} can't be null --> Use initModel: {nameof(SearchResultsInitData)}");
            }
		}
      
		private async Task OnChampagneChosen(UIUserCellarChampagneModel champagneModel)
		{
			await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(champagneModel.ChampagneId));
		}      
    }
}
