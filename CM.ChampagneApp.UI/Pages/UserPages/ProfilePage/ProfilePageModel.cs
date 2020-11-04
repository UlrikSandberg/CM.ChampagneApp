using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Elements.Lists.InfiniteListView;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.Helpers.Routing;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.FollowingFollowersPages.FollowersPage;
using CM.ChampagneApp.UI.Pages.FollowingFollowersPages.FollowingPage;
using CM.ChampagneApp.UI.Pages.FollowingFollowersPages.Helpers;
using CM.ChampagneApp.UI.Pages.UserPages.CellarPage;
using CM.ChampagneApp.UI.Pages.UserPages.CellarPage.Helpers;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper;
using CM.ChampagneApp.UI.Pages.UserPages.UserSettingsPages.UserSettingsOverviewPage;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using CM.ChampagneApp.UI.UIFacade.Services;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.UserPages.ProfilePage
{
    public class ProfilePageModel : BasePageModel
    {
        //Initdata and dataservices
        private ProfilePageInitData _initData;
        private readonly IUIUserDataService _userDataService;
        private readonly IPageRouter _pageRouter;

        //DownloadManager
        public IDownloadManager<UIUser> DownloadManager { get; set; }
        public ToggleManager<string> ToggleManager { get; set; }

        //Command
        public ICommand NavigateToCellar { get; set; }
        public ICommand NavigateToFollowers { get; set; }
        public ICommand NavigateToFollowing { get; set; }
        public ICommand PageSelected { get; set; }

        private async Task<UIUser> CurrentUserEndpoint()
        {
            return await _userDataService.GetCurrentUser();
        }

        private async Task<UIPublicUser> PublicUserEndpoint()
        {
            return await _userDataService.GetUser(_initData.Id);
        }

        public ProfilePageModel(IEventCollector eventCollector, IUIUserDataService userDataService, IPageRouter pageRouter, IMicroInteractionService microInteractionService) : base(eventCollector)
        {
            //Configure dataService
            _userDataService = userDataService;
            _pageRouter = pageRouter;

            //Configure download manager and ToggleManager
            ToggleManager = new ToggleManager<string>(microInteractionService, this, "", "FollowUserFalse.png", () =>
            {
                DownloadManager.Data.followers++;
            }, () => {
                DownloadManager.Data.followers--;
            });

            //Configure Commands
            NavigateToCellar = new Command(async () => await Handle_NavigateToCellar());
            NavigateToFollowers = new Command(async () => await Handle_NavigateToFollowers());
            NavigateToFollowing = new Command(async () => await Handle_NavigateToFollowing());
            RightIconClicked = new Command(async () => await Handle_RightIconClicked());
            PageSelected = new Command<string>(async (x) => await Handle_PageClicked(x));
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            if (initData != null)
            {
                _initData = initData as ProfilePageInitData;
                ToggleManager.BoolToggle.ToggledValue = _initData.IsCurrentUser ? "Settings" : "FollowUserTrue.png";
                ToggleManager.BoolToggle.ToggleState = _initData.IsCurrentUser;
                    
                if (_initData.IsCurrentUser)
                {
                    DownloadManager = new DownloadManager<UIUser, UIUser, UIUser>(CurrentUserEndpoint, updateEndpoint: CurrentUserEndpoint, updateMapper: new UserUpdateMapper());
                }
                else
                {
                    HasBackButton = true;
                    DownloadManager = new DownloadManager<UIUser, UIPublicUser, UIPublicUser>(PublicUserEndpoint, () => ToggleManager.BoolToggle.ToggleState = DownloadManager.Data.IsRequesterFollowing, new PublicUserToUIUserDataMapper(), new PublicUserUpdateMapper(), PublicUserEndpoint, () => ToggleManager.BoolToggle.ToggleState = DownloadManager.Data.IsRequesterFollowing);
                }
                OnReloadEvent += DownloadManager.UpdateHandler;
            }
            else
            {
                throw new ArgumentException($"InitData for {nameof(ProfilePageModel)} can't be null --> Use initModel: {nameof(ProfilePageInitData)}");
            }
        }

        protected override Task ViewDidAppearInitial()
        {
            DownloadManager.FetchData();

            return base.ViewDidAppearInitial();
        }

        private async Task Handle_PageClicked(string pageUrl)
        {
            await _pageRouter.RouteToPath(pageUrl, CoreMethods);
        }

        private async Task Handle_NavigateToCellar()
        {
            if (DownloadManager.Data != null)
            {
                await CoreMethods.PushPageModel<CellarPageModel>(new CellarPageModelInitData(DownloadManager.Data.id, DownloadManager.Data.name, _initData.IsCurrentUser));
            }
        }

        private async Task Handle_NavigateToFollowers()
        {
            if (DownloadManager.Data != null)
            {
                await CoreMethods.PushPageModel<FollowersPageModel>(new FollowersPageModelInitData(DownloadManager.Data.id, FollowersType.Members, _initData.IsCurrentUser, DownloadManager.Data.name));
            }
        }

        private async Task Handle_NavigateToFollowing()
        {
            if (DownloadManager.Data != null)
            {
                await CoreMethods.PushPageModel<FollowingPageModel>(new FollowingPageModelInitData(DownloadManager.Data.id, DownloadManager.Data.name, _initData.IsCurrentUser));
            }
        }

        protected override async Task Handle_RightIconClicked()
        {
            if (_initData.IsCurrentUser)
            {
                await CoreMethods.PushPageModel<UserSettingsOverviewPageModel>();
            }
            else
            {
                if(DownloadManager.Data != null)
                {
                    await ToggleManager.Handle_FollowToggle(DownloadManager.Data.id);
                }
            }
        }
    }
}