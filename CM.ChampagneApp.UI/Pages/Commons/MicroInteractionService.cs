using System;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using System.Threading.Tasks;
using CM.ChampagneApp.DTO.Documents;
namespace CM.ChampagneApp.UI.FreshMvvmHelpers
{   
    public interface IMicroInteractionService
	{
		Task<BaseResponse> HandleLike(Guid contextId, bool interactionState, bool isTasting = true);
		Task<BaseResponse> HandleFollow(Guid contextId, bool interactionState, bool isUser = true);
		Task<BaseResponse> HandleBookmark(Guid champagneId, bool interactionState);
	}
   
	public class MicroInteractionService : IMicroInteractionService
    {
		private readonly IUIFollowLikeServie followLikeService;
		private readonly IUIUserDataService userDataService;

		public MicroInteractionService(IUIFollowLikeServie followLikeService, IUIUserDataService userDataService)
        {
            this.userDataService = userDataService;
			this.followLikeService = followLikeService;
		}


		public async Task<BaseResponse> HandleLike(Guid contextId, bool interactionState, bool isTasting = true)
		{
			App.MicroInteractionsQueManager.StartInteraction(contextId);
			if(interactionState) //Unlike
			{
				var response = await followLikeService.UnlikeEntity(contextId);

				if(!response.IsSuccesfull)
				{
					App.MicroInteractionsQueManager.ClearInteractionManagerFromId(contextId);
					return new BaseResponse(false, response.Message);
				}

				//The user has succesfully unliked the entity
				//Check microInteractionsQue to see if the ui has changed state since
				var queState = App.MicroInteractionsQueManager.IsInteractionInQue(contextId);
				if(queState != null)
				{
					if (queState.State)
                    {
                        App.MicroInteractionsQueManager.StartInteraction(contextId);
						return await HandleLike(contextId, false, isTasting);
                    }
				}            

				App.MicroInteractionsQueManager.EndInteraction(contextId);
				return new BaseResponse(true);

			}
			else //Like
			{
				BaseResponse response = null;
				if (isTasting)
				{
					response = await followLikeService.LikeTasting(contextId);
				}
				else
				{
					response = await followLikeService.LikeComment(contextId);
				}

				if(!response.IsSuccesfull)
				{
					App.MicroInteractionsQueManager.ClearInteractionManagerFromId(contextId);
					return new BaseResponse(false, response.Message);
				}

				//The user has succesfully liked the entity
				//Check microInteractionsQue to see if the ui has changed state since
				var queState = App.MicroInteractionsQueManager.IsInteractionInQue(contextId);
				if(queState != null)
				{
					if (queState.State)
                    {
                        App.MicroInteractionsQueManager.StartInteraction(contextId);
						return await HandleLike(contextId, true, isTasting);
                    }
				}

				App.MicroInteractionsQueManager.EndInteraction(contextId);
				return new BaseResponse(true);
			}
		}

		public async Task<BaseResponse> HandleFollow(Guid contextId, bool interactionState, bool isUser = true)
        {
            App.MicroInteractionsQueManager.StartInteraction(contextId);
            if (interactionState) //Unfollow
            {
				BaseResponse response = null;
                if (isUser)
                {
					response = await followLikeService.UnfollowUser(contextId);
                }
                else
                {
					response = await followLikeService.UnfollowBrand(contextId);
                }        

                if (!response.IsSuccesfull)
                {
                    App.MicroInteractionsQueManager.ClearInteractionManagerFromId(contextId);
                    return new BaseResponse(false, response.Message);
                }

                //The user has succesfully unliked the entity
                //Check microInteractionsQue to see if the ui has changed state since
                var queState = App.MicroInteractionsQueManager.IsInteractionInQue(contextId);
                if (queState != null)
                {
                    if (queState.State)
                    {
                        App.MicroInteractionsQueManager.StartInteraction(contextId);
						return await HandleFollow(contextId, false, isUser);
                    }
                }

                App.MicroInteractionsQueManager.EndInteraction(contextId);
                return new BaseResponse(true);

            }
            else //Follow
            {
                BaseResponse response = null;
				if (isUser)
                {
					response = await followLikeService.FollowUser(contextId);
                }
                else
                {
					response = await followLikeService.FollowBrand(contextId);
                }

                if (!response.IsSuccesfull)
                {
                    App.MicroInteractionsQueManager.ClearInteractionManagerFromId(contextId);
                    return new BaseResponse(false, response.Message);
                }

                //The user has succesfully liked the entity
                //Check microInteractionsQue to see if the ui has changed state since
                var queState = App.MicroInteractionsQueManager.IsInteractionInQue(contextId);
                if (queState != null)
                {
                    if (queState.State)
                    {
                        App.MicroInteractionsQueManager.StartInteraction(contextId);
						return await HandleFollow(contextId, true, isUser);
                    }
                }

                App.MicroInteractionsQueManager.EndInteraction(contextId);
                return new BaseResponse(true);
            }
        }

		public async Task<BaseResponse> HandleBookmark(Guid champagneId, bool interactionState)
        {
            App.MicroInteractionsQueManager.StartInteraction(champagneId);
            if (interactionState) //Unbookmark
            {
				var response = await userDataService.UnbookmarkChampagne(champagneId);

                if (!response.IsSuccesfull)
                {
                    App.MicroInteractionsQueManager.ClearInteractionManagerFromId(champagneId);
                    return new BaseResponse(false, response.Message);
                }

                //The user has succesfully unliked the entity
                //Check microInteractionsQue to see if the ui has changed state since
                var queState = App.MicroInteractionsQueManager.IsInteractionInQue(champagneId);
                if (queState != null)
                {
                    if (queState.State)
                    {
                        App.MicroInteractionsQueManager.StartInteraction(champagneId);
						return await HandleBookmark(champagneId, false);
                    }
                }

                App.MicroInteractionsQueManager.EndInteraction(champagneId);
                return new BaseResponse(true);

            }
            else //Follow
            {
				var response = await userDataService.BookmarkChampagne(champagneId);

                if (!response.IsSuccesfull)
                {
                    App.MicroInteractionsQueManager.ClearInteractionManagerFromId(champagneId);
                    return new BaseResponse(false, response.Message);
                }

                //The user has succesfully liked the entity
                //Check microInteractionsQue to see if the ui has changed state since
                var queState = App.MicroInteractionsQueManager.IsInteractionInQue(champagneId);
                if (queState != null)
                {
                    if (queState.State)
                    {
                        App.MicroInteractionsQueManager.StartInteraction(champagneId);
						return await HandleBookmark(champagneId, true);
                    }
                }

                App.MicroInteractionsQueManager.EndInteraction(champagneId);
                return new BaseResponse(true);
            }
        }
	}
}
