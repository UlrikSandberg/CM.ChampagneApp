using System;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Documents;
using System.Collections.Generic;
using CM.ChampagneApp.DTO.Models.Helpers.EnumOptions;
using CM.ChampagneApp.DTO.Models.GETModels.TastingModels;
using System.Collections;
using CM.ChampagneApp.UI.UIFacade.Models.UICommentModels;
using CM.ChampagneApp.DTO.Models.GETModels.CommentModels;
using CM.ChampagneApp.Acq;
using AutoMapper;
using CM.ChampagneApp.UI.UIFacade.Mapper;

namespace CM.ChampagneApp.UI.UIFacade.Services
{
    public interface IUITastingDataService
    {
        Task<UIEditTastingModel> GetCurrentUserTastingForEdit(Guid champagneId);
        Task<BaseResponse> CreateTasting(Guid champagneId, string review, double rating);
        Task<BaseResponse> EditTasting(Guid tastingId, string review, double rating);
        Task<BaseResponse> DeleteTasting(Guid tastingId);
        Task<IEnumerable<UITasting>> GetTastingsPagedWithFilterAsync(Guid champagneId, int page, int pageSize, bool orderByAcendingOrder);
        Task<UIChampagneWithRatingAndTasting> GetChampagneWithRatingAndTasting(Guid champagneId, int page, int pageSize, TastingOrderByOptions.OrderByOptions orderBy);
		Task<UIInspectTastingModel> GetInspectTasting(Guid tastingId, int page, int pageSize, bool orderAcendingByDate = false);
		Task<IEnumerable<UICommentModel>> GetCommentsForContextIdPagedAsync(Guid contextId, int page, int pageSize, bool orderAcendingByDate = false);
		Task<ResponseWithModel<UICommentModel>> PostCommentTasting(Guid contextId, string comment);
		Task<BaseResponse> EditComment(Guid commentId, string updatedComment);
		Task<BaseResponse> DeleteComment(Guid commentId);
    }

    public class UITastingDataService : IUITastingDataService
    {
        private readonly ITastingDataService tastingDataService;
        private readonly IAppConfiguration configurationProxy;
		private readonly IBusinessAccountService bussAuthService;

		public UITastingDataService(ITastingDataService tastingDataService, IAppConfiguration configurationProxy, IBusinessAccountService bussAuthService)
		{
			this.bussAuthService = bussAuthService;
            this.configurationProxy = configurationProxy;
            this.tastingDataService = tastingDataService;
        }

        public async Task<BaseResponse> CreateTasting(Guid champagneId, string review, double rating)
        {
            return await tastingDataService.CreateTasting(champagneId, review, rating);
        }

        public async Task<BaseResponse> DeleteTasting(Guid tastingId)
        {
            return await tastingDataService.DeleteTasting(tastingId);
        }

        public async Task<BaseResponse> EditTasting(Guid tastingId, string review, double rating)
        {
            return await tastingDataService.EditTasting(tastingId, review, rating);
        }

        public async Task<UIChampagneWithRatingAndTasting> GetChampagneWithRatingAndTasting(Guid champagneId, int page, int pageSize, TastingOrderByOptions.OrderByOptions orderBy)
        {
			var result = await tastingDataService.GetChampagneWithRatingAndTasting(champagneId, page, pageSize, orderBy);

			if(result == null)
			{
				return null;
			}

			var uiTastingModel = GenericMapper<ChampagneWithRatingAndTasting, UIChampagneWithRatingAndTasting>.Map(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ChampagneWithRatingAndTasting, UIChampagneWithRatingAndTasting>()
				.ForMember(x => x.Tastings, opt => opt.Ignore())
				.ForMember(x => x.RatingByStars, opt => opt.Ignore());            
			}), result);

			uiTastingModel.Tastings = convertToUITasting(result.Tastings);
			uiTastingModel.RatingByStars = makeRatingByStars(result.Ratings);

