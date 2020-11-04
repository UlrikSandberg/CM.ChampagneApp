using System;
using System.Collections.Generic;
using System.Windows.Input;
//using CM.ChampagneApp.Business.Models;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.UI.PageModelInitData;
using FreshMvvm;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using CM.ChampagneApp.UI.UIFacade.Services;
using System.Threading.Tasks;
using System.Linq;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Pages.TastingPages.InspectTastingPage;
using CM.ChampagneApp.UI.Pages.TastingPages.CreateTastingPage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;
using CM.ChampagneApp.UI.Pages.TastingPages.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;

namespace CM.ChampagneApp.UI.Pages.TastingPages.TastingsListPage
{
	public class TastingsListPageModel : BasePageModel
	{
        //Initdata and dataservices
        private ChampagneProfileInitData _initData;
        private readonly IUITastingDataService _tastingDataService;
        private readonly IUIUserDataService _userDataService;
        private readonly IBusinessAccountService _bussAuthService;
        private readonly IUIFollowLikeServie _followLikeService;
        private readonly IMicroInteractionService _microInteractionService;

        public ObservableCollection<UITasting> Tastings { get; set; }
		public UIChampagneWithRatingAndTasting ChampagneWithRatingAndTasting { get; set; }

        public ToggleManager<string> BookmarkManager { get; set; }
        public ToggleManager<string> StartTastingToggleManager { get; set; }

        //Indicates if the optionsmenu should be shown!
        public bool ShouldShowOptionsMenu { get; set; } = false;

		//Indicates if the download failed and returned null
		public bool DidDownloadFailed { get; set; } = false;
              
		//Indicates if there has been left no tasting notes yet
		public bool NoTastingsWithNotes { get; set; } = false;

		ChampagneProfileInitData data = null;

		public PagingManager PageManager { get; set; }
		private bool IsRefreshInterrupted { get; set; }
		public bool IsOutOfServiceTextVisible { get; set; }

        public bool IsOptionsMenuVisible { get; set; }

        //Navigation bar
		public ICommand CancelOptionsView { get; set; }

		//ListviewCommands
		public ICommand ScrollToButtom { get; set; }
		public ICommand Refresh { get; set; }
		public ICommand EmptyStateClicked { get; set; }

        //TastingCmds
		public ICommand ProfileNameClicked { get; set; }
		public ICommand InspectReview { get; set; }
		public ICommand LikeBtnClicked { get; set; }
		public ICommand CommentBtnClicked { get; set; }

		//Options menu
		public ICommand Option1Clicked { get; set; }
		public ICommand Option2Clicked { get; set; }

		public TastingsListPageModel(IMicroInteractionService microInteractionService, IUITastingDataService tastingDataService, IUIUserDataService userDataService, IBusinessAccountService bussAuthService, IUIFollowLikeServie followLikeService, IEventCollector ec) : base(ec)
        {
			_microInteractionService = microInteractionService;
			_followLikeService = followLikeService;
			_bussAuthService = bussAuthService;
			_userDataService = userDataService;
			_tastingDataService = tastingDataService;

            //Configure togglemanagers
            BookmarkManager = new ToggleManager<string>(microInteractionService, this, "unbookmarkChampagneOptions.png", "bookmarkChampagneOptions.png", () => BookmarkManager.CustomBoolToggle.ToggleState = true, () => BookmarkManager.CustomBoolToggle.ToggleState = false, customBoolToggle: new BoolToggle<string>("Remove from cellar", "Save to cellar"));
            StartTastingToggleManager = new ToggleManager<string>(microInteractionService, this, "editTastingChampagneOptions.png", "startTastingChampagneOptions.png", customBoolToggle: new BoolToggle<string>("Edit Tasting", "Start Tasting"));

			//ListView cmds
			ScrollToButtom = new Command<UITasting>(async (x) => await OnScrollToButtom(x));
			EmptyStateClicked = new Command(async () => await CoreMethods.PushPageModel<CreateTastingPageModel>(new CreateTastingInitData(false, data.ChampagneId, 0.0)));
			Refresh = new Command(async () => await OnRefresh());

			//Tasting cmds
			ProfileNameClicked = new Command<Guid>(async (x) => 
            {
                if (!x.Equals(_bussAuthService.GetId()))
                {
                    await CoreMethods.PushPageModel<ProfilePageModel>(new ProfilePageInitData(x, false));
                }
            });
			LikeBtnClicked = new Command<UITasting>(async (x) =>
            {
                await BookmarkManager.Handle_LikeToggle(
                    x.Id,
                    x.IsLikedByRequester,
                    true,
                    () => {
                        x.IsLikedByRequester = true;
                        x.NumberOfLikes++;
                    },
                    () => {
                        x.IsLikedByRequester = false;
                        x.NumberOfLikes--;
                    });
            });
            CommentBtnClicked = new Command<UITasting>(async (x) => await CoreMethods.PushPageModel<InspectTastingPageModel>(new InspectTastingInitData(x.Id, x.ChampagneId)));
			InspectReview = new Command<UITasting>(async (x) => await CoreMethods.PushPageModel<InspectTastingPageModel>(new InspectTastingInitData(x.Id, x.ChampagneId)));
			PageManager = new PagingManager();

            //OptionsMeny
            Option1Clicked = new Command(async () => await BookmarkManager.Handle_BookmarkToggle(_initData.ChampagneId));
            Option2Clicked = new Command(async () => await CoreMethods.PushPageModel<CreateTastingPageModel>(new CreateTastingInitData(false, _initData.ChampagneId, 0)));

            HasBackButton = true;
		}

