using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Models;
using CM.ChampagneApp.DTO.Models.GETModels.FollowModels;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure;
using CM.ChampagneApp.UI.UIFacade.Models.UIProfileCardStructure;
using static CM.ChampagneApp.DTO.Models.BrandCellarModel;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.UI.UIFacade.Models.UIBrandModels;
using CM.ChampagneApp.UI.UIFacade.Models.UIImageModels;
using StructureMap.AutoMocking;
using AutoMapper;
using CM.ChampagneApp.DTO.Models.GETModels.BrandModels;
using System.Globalization;
using CM.ChampagneApp.UI.UIFacade.Mapper;

namespace CM.ChampagneApp.UI.UIFacade.Services
{

    public interface IUIBrandDataService
	{

		Task<IEnumerable<UIBrand>> GetBrandsPaged(int page, int pageSize, bool includeUnpublished, bool sortAscending);
		Task<UIBrandProfile> GetBrandProfile(Guid brandId);
		Task<UIBrandCellar> GetBrandCellar(Guid brandId);
		Task<IEnumerable<AbstractFollowModel>> GetBrandFollowers(Guid brandId, int page, int pageSize);
		Task<UIBrandInfoPageModel> GetBrandInfoPageAsync(Guid brandId, Guid infoPageId);
        Task<IEnumerable<UIBrandSearchModel>> SearchBrands(string searchText, int page, int pageSize);

	}

	public class UIBrandDataService : IUIBrandDataService
    {
		private readonly IBrandDataService brandDataService;
		private readonly IAppConfiguration configurationProxy;
		private readonly IBusinessAccountService authService;

		public UIBrandDataService(IBrandDataService brandDataService, IAppConfiguration configurationProxy, IBusinessAccountService authService)
        {
            this.authService = authService;
			this.configurationProxy = configurationProxy;
			this.brandDataService = brandDataService;
		}

		public async Task<UIBrandCellar> GetBrandCellar(Guid brandId)
		{
			var result = await brandDataService.GetBrandCellar(brandId);

			if(result == null)
			{
				return null;
			}
         
			var uiBrandCellar = GenericMapper<BrandCellarModel, UIBrandCellar>.Map(result);
			uiBrandCellar.BrandId = brandId;
			uiBrandCellar.Url = uiBrandCellar.Url.ToLower();
            
			return uiBrandCellar;
            
		}

		public async Task<IEnumerable<AbstractFollowModel>> GetBrandFollowers(Guid brandId, int page, int pageSize)
		{
            var result = await brandDataService.GetBrandFollowers(brandId, page, pageSize);

			if(result == null)
			{
				return null;
			}

			var followList = new List<AbstractFollowModel>();

			var currentUserId = authService.GetId();
         
			foreach (var follower in result)
			{
				var nextFollower = new UIUserFollowModel();
			            
				nextFollower.Id = follower.Id;
				nextFollower.ProfileId = follower.FollowById;            
				nextFollower.ProfileImgId = follower.FollowByProfileImgId;
				if (currentUserId.Equals(follower.FollowById))
				{
					nextFollower.ProfileName = follower.FollowByName + " (You)";
					nextFollower.IsEnabled = false;
				}
				else
				{
					nextFollower.ProfileName = follower.FollowByName;
				}
				nextFollower.IsRequesterFollowing = follower.IsRequesterFollowing;

				followList.Add(nextFollower);

			}
			
			return followList;

		}

		public async Task<UIBrandInfoPageModel> GetBrandInfoPageAsync(Guid brandId, Guid infoPageId)
		{
			var result = await brandDataService.GetBrandInfoPageAsync(brandId, infoPageId);
                     
			if(result == null)
			{
				return null;
			}

			//UImodel to map into
			var model = GenericMapper<BrandInfoPageModel, UIBrandInfoPageModel>.Map(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<BrandInfoPageModel, UIBrandInfoPageModel>().ForMember(x => x.Sections, opt => opt.Ignore());
			}), result);

			foreach(var section in result.Sections)
			{
				var sectionModel = GenericMapper<BrandInfoPageSectionModel, UIBrandInfoPageSection>.Map(section);
				if (sectionModel.Body != null)
				{
					sectionModel.Body = (section.Body).Replace("\n", Environment.NewLine);
				}
				model.Sections.Add(sectionModel);
			}

			return model;

		}

		public async Task<UIBrandProfile> GetBrandProfile(Guid brandId)
		{
			var result = await brandDataService.GetBrandProfile(brandId);

			if(result == null)
			{
				return null;
			}

			var uiBrandProfile = GenericMapper<BrandProfile, UIBrandProfile>.Map(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<BrandProfile, UIBrandProfile>()
				.ForMember(x => x.Pages, opt => opt.Ignore())
				.ForMember(x => x.Social, opt => opt.Ignore());
			}), result);
            
			uiBrandProfile.Social = GenericMapper<BrandProfile.Social, UIBrandProfile.UISocial>.Map(result.social);
                    
			//Convert cellar to UIBrandProfileCard!         
			uiBrandProfile.Pages.Add(GenericMapper<BrandProfile.Cellar, UIBrandProfileCard>.Map(new MapperConfiguration(cfg => 
			{
				cfg.CreateMap<BrandProfile.Cellar, UIBrandProfileCard>()
				.ForMember(x => x.AuthorId, opt => opt.MapFrom(src => brandId))
				.ForMember(x => x.Url, opt => opt.MapFrom(src => src.Url.ToLower()))
				.ForMember(x => x.Id, opt => opt.Ignore());
			}), result.cellar));

            //Convert and add pages to list of pages after cellar page
			foreach(var cardModel in result.Pages)
			{
				uiBrandProfile.Pages.Add(GenericMapper<ProfileCardModel, UIBrandProfileCard>.Map(new MapperConfiguration(cfg =>
				{
					cfg.CreateMap<ProfileCardModel, UIBrandProfileCard>().ForMember(x => x.AuthorId, opt => opt.MapFrom(src => brandId));
				}), cardModel));
			}

			return uiBrandProfile;
		}

		public async Task<IEnumerable<UIBrand>> GetBrandsPaged(int page, int pageSize, bool includeUnpublished, bool sortAscending)
		{
            var result = await brandDataService.GetBrandsPaged(page, pageSize, includeUnpublished, sortAscending);

			if (result == null)
            {
                return null;
            }

			var uiList = new List<UIBrand>();

			foreach(var brand in result)
			{
				uiList.Add(GenericMapper<BrandLight, UIBrand>.Map(brand));
			}

			return uiList;
		}

        public async Task<IEnumerable<UIBrandSearchModel>> SearchBrands(string searchText, int page, int pageSize)
        {
            var result = await brandDataService.SearchBrands(searchText, page, pageSize);

            if(result == null)
            {
                return null;
            }
            
            return result.Select(c => GenericMapper<BrandSearchModel, UIBrandSearchModel>.Map(c));
        }
    }
}
