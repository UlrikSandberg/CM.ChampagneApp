using System;
using System.Threading.Tasks;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Models.GETModels.TastingModels;
using Newtonsoft.Json;
using CM.ChampagneApp.DTO.Documents;
using CM.ChampagneApp.DTO.Models.POSTModels.TastingModels;
using System.Collections.Generic;
using CM.ChampagneApp.DTO.Models.Helpers.EnumOptions;
using CM.ChampagneApp.DTO.Models.GETModels.CommentModels;
using CM.ChampagneApp.DTO.Models.PUTModels.CommentsModels;
using CM.ChampagneApp.Instrumentation.Http;

namespace CM.ChampagneApp.Business.Services
{
    public interface ITastingDataService
	{
		Task<EditTastingModel> GetEditTastingModel(Guid champagneId);
		Task<BaseResponse> CreateTasting(Guid champagneId, string review, double rating);
		Task<BaseResponse> EditTasting(Guid tastingId, string review, double rating);
		Task<BaseResponse> DeleteTasting(Guid tastingId);
		Task<IEnumerable<Tasting>> GetTastingsPagedWithFilterAsync(Guid champagneId, int page, int pageSize, bool orderAcendingByDate = false);
		Task<ChampagneWithRatingAndTasting> GetChampagneWithRatingAndTasting(Guid champagneId, int page, int pageSize, TastingOrderByOptions.OrderByOptions orderByOptions);
		Task<IEnumerable<CommentModel>> GetCommentsForContextIdPagedAsync(Guid contextId, int page, int pageSize, bool orderByAcendingDate = false);
		Task<InspectTastingModel> GetInspectTasting(Guid tastingId, int page, int pageSize, bool orderAcendingByDate = false);
		Task<ResponseWithModel<CommentModel>> PostComment(Guid contextId, CommentContextTypes.ContextTypes contextTypes, string comment);
		Task<BaseResponse> EditComment(Guid commentId, string updatedComment);
		Task<BaseResponse> DeleteComment(Guid commentId);
	}

	public class TastingDataService : BaseDataService, ITastingDataService
    {
        public TastingDataService(IBusinessAccountService authService, IAuthenticationService authenticationService, IAppConfiguration appConfig, IHttpProxy httpProxy) : base(authService, authenticationService, appConfig, httpProxy)
		{
		}

		public async Task<ResponseWithModel<CommentModel>> PostComment(Guid contextId, CommentContextTypes.ContextTypes contextTypes, string comment)
		{
			var createCommentRequestModel = new CreateCommentRequestModel
			{
				ContextId = contextId,
				ContextType = contextTypes,
				Date = DateTime.Now,
				Comment = comment
			};

            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/comments");

			var json = JsonConvert.SerializeObject(createCommentRequestModel, Formatting.Indented);

			var response = await PostAsync(baseurl, json, true);

			if(!response.IsSuccessStatusCode)
			{
				return new ResponseWithModel<CommentModel>(false, null, response.ReasonPhrase);
			}

			var content = await response.Content.ReadAsStringAsync();

            return new ResponseWithModel<CommentModel>(true, JsonConvert.DeserializeObject<CommentModel>(content));
		}

        //Post
		public async Task<BaseResponse> CreateTasting(Guid champagneId, string review, double rating)
		{
			var createTastingRequestModel = new CreateTastingRequestModel
			{
				Review = review,
				Rating = rating,
				TimeStamp = DateTime.Now
			};

            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/tastings/" + champagneId.ToString());

			var json = JsonConvert.SerializeObject(createTastingRequestModel, Formatting.Indented);

			var response = await PostAsync(baseurl, json, true);

			if(!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}

			return new BaseResponse(true);

		}
        
        //Delete
		public async Task<BaseResponse> DeleteTasting(Guid tastingId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/tastings/" + tastingId.ToString());

			var response = await DeleteAsync(baseurl);

			if(!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}

			return new BaseResponse(true);

		}

        //Update
		public async Task<BaseResponse> EditTasting(Guid tastingId, string review, double rating)
		{
			var editTastingRequestModel = new EditTastingRequestModel
			{
				Review = review,
				Rating = rating
			};

            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/tastings/" + tastingId.ToString());

			var json = JsonConvert.SerializeObject(editTastingRequestModel, Formatting.Indented);

			var response = await PutAsync(baseurl, json);

			if(!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}

			return new BaseResponse(true);

		}

		public async Task<ChampagneWithRatingAndTasting> GetChampagneWithRatingAndTasting(Guid champagneId, int page, int pageSize, TastingOrderByOptions.OrderByOptions orderByOptions)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/champagnes/" + champagneId.ToString() + "/ratings?page=" + page + "&pageSize=" + pageSize + "&orderBy=" + orderByOptions.ToString());

			var response = await GetAsync(baseurl);

			return await TryReadAsync<ChampagneWithRatingAndTasting>(response);
		}

		public async Task<IEnumerable<CommentModel>> GetCommentsForContextIdPagedAsync(Guid contextId, int page, int pageSize, bool orderByAcendingDate = false)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/comments?contextId=" + contextId.ToString() + "&page=" + page + "&pageSize=" + pageSize + "&orderAcendingByDate=" + orderByAcendingDate);

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<CommentModel>>(response);
		}

		public async Task<EditTastingModel> GetEditTastingModel(Guid champagneId)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/users/currentUser/tastings/" + champagneId.ToString());

			var response = await GetAsync(baseurl);

			return await TryReadAsync<EditTastingModel>(response);
		}

		public async Task<InspectTastingModel> GetInspectTasting(Guid tastingId, int page, int pageSize, bool orderAcendingByDate = false)
		{
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/tastings/" + tastingId.ToString() + "/inspectTasting?page=" + page + "&pageSize=" + pageSize + "&acendingOrderByDate=" + orderAcendingByDate);

			var response = await GetAsync(baseurl);

			return await TryReadAsync<InspectTastingModel>(response);
		}

		public async Task<IEnumerable<Tasting>> GetTastingsPagedWithFilterAsync(Guid champagneId, int page, int pageSize, bool orderAcendingByDate = false)
		{
			//http://localhost:49991/api/v1/champagnes/00000000-0000-0000-0000-000000000000/tastings?page=0&pageSize=2&orderAcendingByDate=true
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/champagnes/" + champagneId.ToString() + "/tastings?page=" + page + "&pageSize=" + pageSize + "&orderAcendingByDate=" + orderAcendingByDate);

			var response = await GetAsync(baseurl);

			return await TryReadAsync<IEnumerable<Tasting>>(response);
		}

		public async Task<BaseResponse> EditComment(Guid commentId, string updatedComment)
		{
	         
			var editCommentRequestModel = new EditCommentRequestModel
			{
				Content = updatedComment
			};
         
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/comments/" + commentId.ToString());

			var json = JsonConvert.SerializeObject(editCommentRequestModel, Formatting.Indented);

			var response = await PutAsync(baseurl, json);

			if(!response.IsSuccessStatusCode)
			{
				return new BaseResponse(true, response.ReasonPhrase);
			}

			return new BaseResponse(true);

		}

		public async Task<BaseResponse> DeleteComment(Guid commentId)
		{
			         
            var baseurl = new Uri(AppConfig.ApiBaseUrl, "/api/v1/comments/" + commentId.ToString());

			var response = await DeleteAsync(baseurl);

			if(!response.IsSuccessStatusCode)
			{
				return new BaseResponse(false, response.ReasonPhrase);
			}

			return new BaseResponse(true);
		}
	}
}
