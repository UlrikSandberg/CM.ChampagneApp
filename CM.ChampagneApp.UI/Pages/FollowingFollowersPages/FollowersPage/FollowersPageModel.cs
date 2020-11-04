using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Elements.Lists.InfiniteListView;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.Pages.FollowingFollowersPages.Helpers;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper;
using CM.ChampagneApp.UI.UIFacade.Models.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure;
using CM.ChampagneApp.UI.UIFacade.Services;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.FollowingFollowersPages.FollowersPage
{
    public class FollowersPageModel : BasePageModel
    {
        //InitData and dataServices
        private FollowersPageModelInitData _initData;
        private readonly UIUserDataService _userDataService;
        private readonly UIBrandDataService _brandDataService;
        private readonly IMicroInteractionService _microInteractionService;
        private readonly IBusinessAccountService _bussAuthService;

        public string NavigationTitle { get; set; } = "Followers";

        //PagingService and emptyStateModel
        public PagingService<AbstractFollowModel> PagingService { get; set; }
        public EmptyStateModel EmptyStateModel { get; set; } = new EmptyStateModel();
        public ToggleManager<string> ToggleManager { get; set; }

        public ICommand CardClicked { get; set; }
        public ICommand FollowButtonClicked { get; set; }
        public ICommand EmptyStateButtonClicked { get; set; }

        //PagingService for member followers
        private async Task<IEnumerable<AbstractFollowModel>> PagingEndPoint(int page, int pageSize)
        {
            if(_initData.FollowersType.Equals(FollowersType.Members))
            {
                return await _userDataService.GetUserFollowers(_initData.FollowersForId, page, pageSize);
            }
            else
            {
                return await _brandDataService.GetBrandFollowers(_initData.FollowersForId, page, pageSize);
            }
        }

        public FollowersPageModel(IEventCollector eventCollector, UIUserDataService userDataService, UIBrandDataService brandDataService, IMicroInteractionService microInteractionService, IBusinessAccountService bussAuthService) : base(eventCollector)
        {
            _userDataService = userDataService;
            _brandDataService = brandDataService;
            _microInteractionService = microInteractionService;
            _bussAuthService = bussAuthService;

            //PagingService and toggleManager
            PagingService = new PagingService<AbstractFollowModel>(PagingEndPoint, new CollectionViewDataResolver<AbstractFollowModel>(), 30);
            ToggleManager = new ToggleManager<string>(microInteractionService, this, "", "");

            //Configure commands
            CardClicked = new Command<AbstractFollowModel>(async (x) => await Handle_CardClicked(x));
            FollowButtonClicked = new Command<AbstractFollowModel>(async (x) => await Handle_FollowButtonClicked(x));
            EmptyStateButtonClicked = new Command(async () => await Handle_EmptyStateClicked());
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            if(initData != null)
            {
                _initData = (FollowersPageModelInitData)initData;
                ResolveInitData();
                //Request pagingService to fetch entities
                PagingService.FetchEntitiesPagedAsync();
            }
            else
            {
                throw new ArgumentNullException($"InitData for {nameof(FollowersPageModel)} can't be null --> Use initModel: {nameof(FollowersPageModelInitData)}");
            }
        }

        private void ResolveInitData()
        {
            if(_initData.IsCurrentUser)
            {
                EmptyStateModel.TitleProperty.Value = "No Followers Yet";
                EmptyStateModel.BodyProperty.Value = "You don't have any followers. Interact with other people and or invite your friends to join Champagne Moments";
            }
            else
            {
                NavigationTitle = $"{_initData.Username}'s {NavigationTitle}";
                EmptyStateModel.TitleProperty.Value = "No Followers Yet";
                EmptyStateModel.ButtonTextProperty.Value = "Follow";
                EmptyStateModel.BodyProperty.Value = string.IsNullOrEmpty(_initData.Username) ? "This user has no followers yet, be the first..." : $"{_initData.Username} has no followers, be the first...";
            }
        }

        private async Task Handle_EmptyStateClicked()
        {
            PagingService.LoadingStatus = InfiniteListViewActivityStatus.IsDownloading;
            //Follow
            var response = await _microInteractionService.HandleFollow(_initData.FollowersForId, false);

            if(response.IsSuccesfull)
            {
                //Show a followModel
                PagingService.FetchEntitiesPagedAsync(true);
            }
            else
            {
                PagingService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                await CoreMethods.DisplayAlert("Error: ", response.Message + "\n Try Again", "OK");
            }
        }

        private async Task Handle_CardClicked(AbstractFollowModel followModel)
        {
            if (!_bussAuthService.GetId().Equals(followModel.ProfileId))
            {
                await CoreMethods.PushPageModel<ProfilePageModel>(new ProfilePageInitData(followModel.ProfileId, false));    
            }
            else
            {
                await CoreMethods.SwitchSelectedTab<ProfilePageModel>();
            }
        }

        private async Task Handle_FollowButtonClicked(AbstractFollowModel followModel)
        {
            await ToggleManager.Handle_FollowToggle(
                followModel.ProfileId,
                followModel.IsRequesterFollowing,
                true,
                () => followModel.IsRequesterFollowing = true,
                () => followModel.IsRequesterFollowing = false);
        }
    }
}
