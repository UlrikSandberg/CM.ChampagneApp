using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.Pages.Discover;
using CM.ChampagneApp.UI.Pages.TastingPages.Helpers;
using CM.ChampagneApp.UI.Pages.TastingPages.InspectTastingPage;
using CM.ChampagneApp.UI.Pages.UserPages.CellarPage.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using CM.ChampagneApp.UI.UIFacade.Services;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.UserPages.CellarPage
{
    public class CellarPageModel : BasePageModel
    {
        private int _selectedPageIndex = 0;
        private bool _isInitLoadOfSaved = true;

        //NavigationTitle
        public string NavigationTitle { get; set; } = "Cellar";

        //Initdata and dataservices
        private CellarPageModelInitData _initData;
        private readonly IUIUserDataService _userDataService;

        //Itemsource for carouselView
        public ObservableCollection<CellarCarouselModel<UIUserCellarChampagneModel>> ItemSource { get; set; } = new ObservableCollection<CellarCarouselModel<UIUserCellarChampagneModel>>();

        //CarouseModels
        private CellarCarouselModel<UIUserCellarChampagneModel> TastedCarouselModel;
        private CellarCarouselModel<UIUserCellarChampagneModel> SavedCarouselModel;

        //Commands
        public ICommand PageSelected { get; set; }
        public ICommand Champagne_PersonalNotesSelected { get; set; }
        public ICommand Champagne_ProfileSelected { get; set; }
        public ICommand EmptyStateButtonClicked { get; set; }

        private async Task<IEnumerable<UIUserCellarChampagneModel>> TastedPagingEndpoint(int page, int pageSize)
        {
            return await _userDataService.GetUserCellarPagedAsync(_initData.UserId, page, pageSize);
        }

        private async Task<IEnumerable<UIUserCellarChampagneModel>> SavedPagingEndpoint(int page, int pageSize)
        {
            return await _userDataService.GetUserCellarSavedPagedAsync(page, pageSize, _initData.UserId);
        }

        public CellarPageModel(IEventCollector eventCollector, IUIUserDataService userDataService) : base(eventCollector)
        {
            _userDataService = userDataService;

            //Initialize CarouselModels
            TastedCarouselModel = new CellarCarouselModel<UIUserCellarChampagneModel>
            {
                PagingService = new PagingService<UIUserCellarChampagneModel>(TastedPagingEndpoint, new CollectionViewDataResolver<UIUserCellarChampagneModel>(), 30),
                EmptyStateModel = new EmptyStateModel(),
                IsFlipEnabled = true
            };
            SavedCarouselModel = new CellarCarouselModel<UIUserCellarChampagneModel>
            {
                PagingService = new PagingService<UIUserCellarChampagneModel>(SavedPagingEndpoint, new CollectionViewDataResolver<UIUserCellarChampagneModel>(), 30),
                EmptyStateModel = new EmptyStateModel(),
                IsFlipEnabled = false
            };
        
            //Add CarouselModels to carouselView itemSource
            ItemSource.Add(TastedCarouselModel);
            ItemSource.Add(SavedCarouselModel);

            //Configure Commands
            PageSelected = new Command<int>(Handle_PageSelected);
            Champagne_ProfileSelected = new Command<UIUserCellarChampagneModel>(async (x) => await Handle_Champagne_ProfileSelected(x));
            Champagne_PersonalNotesSelected = new Command<UIUserCellarChampagneModel>(async (x) => await Handle_Champagne_PersonalNotesSelected(x));
            EmptyStateButtonClicked = new Command(async () => await Handle_EmptyStateButtonClicked());
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            if(initData != null)
            {
                _initData = initData as CellarPageModelInitData;
                ResolveInitData();
                //Start Download
                TastedCarouselModel.PagingService.FetchEntitiesPagedAsync();
            }
            else
            {
                throw new ArgumentException($"InitData for {nameof(CellarPageModel)} can't be null --> Use initModel: {nameof(CellarPageModelInitData)}");
            }
        }

        private void ResolveInitData()
        {
            TastedCarouselModel.EmptyStateModel.TitleProperty.Value = "No Tasted Champagnes";
            SavedCarouselModel.EmptyStateModel.TitleProperty.Value = "No Saved Champagnes";
            if (_initData.IsCurrentUser)
            {

                TastedCarouselModel.EmptyStateModel.BodyProperty.Value = "You havn't tasted any champagnes yet. Begin to fill your cellar with the champagnes you have tasted to easily keep track of your Champagne Moments.";
                TastedCarouselModel.EmptyStateModel.ButtonTextProperty.Value = "Find Champagne";

                SavedCarouselModel.EmptyStateModel.BodyProperty.Value = "You can use the bookmark option to save champagnes to your cellar. You can access it from the option button.";
                SavedCarouselModel.EmptyStateModel.IconProperty.Value = "OptionIcon.png";
                SavedCarouselModel.EmptyStateModel.ButtonTextProperty.Value = "Find Champagnes";
            }
            else
            {
                TastedCarouselModel.EmptyStateModel.BodyProperty.Value = string.IsNullOrEmpty(_initData.Username) ? "This member havn't tasted any champagnes yet." : $"{_initData.Username} havn't tasted any champagnes yet." ;
                SavedCarouselModel.EmptyStateModel.BodyProperty.Value = string.IsNullOrEmpty(_initData.Username) ? "This member havn't saved any champagnes yet." : $"{_initData.Username} havn't saved any champagnes yet.";
            }
            if (!_initData.IsCurrentUser)
            {
                NavigationTitle = !string.IsNullOrEmpty(_initData.Username) ? $"{_initData.Username}'s {NavigationTitle}" : $"{NavigationTitle}";
            }
            else
            {
                NavigationTitle = "My Cellar";
            }
        }

        private void Handle_PageSelected(int pageIndex)
        {
            _selectedPageIndex = pageIndex;
            if(pageIndex == 1 && _isInitLoadOfSaved)
            {
                SavedCarouselModel.PagingService.FetchEntitiesPagedAsync();
            }
        }

        private async Task Handle_Champagne_PersonalNotesSelected(UIUserCellarChampagneModel chosenChampagne)
        {
            await CoreMethods.PushPageModel<InspectTastingPageModel>(new InspectTastingInitData(chosenChampagne.TastingId, chosenChampagne.ChampagneId));
        }

        private async Task Handle_Champagne_ProfileSelected(UIUserCellarChampagneModel chosenChampagne)
        {
            await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(chosenChampagne.ChampagneId));
        }

        private async Task Handle_EmptyStateButtonClicked()
        {
            await CoreMethods.SwitchSelectedTab<DiscoverPageModel>();
        }
    }
}
