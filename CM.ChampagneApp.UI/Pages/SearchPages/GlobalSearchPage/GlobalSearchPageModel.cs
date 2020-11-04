using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Elements.Lists.InfiniteListView;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.UIBrandModels;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;
using CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.UI.UIReadModels;
using Xamarin.Forms;
using CM.ChampagneApp.UI.Pages.BrandPages.BrandProfilePage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Helpers;
using CM.ChampagneApp.UI.Pages.SearchPages.FilterSearchPage;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper;

namespace CM.ChampagneApp.UI.Pages.SearchPages.GlobalSearchPage
{
    public class GlobalSearchPageModel : BasePageModel
    {
        //ItemSource for the different templates in the globalSearchCarouselView
        public ObservableCollection<GlobalSearchCarouselModel> CarouselItemSource { get; set; }

        private string SharedTextEntry = "";

        //Holds information about the champagne search resualt
        private string ChampagneSearchText = "";
        public PagingService<UIChampagneSearchModel> _champagnesPageService { get; set; }
        private GlobalSearchCarouselModel champagneListViewTemplate;

        //Holds information about the brand search results
        private string BrandSearchText = "";
        public PagingService<UIBrandSearchModel> _brandsPageService { get; set; }
        private GlobalSearchCarouselModel brandListViewTemplate;

        //Holds information about the user search results
        private string UserSearchText = "";
        public PagingService<UIUserSearchModel> _usersPageService { get; set; }
        private GlobalSearchCarouselModel userListViewTemplate;

        //Command to handle changes in selected page
        public ICommand PageChanged { get; set; }

        //Search Commands
        public ICommand TextDidChange { get; set; }
        public ICommand StartSearch { get; set; }

        //Champagne list commands
        public ICommand ChampagneSelected { get; set; }
        public ICommand ChampagneRequestNextPage { get; set; }
        public ICommand TopVisibleChampagne { get; set; }
        public ICommand ChampagneReconnect { get; set; }

        //Brand list command
        public ICommand BrandSelected { get; set; }
        public ICommand BrandRequestNextPage { get; set; }
        public ICommand TopVisibleBrand { get; set; }
        public ICommand BrandReconnect { get; set; }

        //User list command
        public ICommand UserSelected { get; set; }
        public ICommand UserRequestNextPage { get; set; }
        public ICommand UserReconnect { get; set; }
        
        public ICommand FilterSearchClicked { get; set; }

        public bool ShowRefreshIndicator { get; set; } = false;
        private bool ViewHaveAppeared = false;

        //Champagne paging endpoint
        private async Task<IEnumerable<UIChampagneSearchModel>> ChampagnesPagingEndpoint(int page, int pageSize)
        {
            return await _champagneDataService.SearchChampagnes(ChampagneSearchText, page, pageSize);
        }

        //Brands paging endpoint
        private async Task<IEnumerable<UIBrandSearchModel>> BrandsPagingEndpoint(int page, int pageSize)
        {
            return await _brandDataService.SearchBrands(BrandSearchText, page, pageSize);
        }

        private async Task<IEnumerable<UIUserSearchModel>> UsersSearchPagingEndpoint(int page, int pageSize)
        {
            return await _userDataService.SearchUsersByUsernameAndProfilenamePagedAsync(UserSearchText, page, pageSize);
        }

        //Dependencies
        private readonly IUIChampagneDataService _champagneDataService;
        private readonly IUIBrandDataService _brandDataService;
        private readonly IUIUserDataService _userDataService;
        private readonly IBusinessAccountService _bussAuthService;

        //Selected page
        private SelectedPage CurrentPage = SelectedPage.Champagne;

