using System;
using System.Collections.Generic;
using System.Windows.Input;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.PageModelInitData;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using CM.ChampagneApp.Acq;
using StructureMap.Graph;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Pages.TastingPages.InspectTastingPage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneLightroomPage;
using CM.ChampagneApp.UI.Pages.TastingPages.CreateTastingPage;
using CM.ChampagneApp.UI.Pages.TastingPages.TastingsListPage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;
using CM.ChampagneApp.UI.Pages.TastingPages.Helpers;
using CM.ChampagneApp.UI.Helpers.Routing;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using System.Linq;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;

namespace CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage
{
    public class ChampagneProfilePageModel : BasePageModel
    {
        //Dataservices and initData
        private readonly IUIChampagneDataService _champagneDataService;
        private readonly IUIArticleDataService _articleDataService;
        private readonly IPageRouter _router;
        private readonly IUITastingDataService _tastingDataService;
        private readonly IBusinessAccountService _bussAuthService;
        private ChampagneProfileInitData _initData;

        //Download and togglesManagers
        public IDownloadManager<UIChampagne> ChampagneProfileManager { get; set; }
        public IDownloadManager<ObservableCollection<UITasting>> TastingsManager { get; set; }
        public ToggleManager<string> BookmarkManager { get; set; }
        public ToggleManager<string> StartTastingToggleManager { get; set; }

        public VintageArchiveManager<UIChampagneLight> OtherEditions { get; set; }
        public VintageArchiveManager<UIChampagneRoot> OtherChampagnes { get; set; }
        public VintageArchiveManager<UIChampagneLight> VintageArchive { get; set; }
        public ObservableCollection<UIArticleOverview> Articles { get; set; }

        //Commands
        public ICommand NavigateToChampagneLightroom { get; set; }
        public ICommand StartTasting { get; set; }
        public ICommand StartTastingWithValue { get; set; }

        //OptionsMenu
        public ICommand Option1Clicked { get; set; }

        //Other editions, other champagnes and vintagearchive
        public ICommand OtherChampagneClicked { get; set; }
        public ICommand VintageArchiveChampagneClicked { get; set; }

        //Articles
        public ICommand ArticlesClicked { get; set; }

        //Tastings
        public ICommand SeeAllTastingsClicked { get; set; }
        public ICommand InspectTastingClicked { get; set; }

        public ICommand ProfileNameClicked { get; set; }
        public ICommand LikeIconClicked { get; set; }


        //Properties
        public bool IsOptionsMenuVisible { get; set; }

