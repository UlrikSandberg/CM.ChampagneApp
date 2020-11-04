using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using CM.ChampagneApp.UI.UIFacade.Models.UICommentModels;
using CM.ChampagneApp.UI.UIFacade.Services;
using System.Linq;
using CM.ChampagneApp.UI.PageModelInitData;
using System.Threading.Tasks;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.UI.CustomEventArgs;
using System.ComponentModel;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.Elements.Helpers.ElementEnum;
using Microsoft.IdentityModel.Tokens;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage;
using CM.ChampagneApp.UI.Pages.TastingPages.CreateTastingPage;
using CM.ChampagneApp.UI.UIFacade.Mapper;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;
using CM.ChampagneApp.UI.Pages.TastingPages.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage;
using CM.ChampagneApp.UI.Pages.UserPages.ProfilePage.Helper;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;

namespace CM.ChampagneApp.UI.Pages.TastingPages.InspectTastingPage
{
	public class InspectTastingPageModel : BasePageModel
    {
        //DataService and initData
        private readonly IUITastingDataService _tastingDataService;
        private readonly IUIUserDataService _userDataService;
        private readonly IUIFollowLikeServie _followLikeServie;
        private readonly IBusinessAccountService _bussAuthService;
        private readonly IMicroInteractionService _microInteractionService;
        private InspectTastingInitData _initData;

        //Data
        public UIInspectTastingModel InspectTastingModel { get; set; }
		public ObservableCollection<UICommentModel> Comments { get; set; }
	    public ObservableCollection<double> DoubleSource { get; set; }

        //ToggleManagers
        public ToggleManager<string> BookmarkManager { get; set; }
        public ToggleManager<string> StartTastingToggleManager { get; set; }

        //Indicates if the download failed
        public bool DidDownloadFail { get; set; } = false;

		public PagingManager PageManager { get; set; }
		public bool IsOutOfServiceTextVisible { get; set; }
		public bool IsRefreshInterrupted { get; set; }

		//Indicates if there where no comments to this tasting
		public string AllCommentsDisplayMessage { get; set; }

		//Indicates whether optionsMenu should be shown
		public bool ShouldShowOptionsMenu { get; set; } = false;

        //NavBar cmds
		public ICommand OptionsMenuClicked { get; set; }
		public ICommand TitleClicked { get; set; }

        //OptionsMeny cmds
		public ICommand BookmarkClicked { get; set; }
		public ICommand TastingClicked { get; set; }

        //Tasting and comment cmds
        public ICommand ProfileNameClicked { get; set; }

        //Tasting cmds
        public ICommand TastingLikeBtnClicked { get; set; }

        //Comment cmds
		public ICommand CommentLikeClicked { get; set; }
		public ICommand DeleteComment { get; set; }
        public ICommand EditComment { get; set; }
		public ICommand CancelEditComment { get; set; }
		public ICommand UpdateComment { get; set; }

        //ChatEntry cmds
		public ICommand PostComment { get; set; }

        //ListView cmds
		public ICommand ScrollToBottom { get; set; }
		public ICommand RefreshData { get; set; } 
      
		public InspectTastingPageModel(IMicroInteractionService microInteractionService, IUITastingDataService tastingDataService, IUIUserDataService userDataService, IUIFollowLikeServie followLikeServie, IBusinessAccountService bussAuthService, IEventCollector ec) : base(ec)
        {
            _microInteractionService = microInteractionService;
			_bussAuthService = bussAuthService;
			_followLikeServie = followLikeServie;
			_userDataService = userDataService;
			_tastingDataService = tastingDataService;

            BookmarkManager = new ToggleManager<string>(microInteractionService, this, "unbookmarkChampagneOptions.png", "bookmarkChampagneOptions.png", () => BookmarkManager.CustomBoolToggle.ToggleState = true, () => BookmarkManager.CustomBoolToggle.ToggleState = false, customBoolToggle: new BoolToggle<string>("Remove from cellar", "Save to cellar"));
            StartTastingToggleManager = new ToggleManager<string>(microInteractionService, this, "editTastingChampagneOptions.png", "startTastingChampagneOptions.png", customBoolToggle: new BoolToggle<string>("Edit Tasting", "Start Tasting"));

            //NavBar
            TitleClicked = new Command(async () => await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(_initData.ChampagneId)));

			//OptionsMenu
			BookmarkClicked = new Command(async () => await BookmarkManager.Handle_BookmarkToggle(_initData.ChampagneId));
			TastingClicked = new Command(async () => 
            {
                ShouldShowOptionsMenu = false;
                await CoreMethods.PushPageModel<CreateTastingPageModel>(new CreateTastingInitData(true, _initData.ChampagneId, 0.0));
            });
			//Tasting cmds
			ProfileNameClicked = new Command<Guid>(async (x) => 
            {
                if (!x.Equals(bussAuthService.GetId()))
                {
                    await CoreMethods.PushPageModel<ProfilePageModel>(new ProfilePageInitData(x, false));
                }
            });