        public GlobalSearchPageModel(IEventCollector eventCollector, IUIChampagneDataService champagneDataService, IUIBrandDataService brandDataService, IUIUserDataService userDataService, IBusinessAccountService bussAuthService) : base(eventCollector)
        {
            //Dependencies
            _champagneDataService = champagneDataService;
            _brandDataService = brandDataService;
            _userDataService = userDataService;
            _bussAuthService = bussAuthService;

            //Setup paging services
            _champagnesPageService = new PagingService<UIChampagneSearchModel>(ChampagnesPagingEndpoint);
            _brandsPageService = new PagingService<UIBrandSearchModel>(BrandsPagingEndpoint);
            _usersPageService = new PagingService<UIUserSearchModel>(UsersSearchPagingEndpoint);

            SetItemSources();

            //Search Commands
            TextDidChange = new Command<string>(Handle_TextDidChange);
            StartSearch = new Command<string>(Handle_StartSearch);

            //Page Commands
            PageChanged = new Command<int>(OnPagedChanged);
            ChampagneReconnect = new Command(Handle_OnReconnect);

            //Champagnelist commands
            ChampagneSelected = new Command<Guid>(async (x) => await Handle_ChampagneSelected(x));
            ChampagneRequestNextPage = new Command(async () => await Handle_ChampagneRequestNextPage());
            TopVisibleChampagne = new Command<object>(Handle_Champagnes_TopChampagneVisible);
            FilterSearchClicked = new Command(async () => await CoreMethods.PushPageModel<FilterSearchPageModel>());

            //Brandlist commands
            BrandSelected = new Command<Guid>(async (x) => await Handle_BrandSelected(x));
            BrandRequestNextPage = new Command(async () => await Handle_BrandRequestNextPage());
            TopVisibleBrand = new Command<object>(Handle_Brand_TopChampagneVisible);
            BrandReconnect = new Command(Handle_OnReconnect);

            //Userlist commands
            UserRequestNextPage = new Command(async () => await Handle_UserRequestNextPage());
            UserReconnect = new Command(Handle_OnReconnect);
            UserSelected = new Command<Guid>(async (x) => await Handle_Users_Selected(x));

            //Configure the carouselItemSource
            ConfigurePagingServices();

            ShowRefreshIndicator = true;
            ShowRefreshIndicator = false;
        }


        protected override Task ViewDidAppearInitial()
        {
            ViewHaveAppeared = true;
            DownloadUserDefault();

            return base.ViewDidAppearInitial();
        }

        private void Handle_OnReconnect()
        {
            if(CurrentPage.Equals(SelectedPage.Champagne))
            {
                DownloadChampagnes();
            }
            if(CurrentPage.Equals(SelectedPage.Brands))
            {
                DownloadBrands();
            }
            if(CurrentPage.Equals(SelectedPage.Users))
            {
                DownloadUsers();
            }
        }

        private void Handle_TextDidChange(string newText)
        {
            SharedTextEntry = newText;
            //Disply load here!

            if (CurrentPage.Equals(SelectedPage.Champagne))
            {
                if (string.IsNullOrEmpty(newText))
                {
                    champagneListViewTemplate.EmptyStateText = "Search Champagnes...";
                    _champagnesPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                    ShowRefreshIndicator = false;
                    return;
                }

                if (_champagnesPageService.itemSource.Count > 0)
                {
                    ShowRefreshIndicator = true;
                    _champagnesPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                }
                else
                {
                    if (ChampagneSearchText.Equals(newText)) //Since the search term is identical abort the search
                    {
                        _champagnesPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                        ShowRefreshIndicator = false;
                        return;
                    }
                    champagneListViewTemplate.EmptyStateText = "Searching...";
                   _champagnesPageService.LoadingStatus = InfiniteListViewActivityStatus.IsDownloading;
                }
            }
            if(CurrentPage.Equals(SelectedPage.Brands))
            {
                if (string.IsNullOrEmpty(newText))
                {
                    brandListViewTemplate.EmptyStateText = "Search Brands...";
                    _brandsPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                    ShowRefreshIndicator = false;
                    return;
                }

                if (_brandsPageService.itemSource.Count > 0)
                {
                    ShowRefreshIndicator = true;
                    _brandsPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                }
                else
                {
                    if(BrandSearchText.Equals(newText))
                    {
                        _brandsPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                        ShowRefreshIndicator = false;
                        return;
                    }
                    brandListViewTemplate.EmptyStateText = "Searching...";
                    _brandsPageService.LoadingStatus = InfiniteListViewActivityStatus.IsDownloading;
                }
            }
            if(CurrentPage.Equals(SelectedPage.Users))
            {
                if (string.IsNullOrEmpty(newText))
                {
                    //Show the newest 30 default users;
                    _brandsPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                    ShowRefreshIndicator = false;
                    if(ViewHaveAppeared)
                    {
                        DownloadUserDefault();
                    }
                    return;
                }
                if (_usersPageService.itemSource.Count > 0)
                {
                    ShowRefreshIndicator = true;
                    _brandsPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                }
                else
                {
                    brandListViewTemplate.EmptyStateText = "Searching...";
                    _brandsPageService.LoadingStatus = InfiniteListViewActivityStatus.IsDownloading;
                }
            }
        }