        public ChampagneProfilePageModel(IMicroInteractionService microInteractionService, IUIChampagneDataService uiChampagneDataService, IUIArticleDataService uIArticleDataService, IPageRouter router, IUIUserDataService userDataService, IUITastingDataService tastingDataService, IBusinessAccountService bussAuthService, IUIFollowLikeServie followLikeServie, IEventCollector ec) : base(ec)
        {
            //Configure 
            _champagneDataService = uiChampagneDataService;
            _articleDataService = uIArticleDataService;
            _router = router;
            _tastingDataService = tastingDataService;
            _bussAuthService = bussAuthService;

            //Configure Download and toggleManagers
            ChampagneProfileManager = new DownloadManager<UIChampagne, UIChampagne, UIChampagne>(() => _champagneDataService.GetChampagneProfile(_initData.ChampagneId), () => 
            {
                BookmarkManager.BoolToggle.ToggleState = ChampagneProfileManager.Data.IsBookmarkedByRequester;
                BookmarkManager.CustomBoolToggle.ToggleState = ChampagneProfileManager.Data.IsBookmarkedByRequester;
                StartTastingToggleManager.BoolToggle.ToggleState = ChampagneProfileManager.Data.IsTastedByRequester;
                StartTastingToggleManager.CustomBoolToggle.ToggleState = ChampagneProfileManager.Data.IsTastedByRequester;

                if(!ChampagneProfileManager.Data.RootIsSingleton)
                {
                    OtherEditions.DownloadVintageArchive(ChampagneProfileManager.Data.ChampagneRootId, "");
                }

                OtherChampagnes.DownloadVintageArchive();
                if(Articles == null)
                {
                    Articles = new ObservableCollection<UIArticleOverview>(_articleDataService.GetChampagneProfileArticles(ChampagneProfileManager.Data));
                }
                TastingsManager.FetchData();
            }, updateEndpoint: () => _champagneDataService.GetChampagneProfile(_initData.ChampagneId), updateMapper: new ChampagneProfileUpdateMapper(), updateCompletionHandler: () =>
            {
                BookmarkManager.BoolToggle.ToggleState = ChampagneProfileManager.Data.IsBookmarkedByRequester;
                BookmarkManager.CustomBoolToggle.ToggleState = ChampagneProfileManager.Data.IsBookmarkedByRequester;
                StartTastingToggleManager.BoolToggle.ToggleState = ChampagneProfileManager.Data.IsTastedByRequester;
                StartTastingToggleManager.CustomBoolToggle.ToggleState = ChampagneProfileManager.Data.IsTastedByRequester;
            });

            TastingsManager = new DownloadManager<ObservableCollection<UITasting>, IEnumerable<UITasting>, IEnumerable<UITasting>>(() => _tastingDataService.GetTastingsPagedWithFilterAsync(_initData.ChampagneId, 0, 2, true), dataMapper: new TastingsDataMapper(), updateMapper: new TastingsUpdateMapper(), updateEndpoint: () => _tastingDataService.GetTastingsPagedWithFilterAsync(_initData.ChampagneId, 0, 2, true), updateCompletionHandler: () => 
            {
                TastingsManager.Data = TastingsManager.Data;
            });

            OnReloadEvent += ChampagneProfileManager.UpdateHandler;

            BookmarkManager = new ToggleManager<string>(microInteractionService, this, "unbookmarkChampagneOptions.png", "bookmarkChampagneOptions.png",() => BookmarkManager.CustomBoolToggle.ToggleState = true, () => BookmarkManager.CustomBoolToggle.ToggleState = false, customBoolToggle: new BoolToggle<string>("Remove from cellar", "Save to cellar"));
            StartTastingToggleManager = new ToggleManager<string>(microInteractionService, this, "editTastingChampagneOptions.png", "startTastingChampagneOptions.png", customBoolToggle: new BoolToggle<string>("Edit Tasting", "Start Tasting"));
            OtherEditions = new VintageArchiveManager<UIChampagneLight>(async (x) => await _champagneDataService.GetBottlesFromRoot(x), (x) => 
            {
                return x.Where(c => !c.Id.Equals(_initData.ChampagneId)); 
            });
            OtherChampagnes = new VintageArchiveManager<UIChampagneRoot>(async () => await _champagneDataService.GetChampagneRootsShuffled(true, 5), (x) =>
            {
                return x.Where(c => !c.Id.Equals(ChampagneProfileManager.Data.ChampagneRootId)); 
            });
            VintageArchive = new VintageArchiveManager<UIChampagneLight>(_champagneDataService.GetBottlesFromRoot);

            //Command
            NavigateToChampagneLightroom = new Command(async () => await CoreMethods.PushPageModel<ChampagneLightroomPageModel>(new ChampagneLightroomInitData(ChampagneProfileManager.Data.BottleImgUrl, ChampagneProfileManager.Data.BottleName, ChampagneProfileManager.Data.BrandName)));
            StartTasting = new Command(async () => await CoreMethods.PushPageModel<CreateTastingPageModel>(new CreateTastingInitData(false, _initData.ChampagneId, 0)));
            StartTastingWithValue = new Command<double>(async (x) => await CoreMethods.PushPageModel<CreateTastingPageModel>(new CreateTastingInitData(false, _initData.ChampagneId, x)));
            //Option 1 Command
            Option1Clicked = new Command(async () => await BookmarkManager.Handle_BookmarkToggle(_initData.ChampagneId));
            //Other editions - Other champagnes - VintageArchive
            VintageArchiveChampagneClicked = new Command<Guid>(async (x) => await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(x)));
            OtherChampagneClicked = new Command<UIChampagneRoot>(async (x) =>
            {
                if (x.ChampagneIds.Count == 1)
                    await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(x.ChampagneIds[0]));
                else
                    await VintageArchive.DownloadVintageArchive(x.Id, x.FolderContentType);
            });
            ArticlesClicked = new Command<string>(async (x) => await _router.RouteToPath(x, CoreMethods));

            //Tastings
            SeeAllTastingsClicked = new Command(async () => await CoreMethods.PushPageModel<TastingsListPageModel>(new ChampagneProfileInitData(_initData.ChampagneId)));
            InspectTastingClicked = new Command<UITasting>(async (x) => await CoreMethods.PushPageModel<InspectTastingPageModel>(new InspectTastingInitData(x.Id, _initData.ChampagneId)));
            ProfileNameClicked = new Command<Guid>(async (x) =>
            {
                if(!_bussAuthService.GetId().Equals(x))
                {
                    await CoreMethods.PushPageModel<ProfilePageModel>(new ProfilePageInitData(x, false));
                }
            });
            LikeIconClicked = new Command<UITasting>(async (x) =>
            {
                await BookmarkManager.Handle_LikeToggle(
                    x.Id,
                    x.IsLikedByRequester,
                    true,
                    () => { 
                        x.IsLikedByRequester = true;
                        x.NumberOfLikes++; },
                    () => {
                        x.IsLikedByRequester = false;
                        x.NumberOfLikes--;});
            });

            HasBackButton = true;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            if(initData != null)
            {
                _initData = initData as ChampagneProfileInitData;

                ChampagneProfileManager.FetchData();
            }
            else
            {
                throw new ArgumentException($"InitData for {nameof(ChampagneProfilePageModel)} can't be null --> Use initModel: {nameof(ChampagneProfileInitData)}");
            }
        }

        protected override Task Reload()
        {
            UpdateTasting();
            return base.Reload();
        }

        private async Task UpdateTasting()
        {
            await TastingsManager.FetchData();
        }

        protected override Task Handle_RightIconClicked()
        {
            IsOptionsMenuVisible = !IsOptionsMenuVisible;
            return base.Handle_RightIconClicked();
        }
    }
}