			TastingLikeBtnClicked = new Command<UITasting>(async (x) =>
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
			CommentLikeClicked = new Command<UICommentModel>(async (x) =>
            {
                await BookmarkManager.Handle_LikeToggle(
                    x.Id,
                    x.IsLikedByRequester,
                    false,
                    () => {
                        x.IsLikedByRequester = true;
                        x.NumberOfLikes++;
                    },
                    () => {
                        x.IsLikedByRequester = false;
                        x.NumberOfLikes--;
                    });
            });

            DeleteComment = new Command<UICommentModel>(async (x) => await OnDeleteComment(x));
			EditComment = new Command<UICommentModel>(async (x) => await OnEditComment(x));
			CancelEditComment = new Command(async () => await OnEditCommentCancel());
			UpdateComment = new Command<UpdateCommentEventArgs>(async (x) => await OnUpdateComment(x));
			RefreshData = new Command(async () => await OnRefreshData());
            //ChatEntry cmds
            PostComment = new Command<string>(async (x) => await OnPostComment(x));
            ScrollToBottom = new Command<UICommentModel>(async (x) => await OnScrollToBottom(x));

            PageManager = new PagingManager(30);
            HasBackButton = true;
        }

		public override void Init(object initData)
		{
			base.Init(initData);

            _initData = initData as InspectTastingInitData;

			if(_initData != null)
            {
                DownloadInitTasting().RunForget();
            }
            else
            {
                throw new ArgumentException($"InitData for {nameof(InspectTastingPageModel)} can't be null --> Use initModel: {nameof(InspectTastingInitData)}");
            }      
		}

        protected override Task Handle_RightIconClicked()
        {
            if (!IsOutOfService && IsDownloadingInitTastingInverse)
                ShouldShowOptionsMenu = !ShouldShowOptionsMenu;
            return base.Handle_RightIconClicked();
        }

        protected override async Task OnReconnect()
		{
			await base.OnReconnect();

			IsOutOfService = false;
			if(!IsRefreshInterrupted)
			{
				PageManager.IsInitialLoadInProgress = true;
				IsDownloadingInitTastingInverse = false;
				await DownloadInitTasting();
			}
			else
			{
				PageManager.RollBack();
			}
		}

		public bool IsDownloadingInitTastingInverse { get; set; } = false;
        private async Task DownloadInitTasting()
		{
			var result = await _tastingDataService.GetInspectTasting(_initData.TastingId, PageManager.Page, PageManager.PageSize, false);
			if(result == null)
			{
				IsOutOfService = true;
				IsDownloadingInitTastingInverse = false;
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
				if(!result.Comments.Any())
				{
					PageManager.AllEntitiesHasBeenDownloaded = true;
				}
                BookmarkManager.BoolToggle.ToggleState = result.IsBookmarkedByRequester;
                BookmarkManager.CustomBoolToggle.ToggleState = result.IsBookmarkedByRequester;
                StartTastingToggleManager.BoolToggle.ToggleState = result.IsTastedByRequester;
                StartTastingToggleManager.CustomBoolToggle.ToggleState = result.IsTastedByRequester;

				Comments = new ObservableCollection<UICommentModel>(result.Comments);            
				InspectTastingModel = result;
				PageManager.IncrementPage();
			}

			PageManager.IsInitialLoadInProgress = false;
			IsDownloadingInitTastingInverse = true;
			IsRefreshing = false;
		}

		private async Task OnScrollToBottom(UICommentModel comment)
		{
			var commentIndex = Comments.IndexOf(comment);

			if(Comments.Count - 1 == commentIndex)
			{
				await DownloadComments();
			}
		}

        private async Task DownloadComments()
		{
			IsOutOfServiceTextVisible = false;
			if(!PageManager.AllEntitiesHasBeenDownloaded)
			{
				if(!PageManager.IsLoadingEntities)
				{
					PageManager.IsLoadingEntities = true;

					System.Diagnostics.Debug.WriteLine("Downloading new comment");

					var comments = await _tastingDataService.GetCommentsForContextIdPagedAsync(_initData.TastingId, PageManager.Page, PageManager.PageSize, false);
					if(comments == null)
					{
						PageManager.SetBoolFalseState();
						IsOutOfServiceTextVisible = true;
						return;
					}

					PageManager.IncrementPage();

					if(!comments.Any())
					{
						PageManager.AllEntitiesHasBeenDownloaded = true;
						PageManager.IsLoadingEntities = false;
						return;
					}

					foreach(var comment in comments)
					{
						Comments.Add(comment);
					}

					PageManager.IsLoadingEntities = false;
				}
			}
		}