        private void Handle_StartSearch(string newText)
        {
            if(CurrentPage.Equals(SelectedPage.Champagne))
            {
                if(ChampagneSearchText.Equals(newText)) //Since the search term is identical abort the search
                {
                    _champagnesPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                    ShowRefreshIndicator = false;
                    return;
                }

                //Download champagnes!
                ChampagneSearchText = newText;
                if(string.IsNullOrEmpty(newText))
                {
                    champagneListViewTemplate.EmptyStateText = "Search Champagnes...";
                    _champagnesPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                    ShowRefreshIndicator = false;
                }
                else
                {
                    DownloadChampagnes(true);
                }
            }
            if(CurrentPage.Equals(SelectedPage.Brands))
            {
                if(BrandSearchText.Equals(newText)) //Since search term is identical abort search
                {
                    _brandsPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                    ShowRefreshIndicator = false;
                    return;
                }

                //Download brands!
                BrandSearchText = newText;
                if(string.IsNullOrEmpty(newText))
                {
                    brandListViewTemplate.EmptyStateText = "Search Brands...";
                    _brandsPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                    ShowRefreshIndicator = false;
                }
                else
                {
                    DownloadBrands(true);
                }
            }
            if(CurrentPage.Equals(SelectedPage.Users))
            {
                if(string.IsNullOrEmpty(newText))
                {
                    //Just abort the search...
                    return;
                }

                //If the searhc term is identical to previous search abort the search
                if(UserSearchText.Equals(newText))
                {
                    _usersPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                    ShowRefreshIndicator = false;
                    return;
                }

                //Make a new search
                UserSearchText = newText;

                DownloadUsers(true);
            }
        }

        //***** Champagne Section *****
        //***** Champagne Section *****
        //***** Champagne Section *****
        private async Task DownloadChampagnes(bool forceInitialDownload = false)
        {
            await _champagnesPageService.FetchEntitiesPagedAsync(forceInitialDownload);
        }

        private async Task Handle_ChampagneSelected(Guid champagneId)
        {
            await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(champagneId));
        }

        private async Task Handle_ChampagneRequestNextPage()
        {
            //If the text is empty and defaultContent is not shown, dont scroll further
            if(string.IsNullOrEmpty(ChampagneSearchText))
            {
                return;
            }

            await DownloadChampagnes();
        }

        private void Handle_Champagnes_InvalidSearch(object sender, System.EventArgs e)
        {
            champagneListViewTemplate.EmptyStateText = "No Results...";
            _champagnesPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
            ShowRefreshIndicator = false;
        }

        private void Handle_Champagnes_LoadingEntitesChanged(object sender, ValueChangedEventArgs<bool> args)
        {
            if(!args.NewValue)
            {
                ShowRefreshIndicator = false;
            }
        }

