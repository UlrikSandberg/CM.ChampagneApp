using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.UIProfileCardStructure;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser.Entities;
using CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.DTO.Models.GETModels.FollowModels;
using Xamarin.Auth;
using System.Collections;
using CM.ChampagneApp.DTO.Models.GETModels.UserModels;
using CM.ChampagneApp.DTO.Models;
using CM.ChampagneApp.DTO.Builders;
using CM.ChampagneApp.DTO.Documents;
using CM.ChampagneApp.DTO.Models.UserModels;
using CM.ChampagneApp.UI.UIFacade.Mapper;
using CM.ChampagneApp.UI.UIFacade.Mapper.CustomMapperConfigurations.UserMapperConfiguration;
using CM.ChampagneApp.UI.UIFacade.Mapper.CustomMapperConfigurations.ChampagneMapperConfigurations;

namespace CM.ChampagneApp.UI.UIFacade.Services
{   
    public interface IUIUserDataService
    {
        Task<UIPublicUser> GetUser(Guid id);
        Task<UIUser> GetCurrentUser();
        Task<IEnumerable<AbstractFollowModel>> GetUserFollowers(Guid userId, int page, int pageSize);
        Task<IEnumerable<AbstractFollowModel>> GetUserFollowing(Guid userId, int page, int pageSize);
		Task<IEnumerable<AbstractFollowModel>> GetUserBrandFollowing(Guid userId, int page, int pageSize);
		Task<BaseResponse> BookmarkChampagne(Guid champagneId);
		Task<BaseResponse> UnbookmarkChampagne(Guid champagneId);
		Task<IEnumerable<UIUserCellarChampagneModel>> GetUserCellarPagedAsync(Guid userId, int page, int pageSize);
		Task<IEnumerable<UIUserCellarChampagneModel>> GetCurrentUserCellarPagedAsync(int page, int pageSize);
		Task<IEnumerable<UIUserCellarChampagneModel>> GetUserCellarSavedPagedAsync(int page, int pageSize, Guid userId);
		Task<IEnumerable<UIUserCellarChampagneModel>> GetCurrentUserCellarSavedPagedAsync(int page, int pageSize);
		Task<UIUserInfo> GetUserInfo(Guid userId);
		Task<UIUserSettings> GetCurrentUserSetting();
		Task<BaseResponse> UpdateUserSettings(UserSettingsUpdate update);
		Task<IEnumerable<AbstractFollowModel>> SearchUserByUsername(string username, int page, int pageSize);
		Task<IEnumerable<AbstractFollowModel>> SearchUsersPagedAsync(int page, int pageSize);
        Task<IEnumerable<UIUserSearchModel>> SearchUsersPagedAsyncLight(int page, int pageSize);
		Task<IEnumerable<AbstractFollowModel>> SearchUserByProfilename(string profilename, int page, int pageSize);
		Task<BaseResponse> SubmitBugAndFeedback(FeedbackAndBugSubmission feedbackAndBugSubmission);
		Task<BaseResponse> UpdateUserNotificationSettings(UserNotificationsSettingsUpdate update);
        Task<IEnumerable<UIUserSearchModel>> SearchUsersByUsernameAndProfilenamePagedAsync(string searchText, int page, int pageSize);
    }

    public class UIUserDataService : IUIUserDataService
    {
        private readonly IAppConfiguration configurationProxy;
        private readonly IUserDataService userDataService;
		private readonly IBusinessAccountService authService;

		public UIUserDataService(IAppConfiguration configurationProxy, IUserDataService userDataService, IBusinessAccountService authService)
		{
			this.authService = authService;
            this.userDataService = userDataService;
            this.configurationProxy = configurationProxy;
        }

        public async Task<UIUser> GetCurrentUser()
        {
            var result = await userDataService.GetCurrentUser();
            if(result == null)
            {
                return null;
            }

			//Map into UI objects         
			var uiResult = GenericMapper<UserModel, UIUser>.Map(result);

            uiResult.pages = new List<AbstractProfileCard>();
            //Configure profile page cards
			var page = new UIUserProfileCard();

            page.Id = Guid.NewGuid();
            page.AuthorId = uiResult.id;
            page.Title = "Cellar";
            page.Url = $"user/{uiResult.id}/cellar/{uiResult.id}";
            if (result.ImageCustomization != null)
            {
                page.CardImgId = result.ImageCustomization.cellarCardImgId;
            }

            page.Labels = new List<string>();
            page.Labels.Add(uiResult.tastedChampagnes + " Tastings");

            //Add configured profile page card
            uiResult.pages.Add(page);

            return uiResult;

        }