        //***** OptionMenuCmds *****
		private bool IsBookmarkingInProgress { get; set; }
        private async Task OnBookmarkClicked()
		{         
			if (!App.MicroInteractionsQueManager.IsInteractionInProgress(_initData.ChampagneId))
            {
				var state = InspectTastingModel.IsBookmarkedByRequester;
				if (InspectTastingModel.IsBookmarkedByRequester)
                {
					InspectTastingModel.IsBookmarkedByRequester = false;
                }
                else
                {
					InspectTastingModel.IsBookmarkedByRequester = true;
                }

                var response = await _microInteractionService.HandleBookmark(_initData.ChampagneId, state);

                if (!response.IsSuccesfull)
                {
                    await CoreMethods.DisplayAlert("Error: ", response.Message + "\n Try Again", "OK");
                }
            }
            else
            {
				if (InspectTastingModel.IsBookmarkedByRequester) //Unlike
                {
					InspectTastingModel.IsBookmarkedByRequester = false;
                    App.MicroInteractionsQueManager.QueInteraction(_initData.ChampagneId, false);
                }
                else //LIKE
                {
					InspectTastingModel.IsBookmarkedByRequester = true;
                    App.MicroInteractionsQueManager.QueInteraction(_initData.ChampagneId, true);
                }
            }
		}


		//***** ChatEntry cmds *****
		public bool IsPostingCommentInProgress { get; set; } = false;
		public SuccesStateEnum SuccesState { get; set; } = SuccesStateEnum.Default;
        private async Task OnPostComment(string comment)
		{
			SuccesState = SuccesStateEnum.Default;
			if (IsPostingCommentInProgress)
			{
				return;
			}

			if(IsEditing)
			{

			}

			System.Diagnostics.Debug.WriteLine("Should post comment");
            //Check if comment is valid!
			if(string.IsNullOrEmpty(comment) || string.IsNullOrWhiteSpace(comment))
			{
				System.Diagnostics.Debug.WriteLine("Not a valid comment! Ignore");
			}
			else
			{
				IsPostingCommentInProgress = true;//result.Model
				var newComment = new UICommentModel
				{
					Id = Guid.NewGuid(),
					ContextId = InspectTastingModel.Id,
					ContextType = "Tasting",

					AuthorId = Guid.Empty,
					AuthorName = "Working",
					AuthorProfileImgId = Guid.Empty,
					Date = DateTime.Now,
					Comment = comment,
					NumberOfLikes = 0,
					IsLikedByRequester = false,
					IsRequesterAuthor = true,
					IsEnabled = false
				};


				Comments.Insert(0, newComment);
				var result = await _tastingDataService.PostCommentTasting(_initData.TastingId, comment);

				if(!result.IsSuccesfull)
				{
					IsPostingCommentInProgress = false;
					await CoreMethods.DisplayAlert("Error: ", result.Message + "\n Error in posting, try again", "OK");
					SuccesState = SuccesStateEnum.Error;
					Comments.Remove(newComment);
				}
				else
				{
					//Comments.Insert(0, result.Model)
					GenericMapper<UICommentModel, UICommentModel>.Map(result.Model, newComment);
					//newComment = result.Model;
				}
                            
				IsPostingCommentInProgress = false;
            
			}
		}

		//***** CommentCard cmds *****
		private bool IsDeletingComment { get; set; } = false;
		private async Task OnDeleteComment(UICommentModel comment)
		{
			if(!comment.IsEnabled)
			{
				return;
			}

			if (!IsDeletingComment)
			{
				IsDeletingComment = true;
				var respone = await CoreMethods.DisplayAlert("You are about to delete you comment?", "Once you delete the comment there is no going back!", "Yes, delete!", "Cancel");

				if (respone)
				{
					Comments.Remove(comment);
					var deleteResponse = await _tastingDataService.DeleteComment(comment.Id);

					if (!deleteResponse.IsSuccesfull)
					{
						//Do something if fails
					}
				}
				IsDeletingComment = false;
			}
		}

        //***** Edit Comment resources *****
		public bool IsEditing { get; set; } = false;
		public UICommentModel CommentToEdit { get; set; }
        //***** Edit comment *****
		private async Task OnEditComment(UICommentModel comment)
		{
			if(!IsEditing)
			{
				IsEditing = true;
				CommentToEdit = comment;
			}
		}

        private async Task OnEditCommentCancel()
		{
			IsEditing = false;
			CommentToEdit = null;
		}

		private bool IsUpdatingComment { get; set; } = false;
		private async Task OnUpdateComment(UpdateCommentEventArgs args)
		{
			if(!args.Model.IsEnabled)
			{
				return;
			}
			if(!IsUpdatingComment)
			{
				IsUpdatingComment = true;
				args.Model.Comment = args.UpdatedComment;

				var response = await _tastingDataService.EditComment(args.Model.Id, args.UpdatedComment);

				if(!response.IsSuccesfull)
				{
                    //Do something if fails
				}
			}

			IsEditing = false;
			IsUpdatingComment = false;
			CommentToEdit = null;
		}


        private async Task OnRefreshData()
		{         
			IsRefreshing = true;
			PageManager.AllEntitiesHasBeenDownloaded = false;
			PageManager.SetPage(0);
			await DownloadInitTasting();         
		}

		private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
            }
        }
    }
}
