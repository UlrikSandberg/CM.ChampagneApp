using System;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.DTO.Models.Helpers.EnumOptions;
using CM.ChampagneApp.DTO.Documents;

namespace CM.ChampagneApp.UI.UIFacade.Services
{
    public interface IUIFollowLikeServie
	{
		Task<BaseResponse> FollowBrand(Guid brandId);
		Task<BaseResponse> UnfollowBrand(Guid brandId);
		Task<BaseResponse> FollowUser(Guid userId);
		Task<BaseResponse> UnfollowUser(Guid userId);
		Task<BaseResponse> LikeTasting(Guid contextId);
		Task<BaseResponse> LikeComment(Guid contextId);
		Task<BaseResponse> UnlikeEntity(Guid contextId);
	}

	public class UIFollowLikeService : IUIFollowLikeServie
    {
	    private readonly IFollowLikeService _followLikeService;

		public UIFollowLikeService()
		{
		}

		public UIFollowLikeService(IFollowLikeService followLikeService)
	    {
		    _followLikeService = followLikeService;
	    }
        /// <summary>
        /// Follows the brand.
        /// </summary>
        /// <returns>The brand.</returns>
        /// <param name="brandId">Brand identifier.</param>
		public async Task<BaseResponse> FollowBrand(Guid brandId)
	    {
		   	return await _followLikeService.FollowBrand(brandId);
	    }
        /// <summary>
        /// Follows the user.
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="userId">User identifier.</param>
		public async Task<BaseResponse> FollowUser(Guid userId)
        {
	        return await _followLikeService.FollowUser(userId);   
        }

		public async Task<BaseResponse> LikeComment(Guid contextId)
		{
			return await _followLikeService.LikeEntity(contextId, LikeContextTypes.contextTypes.Comment);         
		}

		public async Task<BaseResponse> LikeTasting(Guid contextId)
		{
			return await _followLikeService.LikeEntity(contextId, LikeContextTypes.contextTypes.Tasting);
        }

		/// <summary>
		/// Unfollows the brand.
		/// </summary>
		/// <returns>The brand.</returns>
		/// <param name="brandId">Brand identifier.</param>
		public async Task<BaseResponse> UnfollowBrand(Guid brandId)
	    {
			return await _followLikeService.UnfollowBrand(brandId);
	    }

        /// <summary>
        /// Unfollows the user. Not implemented yet
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="userId">User identifier.</param>
		public async Task<BaseResponse> UnfollowUser(Guid userId)
        {
	        return await _followLikeService.UnfollowUser(userId);
        }

		public async Task<BaseResponse> UnlikeEntity(Guid contextId)
		{
			return await _followLikeService.UnlikeEntity(contextId);
		}
	}
}