        public async Task<UIPublicUser> GetUser(Guid id)
        {
            
            var result = await userDataService.GetUser(id);

            if (result == null)
            {
                return null;
            }

			//Map into object
			var publicUser = GenericMapper<PublicUserModel, UIPublicUser>.Map(result);//new UIPublicUser();
            
            publicUser.IsRequesterFollowing = result.IsRequesterFollowing;

            publicUser.Pages = new List<AbstractProfileCard>();

			var page = new UIUserProfileCard();

            page.Id = Guid.NewGuid();
            page.AuthorId = publicUser.Id;
            page.Title = "Cellar";
            page.Url = $"publicuser/{publicUser.Id}/cellar/{publicUser.Id}";
            if(result.ImageCustomization != null)
            {
                page.CardImgId = result.ImageCustomization.cellarCardImgId;
            }

            page.Labels = new List<string>();
            page.Labels.Add(result.TastedChampagnes + " Bottles");

            publicUser.Pages.Add(page);
            
            return publicUser;

        }

		public async Task<IEnumerable<AbstractFollowModel>> GetUserBrandFollowing(Guid userId, int page, int pageSize)
		{
			var result = await userDataService.GetUserBrandFollowing(userId, page, pageSize);

			if(result == null)
			{
				return null;
			}

			var followingList = new List<AbstractFollowModel>();
         
			foreach(var following in result)
			{
				var nextFollowing = new UIBrandFollowModel();

				nextFollowing.Id = following.Id;
				nextFollowing.ProfileId = following.FollowToId;
				nextFollowing.ProfileName = following.FollowToName;
				nextFollowing.ProfileImgId = following.FollowToProfileImg;

				nextFollowing.IsRequesterFollowing = following.IsRequesterFollowing;

				followingList.Add(nextFollowing);
			}

			return followingList;
            
		}

		public async Task<BaseResponse> BookmarkChampagne(Guid champagneId)
	    {
		    return await userDataService.BookmarkChampagne(champagneId);   
	    }

		public async Task<BaseResponse> UnbookmarkChampagne(Guid champagneId)
	    {
		    return await userDataService.UnbookmarkChampagne(champagneId);
	    }

	    public async Task<IEnumerable<AbstractFollowModel>> GetUserFollowers(Guid userId, int page, int pageSize)
        {
			var result = await userDataService.GetUserFollowers(userId, page, pageSize);

			if(result == null)
			{
				return null;
			}

			var followList = new List<AbstractFollowModel>();
            
			var currentUserId = authService.GetId();
                     
			foreach(var follower in result)
			{
				followList.Add(MapToUIUserFollowModel.Map(follower, authService.GetId()));
			}

			return followList;
        }

        public async Task<IEnumerable<AbstractFollowModel>> GetUserFollowing(Guid userId, int page, int pageSize)
        {
            var result = await userDataService.GetUserFollowing(userId, page, pageSize);

			if(result == null)
			{
				return null;
			}

            var followingList = new List<AbstractFollowModel>();

			var currentUserId = authService.GetId();
         
            foreach (var following in result)
            {
				followingList.Add(MapToUIUserFollowModel.Map(following, authService.GetId()));
            }

            return followingList;
        }

		public async Task<IEnumerable<UIUserCellarChampagneModel>> GetUserCellarPagedAsync(Guid userId, int page, int pageSize)
		{
			var result = await userDataService.GetUserCellarPagedAsync(userId, page, pageSize);

			if(result == null)
			{
				return null;
			}

			return ConvertToUIUserCellarChampagneModel(result);
		}
        
		public async Task<IEnumerable<UIUserCellarChampagneModel>> GetCurrentUserCellarPagedAsync(int page, int pageSize)
		{
			var result = await userDataService.GetCurrentUserCellarPagedAsync(page, pageSize);

			if(result == null)
			{
				return null;
			}

			return ConvertToUIUserCellarChampagneModel(result);
		}      

		private IEnumerable<UIUserCellarChampagneModel> ConvertToUIUserCellarChampagneModel(IEnumerable<UserCellarChampagneModel> listToConvert)
		{
			var convertedList = new List<UIUserCellarChampagneModel>();
         
			foreach (var model in listToConvert)
            {
				convertedList.Add(MapToUIUserCellarChampagneModel.Map(model));
            }

			return convertedList;
		}

		public async Task<IEnumerable<UIUserCellarChampagneModel>> GetUserCellarSavedPagedAsync(int page, int pageSize, Guid userId)
		{
			var result = await userDataService.GetUserCellarSavedPagedAsync(page, pageSize, userId);

			if(result == null)
			{
				return null;
			}

			return ConvertToUIUserCellarChampagneModel(result);
		}
      
		public async Task<IEnumerable<UIUserCellarChampagneModel>> GetCurrentUserCellarSavedPagedAsync(int page, int pageSize)
		{
			var result = await userDataService.GetCurrentUserCellarSavedPagedAsync(page, pageSize);

			if(result == null)
			{
				return null;
			}

			return ConvertToUIUserCellarChampagneModel(result);

		}

