using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.Pages.BrandPages.BrandListPage;
using CM.ChampagneApp.UI.Pages.BrandPages.BrandProfilePage;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.Pages.FollowingFollowersPages.Helpers;
using CM.ChampagneApp.UI.Pages.Helpers;
using CM.ChampagneApp.UI.Pages.SearchPages.GlobalSearchPage;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper;
using CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure;
using CM.ChampagneApp.UI.UIFacade.Services;
using Xamarin.Forms;
using ProfilePageModel = CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.ProfilePageModel;

namespace CM.ChampagneApp.UI.Pages.FollowingFollowersPages.FollowingPage
{
    public class FollowingPageModel : BasePageModel
    {
        private int _selectedPageIndex = 0;
        private bool _isInitLoadOfBrands = true;

        //Initdata and dataservices
        private FollowingPageModelInitData _initData;
        private readonly IMicroInteractionService _microInteractionService;
        private readonly IUIUserDataService _userDataService;
        private readonly IBusinessAccountService _bussAuthService;

        public string NavigationTitle { get; set; } = "Following";

        //Itemsource for carouselView
        public ObservableCollection<CarouselListModel<AbstractFollowModel>> ItemSource { get; set; } = new ObservableCollection<CarouselListModel<AbstractFollowModel>>();

        //CarouselModels and toggleManager
        private CarouselListModel<AbstractFollowModel> MembersCarouselModel;
        private CarouselListModel<AbstractFollowModel> BrandsCarouselModel;
        
        private ToggleManager<string> ToggleManager { get; set; }

        public ICommand CardClicked { get; set; }
        public ICommand FollowButtonClicked { get; set; }
        public ICommand PageSelected { get; set; }
        public ICommand EmptyStateButtonClicked { get; set; }

        //PagingService for members
        //PagingService for member followers
        private async Task<IEnumerable<AbstractFollowModel>> MemberPagingEndPoint(int page, int pageSize)
        {
            return await _userDataService.GetUserFollowing(_initData.UserId, page, pageSize);
        }

        private async Task<IEnumerable<AbstractFollowModel>> BrandPagingEndPoint(int page, int pageSize)
        {
            return await _userDataService.GetUserBrandFollowing(_initData.UserId, page, pageSize);
        }

        public FollowingPageModel(IEventCollector eventCollector, IMicroInteractionService microInteractionService, IUIUserDataService userDataService, IBusinessAccountService bussAuthService) : base(eventCollector)
        {
            _microInteractionService = microInteractionService;
            _userDataService = userDataService;
            _bussAuthService = bussAuthService;

            //Initialize CarouselModels
            MembersCarouselModel = new CarouselListModel<AbstractFollowModel>
            {
                PagingService = new PagingService<AbstractFollowModel>(MemberPagingEndPoint, new CollectionViewDataResolver<AbstractFollowModel>(), 30),
                EmptyStateModel = new EmptyStateModel()
            };
            BrandsCarouselModel = new CarouselListModel<AbstractFollowModel>
            {
                PagingService = new PagingService<AbstractFollowModel>(BrandPagingEndPoint, new CollectionViewDataResolver<AbstractFollowModel>(), 30),
                EmptyStateModel = new EmptyStateModel()
            };

            //Add pagingService to carouselView itemSource
            ItemSource.Add(MembersCarouselModel);
            ItemSource.Add(BrandsCarouselModel);
            
            //ToggleManager
            ToggleManager = new ToggleManager<string>(microInteractionService, this, "", "");

            //Configure Commands
            CardClicked = new Command<AbstractFollowModel>(async (x) => await Handle_CardClicked(x));
            FollowButtonClicked = new Command<AbstractFollowModel>(async (x) => await Handle_FollowButtonClicked(x));
            PageSelected = new Command<int>(Handle_PageSelected);
            EmptyStateButtonClicked = new Command(async () => await Handle_EmptyStateButtonClicked());
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            if(initData != null)
            {
                _initData = initData as FollowingPageModelInitData;
                ResolveInitData();
                //Start download
                MembersCarouselModel.PagingService.FetchEntitiesPagedAsync();
            }
            else
            {
                throw new ArgumentException($"InitData for {nameof(FollowingPageModel)} can't be null --> Use initModel: {nameof(FollowingPageModelInitData)}");
            }
        }

        private void ResolveInitData()
        {
            if(_initData.IsCurrentUser)
            {
                //Configure emptyStates for CurrentUser.
                MembersCarouselModel.EmptyStateModel.TitleProperty.Value = "It's lonely here...";
                MembersCarouselModel.EmptyStateModel.BodyProperty.Value = "Find members to follow and get inspired by their Champagne Moments";
                MembersCarouselModel.EmptyStateModel.ButtonTextProperty.Value = "Find";

                BrandsCarouselModel.EmptyStateModel.TitleProperty.Value = "No brands...";
                BrandsCarouselModel.EmptyStateModel.BodyProperty.Value = "Start to follow brands to receive news and brand notifications";
                BrandsCarouselModel.EmptyStateModel.ButtonTextProperty.Value = "Find";
            }
            else
            {
                //Configure emptyStates for visiting a user.
                MembersCarouselModel.EmptyStateModel.TitleProperty.Value = "It's lonely here...";
                MembersCarouselModel.EmptyStateModel.BodyProperty.Value = string.IsNullOrEmpty(_initData.Username) ? "This user is currently not following any members" : $"{_initData.Username} is currently not following any members";

                BrandsCarouselModel.EmptyStateModel.TitleProperty.Value = "No brands...";
                BrandsCarouselModel.EmptyStateModel.BodyProperty.Value = string.IsNullOrEmpty(_initData.Username) ? "This user is currently not following any brands" : $"{_initData.Username} is currently not following any brands";
            }

            if(!_initData.IsCurrentUser)
            {
                NavigationTitle = string.IsNullOrEmpty(_initData.Username) ? $"{NavigationTitle}" : $"{_initData.Username}'s Following";
            }
        }

        private async Task Handle_EmptyStateButtonClicked()
        {
            if (_selectedPageIndex == 0)
            {
                //Switch tabbar selected index to Find
                await CoreMethods.SwitchSelectedTab<GlobalSearchPageModel>();
            }
            else
            {
                //Switch tabbar selected index to Brands
                await CoreMethods.SwitchSelectedTab<BrandListPageModel>();
            }
        }

        private void Handle_PageSelected(int selectedPageIndex)
        {
            _selectedPageIndex = selectedPageIndex;
            if(selectedPageIndex == 1 && _isInitLoadOfBrands)
            {
                BrandsCarouselModel.PagingService.FetchEntitiesPagedAsync();
            }
        }

        private async Task Handle_CardClicked(AbstractFollowModel followModel)
        {
            if(_selectedPageIndex == 0)
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
            else
            {
                await CoreMethods.PushPageModel<BrandProfilePageModel>(new BrandProfileInitData(followModel.ProfileId));
            }
        }

        private async Task Handle_FollowButtonClicked(AbstractFollowModel followModel)
        {
            await ToggleManager.Handle_FollowToggle(
                followModel.ProfileId,
                followModel.IsRequesterFollowing,
                _selectedPageIndex != 1,
                () => followModel.IsRequesterFollowing = true,
                () => followModel.IsRequesterFollowing = false);
        }
    }
}