			return uiTastingModel;

        }

		public async Task<IEnumerable<UICommentModel>> GetCommentsForContextIdPagedAsync(Guid contextId, int page, int pageSize, bool orderAcendingByDate = false)
		{
			var result = await tastingDataService.GetCommentsForContextIdPagedAsync(contextId, page, pageSize, orderAcendingByDate);

			return ConvertComments(result);
		}

		public async Task<UIEditTastingModel> GetCurrentUserTastingForEdit(Guid champagneId)
        {
            var result = await tastingDataService.GetEditTastingModel(champagneId);
         
            if(result == null)
            {
                return null;
            }

			//Map into UIEditTastingModel
			var editTastingModel = GenericMapper<EditTastingModel, UIEditTastingModel>.Map(result);//new UIEditTastingModel();

            if(result.IsTastingNull)
            {
                editTastingModel.Id = Guid.Empty;
                editTastingModel.Review = null;
                editTastingModel.Rating = 0.0;
                editTastingModel.IsTastingNull = result.IsTastingNull;
            }
            else
            {
                editTastingModel.Id = result.Id;
                editTastingModel.Review = result.Review;
                editTastingModel.Rating = result.Rating;
                editTastingModel.IsTastingNull = result.IsTastingNull;
            }

            return editTastingModel;
        }

		public async Task<UIInspectTastingModel> GetInspectTasting(Guid tastingId, int page, int pageSize, bool orderAcendingByDate = false)
		{
			var result = await tastingDataService.GetInspectTasting(tastingId, page, pageSize, orderAcendingByDate);

			if(result == null)
			{
				return null;
			}

			var uiInspectTasting = GenericMapper<InspectTastingModel, UIInspectTastingModel>.Map(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<InspectTastingModel, UIInspectTastingModel>()
				.ForMember(x => x.Tasting, opt => opt.Ignore())
				.ForMember(x => x.Comments, opt => opt.Ignore());
			}), result);

			uiInspectTasting.Tasting = GenericMapper<InspectTastingModel, UITasting>.Map(result);

			uiInspectTasting.Comments = ConvertComments(result.Comments);

			return uiInspectTasting;

		}

		public async Task<IEnumerable<UITasting>> GetTastingsPagedWithFilterAsync(Guid champagneId, int page, int pageSize, bool orderByAcendingOrder = false)
        {
            var result = await tastingDataService.GetTastingsPagedWithFilterAsync(champagneId, page, pageSize, orderByAcendingOrder);

			if(result == null)
			{
				return null;
			}

			var uiTastingList = convertToUITasting(result);         

            return uiTastingList;

        }


		private IEnumerable<UITasting> convertToUITasting(IEnumerable<Tasting> tastings)
		{
			var uiTastingList = new List<UITasting>();

			foreach (var tasting in tastings)
            {            
				uiTastingList.Add(GenericMapper<Tasting, UITasting>.Map(tasting));
            }

			return uiTastingList;
		}

		private List<UICommentModel> ConvertComments(IEnumerable<CommentModel> comments)
		{
			var convertedList = new List<UICommentModel>();

            foreach (var comment in comments)
            {
				var uiComment = GenericMapper<CommentModel, UICommentModel>.Map(comment);
                            
				if(uiComment.AuthorId.Equals(bussAuthService.GetId()))
				{
					uiComment.IsRequesterAuthor = true;
				}
				else
				{
					uiComment.IsRequesterAuthor = false;
				}

				convertedList.Add(uiComment);
            }

			return convertedList;
		}

        private List<int> makeRatingByStars(List<double> ratings)
		{
			var ratingByStarList = new List<int> {0,0,0,0,0};

            foreach(var rating in ratings)
			{
                var rounded = (int)rating;
				if(rating == 0 || rating == 0.5)
				{
					ratingByStarList[0] += 1;
				}
				else
				{
					ratingByStarList[(int)rating - 1] += 1;
				}
			}

			return ratingByStarList;
		}

		public async Task<ResponseWithModel<UICommentModel>> PostCommentTasting(Guid contextId, string comment)
		{
			var result = await tastingDataService.PostComment(contextId, CommentContextTypes.ContextTypes.Tasting, comment);
			await Task.Delay(5000);
			if(!result.IsSuccesfull)
			{
				return new ResponseWithModel<UICommentModel>(false, null, result.Message, result.Exception);
			}

			//Map from CommentModel to UICommentModel         
			var uiCommentModel = GenericMapper<CommentModel, UICommentModel>.Map(result.Model);

			if (uiCommentModel.AuthorId.Equals(bussAuthService.GetId()))
            {
                uiCommentModel.IsRequesterAuthor = true;
            }
            else
            {
                uiCommentModel.IsRequesterAuthor = false;
            }

			return new ResponseWithModel<UICommentModel>(true, uiCommentModel);         
		}

		public async Task<BaseResponse> EditComment(Guid commentId, string updatedComment)
		{
			return await tastingDataService.EditComment(commentId, updatedComment);
		}

		public async Task<BaseResponse> DeleteComment(Guid commentId)
		{
			return await tastingDataService.DeleteComment(commentId);  
		}
	}
}