        private void Handle_Champagnes_TopChampagneVisible(object entry)
        {
            champagneListViewTemplate.TopVisibleItem = entry;
        }

        private void Handle_Champagnes_OutOfService(object sender, System.EventArgs e)
        {
            champagneListViewTemplate.EmptyStateText = "Couldn't load results...";
        }

        //***** Brand Section *****
        //***** Brand Section *****
        //***** Brand Section *****
        private async Task DownloadBrands(bool forceInitialReload = false)
        {
            await _brandsPageService.FetchEntitiesPagedAsync(forceInitialReload);
        }

        private async Task Handle_BrandSelected(Guid brandId)
        {
            await CoreMethods.PushPageModel<BrandProfilePageModel>(new BrandProfileInitData(brandId));
        }

        private async Task Handle_BrandRequestNextPage()
        {
            if(string.IsNullOrEmpty(BrandSearchText))
            {
                return;
            }

            await DownloadBrands();
        }

        private void Handle_Brands_InvalidSearch(object sender, System.EventArgs e)
        {
            brandListViewTemplate.EmptyStateText = "No Results...";
            _brandsPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
            ShowRefreshIndicator = false;
        }

        private void Handle_brands_LoadingEntitesChanged(object sender, ValueChangedEventArgs<bool> args)
        {
            if (!args.NewValue)
            {
                ShowRefreshIndicator = false;
            }
        }

        private void Handle_Brand_TopChampagneVisible(object entry)
        {
            champagneListViewTemplate.TopVisibleItem = entry;
        }

        private void Handle_Brands_OutOfService(object sender, EventArgs e)
        {
            brandListViewTemplate.EmptyStateText = "Couldn't load results...";
        }

        //***** UserSection *****
        //***** UserSection *****
        //***** UserSection *****
        private bool DefaultUserDownloadWasSuccesfull = false;
        private bool IsShowingDefaultUsers = false;
        private List<UIUserSearchModel> DefaultUser;
        private async Task DownloadUserDefault()
        {
            if(DefaultUserDownloadWasSuccesfull)
            {
                _usersPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                if(!IsShowingDefaultUsers)
                {
                    _usersPageService.itemSource.Clear();
                    foreach (var user in DefaultUser)
                    {
                        _usersPageService.itemSource.Add(user);
                    }
                    userListViewTemplate.EmptyStateText = "";
                } 
                IsShowingDefaultUsers = true;
                return;
            }
            //Show initial download
            userListViewTemplate.EmptyStateText = "Searching...";
            _usersPageService.LoadingStatus = InfiniteListViewActivityStatus.IsDownloading;
            var result = await _userDataService.SearchUsersPagedAsyncLight(0, 30);

            if(result == null)
            {
                userListViewTemplate.EmptyStateText = "Couldn't load results...";
                _usersPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
                return;
            }

            DefaultUser = new List<UIUserSearchModel>(result.Select(c => new UIUserSearchModel
            {
                Id = c.Id,
                Name = c.Name,
                ProfileName = c.ProfileName,
                ImageId = c.ImageId
            }));

            _usersPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;

            _usersPageService.itemSource.Clear();

            foreach(var user in DefaultUser)
            {
                _usersPageService.itemSource.Add(user);
            }
            userListViewTemplate.EmptyStateText = "";
            IsShowingDefaultUsers = true;
            DefaultUserDownloadWasSuccesfull = true;
        }

        private async Task DownloadUsers(bool forceInitialLoad = false)
        {
            IsShowingDefaultUsers = false;
            await _usersPageService.FetchEntitiesPagedAsync(forceInitialLoad);
        }

        private async Task Handle_UserRequestNextPage()
        {
            if(!string.IsNullOrEmpty(UserSearchText.Trim()) && !string.IsNullOrEmpty(SharedTextEntry.Trim()) && !IsShowingDefaultUsers)
            {
                await DownloadUsers();
            }
        }