        protected override Task Handle_RightIconClicked()
        {
            if(!IsOutOfService && IsInitialDownloadInProgressInverse)
                IsOptionsMenuVisible = !IsOptionsMenuVisible;
            return base.Handle_RightIconClicked();
        }

        public override void Init(object initData)
		{
			base.Init(initData);
            //Dont touch this for now, no good! Download indicator is never stopping...
            _initData = initData as ChampagneProfileInitData;
			data = (ChampagneProfileInitData)initData;
			if (data != null)
			{
                InitialDownload().RunForget();
			}
		}

		protected override async Task OnReconnect()
		{
			await base.OnReconnect();

			IsOutOfService = false;

			if(!IsRefreshInterrupted)
			{
				PageManager.IsInitialLoadInProgress = true;
				await InitialDownload();
			}
			else
			{
				PageManager.RollBack();
			}
		}
		public bool IsInitialDownloadInProgressInverse = false;
		private async Task InitialDownload()
		{

			var result = await _tastingDataService.GetChampagneWithRatingAndTasting(data.ChampagneId, PageManager.Page, PageManager.PageSize, DTO.Models.Helpers.EnumOptions.TastingOrderByOptions.OrderByOptions.AcendingByDate);
			if (result == null)
			{
				IsOutOfService = true;
				PageManager.SetBoolFalseState();
				if(IsRefreshing)
				{
					IsRefreshing = false;
					IsRefreshInterrupted = true;
				}
				return;
			}
			else
			{
				if (!result.Tastings.Any())
                {
                    NoTastingsWithNotes = true;
                }
                BookmarkManager.BoolToggle.ToggleState = result.IsBookmarkedByRequester;
                BookmarkManager.CustomBoolToggle.ToggleState = result.IsBookmarkedByRequester;
                StartTastingToggleManager.BoolToggle.ToggleState = result.IsTastedByRequester;
                StartTastingToggleManager.CustomBoolToggle.ToggleState = result.IsTastedByRequester;
                Tastings = new ObservableCollection<UITasting>(result.Tastings);
                ChampagneWithRatingAndTasting = result;
				PageManager.IncrementPage();
			}       
			PageManager.IsInitialLoadInProgress = false;
			IsRefreshing = false;
			IsInitialDownloadInProgressInverse = true;
		}

		private async Task OnScrollToButtom(UITasting tasting)
		{
			var tastingIndex = Tastings.IndexOf(tasting);

			if (Tastings.Count - 1 == tastingIndex)
			{
				await DownloadTastings();
			}
		}
      
		private async Task DownloadTastings()
		{
			IsOutOfServiceTextVisible = false;
			if (!PageManager.AllEntitiesHasBeenDownloaded)
			{
				if (!PageManager.IsLoadingEntities)
				{
					PageManager.IsLoadingEntities = true;

					var tastings = await _tastingDataService.GetTastingsPagedWithFilterAsync(data.ChampagneId, PageManager.Page, PageManager.PageSize, true);

					if(tastings == null)
					{
						PageManager.SetBoolFalseState();
						IsOutOfServiceTextVisible = true;
						return;
					}

					PageManager.IncrementPage();

					if (!tastings.Any())
					{
						PageManager.AllEntitiesHasBeenDownloaded = true;
						PageManager.IsLoadingEntities = false;
						return;
					}

					foreach (var tasting in tastings)
					{
						Tastings.Add(tasting);
					}

					PageManager.IsLoadingEntities = false;

				}
			}
		} 

        //Refresh
		public bool IsRefreshing { get; set; }
		private async Task OnRefresh()
		{         
			IsRefreshing = true;
			PageManager.AllEntitiesHasBeenDownloaded = false;
			PageManager.SetPage(0);
			await InitialDownload();
		}
	}
}