		private IEnumerable<UIUserCellarChampagneModel> ConvertToUIUserCellarChampagneModel(IEnumerable<ChampagneLight> listToConvert)
		{
			var convertedItems = new List<UIUserCellarChampagneModel>();

			foreach(var champagne in listToConvert)
			{      
				convertedItems.Add(MapToUIUserCellarChampagneModel.Map(champagne));            
			}

			return convertedItems;
		}

		public async Task<UIUserInfo> GetUserInfo(Guid userId)
		{
			var result = await userDataService.GetUserInfo(userId);

            if(result == null)
			{
				return null;
			}

			return GenericMapper<UserInfo, UIUserInfo>.Map(result);
		}

		public async Task<UIUserSettings> GetCurrentUserSetting()
		{
			var result = await userDataService.GetCurrentUserSettings();

			if(result == null)
			{
				return null;
			}
			//await Task.Delay(1000);

			var model = GenericMapper<UserSettings, UIUserSettings>.Map(result);//new UIUserSettings();
         
			if(result.imageCustomization != null)
			{
				model.ImageSettings = new Models.UIUser.Entities.ImageCustomization();

				model.ImageSettings.profileCoverImgId = result.imageCustomization.ProfileCoverImgId;
				model.ImageSettings.profilePictureImgId = result.imageCustomization.ProfilePictureImgId;
				model.ImageSettings.cellarCardImgId = result.imageCustomization.CellarCardImgId;
				model.ImageSettings.cellarHeaderImgId = result.imageCustomization.CellarHeaderImgId;
			}
			else
			{
				model.ImageSettings = new Models.UIUser.Entities.ImageCustomization();
				model.ImageSettings.profileCoverImgId = Guid.Empty;
				model.ImageSettings.profilePictureImgId = Guid.Empty;
				model.ImageSettings.cellarCardImgId = Guid.Empty;
				model.ImageSettings.cellarHeaderImgId = Guid.Empty;
			}
         
			return model;
		}

		public async Task<BaseResponse> UpdateUserSettings(UserSettingsUpdate update)
		{
			return await userDataService.UpdateUserSettings(update);
		}

		public async Task<BaseResponse> UpdateUserNotificationSettings(UserNotificationsSettingsUpdate update)
		{
			return await userDataService.UpdateUserNotificationSettings(update);
		}

		public async Task<IEnumerable<AbstractFollowModel>> SearchUserByUsername(string username, int page, int pageSize)
		{
			var result = await userDataService.SearchUserByUsernamePagedAsync(username, page, pageSize);

			if(result == null)
			{
				return null;
			}

			return ConvertFollowingModelToUIUserFollowModel(result);
		}

		public async Task<IEnumerable<AbstractFollowModel>> SearchUsersPagedAsync(int page, int pageSize)
		{
			var result = await userDataService.SearchUsersPagedAsync(page, pageSize);

			if(result == null)
			{
				return null;
			}

			return ConvertFollowingModelToUIUserFollowModel(result);
            
		}

		public async Task<IEnumerable<AbstractFollowModel>> SearchUserByProfilename(string profilename, int page, int pageSize)
		{
			var result = await userDataService.SearchUserByProfilenamePagedAsync(profilename, page, pageSize);

			if(result == null)
			{
				return null;
			}

			return ConvertFollowingModelToUIUserFollowModel(result);
		}

		private IEnumerable<AbstractFollowModel> ConvertFollowingModelToUIUserFollowModel(IEnumerable<FollowingModel> listToConvert)
		{

			var convertedList = new List<AbstractFollowModel>();

			var currentUserId = authService.GetId();
         
			foreach (var model in listToConvert)
            {
				convertedList.Add(MapToUIUserFollowModel.Map(model, authService.GetId()));
            }

			return convertedList;
		}

		public async Task<BaseResponse> SubmitBugAndFeedback(FeedbackAndBugSubmission feedbackAndBugSubmission)
		{
			return await userDataService.SubmitBugAndFeedback(feedbackAndBugSubmission);
		}

        public async Task<IEnumerable<UIUserSearchModel>> SearchUsersByUsernameAndProfilenamePagedAsync(string searchText, int page, int pageSize)
        {
            var result = await userDataService.SearchUsersByProfilenameAndUsernamePagedAsync(searchText, page, pageSize);

            if(result == null)
            {
                return null;
            }

            return new List<UIUserSearchModel>(result.Select(c => GenericMapper<UserSearchModel, UIUserSearchModel>.Map(c)));
        }

        public async Task<IEnumerable<UIUserSearchModel>> SearchUsersPagedAsyncLight(int page, int pageSize)
        {
            var result = await userDataService.SearchUsersPagedAsyncLight(page, pageSize);

            if (result == null)
            {
                return null;
            }

            return new List<UIUserSearchModel>(result.Select(c => GenericMapper<UserSearchModel, UIUserSearchModel>.Map(c)));
        }
    }
}