        private void Handle_Users_LoadingEntitesChanged(object sender, ValueChangedEventArgs<bool> args)
        {
            if (!args.NewValue)
            {
                ShowRefreshIndicator = false;
            }
        }

        private void Handle_Users_InvalidSearch(object sender, System.EventArgs e)
        {
            userListViewTemplate.EmptyStateText = "No Results...";
            _usersPageService.LoadingStatus = InfiniteListViewActivityStatus.DownloadingFinished;
            ShowRefreshIndicator = false;
        }

        private void Handle_Users_OutOfService(object sender, EventArgs e)
        {
            if(!_usersPageService.itemSource.Any())
            {
                userListViewTemplate.EmptyStateText = "Couldn't load results...";
            }
        }

        private async Task Handle_Users_Selected(Guid userId)
        {
            if (!_bussAuthService.GetId().Equals(userId))
            {
                await CoreMethods.PushPageModel<ProfilePageModel>(new ProfilePageInitData(userId, _bussAuthService.GetId().Equals(userId)));    
            }
            else
            {
                await CoreMethods.SwitchSelectedTab<ProfilePageModel>();
            }
        }

        //***** Configure *****
        private void ConfigurePagingServices()
        {
            //Configure champagne paging service
            _champagnesPageService.OnInvalidSearch += Handle_Champagnes_InvalidSearch;
            _champagnesPageService.LoadingEntitiesChanged += Handle_Champagnes_LoadingEntitesChanged;
            _champagnesPageService.OnOutOfService += Handle_Champagnes_OutOfService;
            _brandsPageService.OnInvalidSearch += Handle_Brands_InvalidSearch;
            _brandsPageService.LoadingEntitiesChanged += Handle_brands_LoadingEntitesChanged;
            _brandsPageService.OnOutOfService += Handle_Brands_OutOfService;
            _usersPageService.OnOutOfService += Handle_Users_OutOfService;
            _usersPageService.LoadingEntitiesChanged += Handle_Users_LoadingEntitesChanged;
            _usersPageService.OnInvalidSearch += Handle_Users_InvalidSearch;
        }

        private void SetItemSources()
        {
            champagneListViewTemplate = new GlobalSearchCarouselModel
            {
                DataTemplate = GlobalSearchCarouselTemplate.ChampagneListTemplate,
                PagingService = _champagnesPageService,
                EmptyStateText = "Champagne Search..."
            };

            brandListViewTemplate = new GlobalSearchCarouselModel
            {
                DataTemplate = GlobalSearchCarouselTemplate.BrandListTemplate,
                EmptyStateText = "Brand Search...",
                PagingService = _brandsPageService
            };

            userListViewTemplate = new GlobalSearchCarouselModel
            {
                DataTemplate = GlobalSearchCarouselTemplate.UserListTemplate,
                EmptyStateText = "",
                PagingService = _usersPageService
            };

            CarouselItemSource = new ObservableCollection<GlobalSearchCarouselModel>
            {
                userListViewTemplate,//First
                champagneListViewTemplate, //Second
                brandListViewTemplate //Third
            };
        }

        private void OnPagedChanged(int pageIndex)
        {
            switch (pageIndex)
            {
                case 0:
                    CurrentPage = SelectedPage.Users;
                    Handle_TextDidChange(SharedTextEntry);
                    Handle_StartSearch(SharedTextEntry);
                    break;
                case 1:
                    CurrentPage = SelectedPage.Champagne;
                    Handle_TextDidChange(SharedTextEntry);
                    Handle_StartSearch(SharedTextEntry);
                    break;
                case 2:
                    CurrentPage = SelectedPage.Brands;
                    Handle_TextDidChange(SharedTextEntry);
                    Handle_StartSearch(SharedTextEntry);
                    break;
            }
        }

        private enum SelectedPage
        {
            Champagne,
            Brands,
            Users
        }
    }
}
