using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using System.Collections;
using CM.ChampagneApp.DTO.Builders;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using AutoMapper;
using CM.ChampagneApp.DTO.Models.GETModels.TastingModels;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;
using CM.ChampagneApp.DTO.Models.GETModels.ChampagneModels;
using CM.ChampagneApp.UI.UIFacade.Mapper;
using CM.ChampagneApp.UI.UIFacade.Mapper.CustomMapperConfigurations.ChampagneMapperConfigurations;

namespace CM.ChampagneApp.UI.UIFacade.Services
{

    public interface IUIChampagneDataService
    {
        Task<IEnumerable<UIChampagneRoot>> GetChampagneRoots(int page, int pageSize);
        Task<UIChampagne> GetChampagneProfile(Guid id);
        Task<IEnumerable<UIChampagneLight>> GetBottlesFromRoot(Guid champagneRootId);
        Task<IEnumerable<UIChampagneRoot>> GetBrandChampagneRoots(Guid brandId);
        Task<IEnumerable<UIChampagneRoot>> GetBrandRootsShuffled(Guid brandId, bool isShuffled, int amount);
        Task<IEnumerable<UIChampagneRoot>> GetChampagneRootsShuffled(bool isShuffled, int amount);
		Task<IEnumerable<UIUserCellarChampagneModel>> GetChampagnesByFilterAsync(FilterSearchQuery filterSearchQuery, int page, int pageSize);
        Task<IEnumerable<UIChampagneSearchModel>> SearchChampagnes(string searchText, int page, int pageSize, bool forgetCurrentActiveCalls = false);
	}


    public class UIChampagneDataService : IUIChampagneDataService
    {
        private readonly IChampagneDataService champagneDataService;
        private readonly IAppConfiguration configurationProxy;
		private readonly ITastingDataService tastingDataService;

        private static Dictionary<string, Guid> ActiveCalls = new Dictionary<string, Guid>();

        public UIChampagneDataService(IChampagneDataService champagneDataService, IAppConfiguration configurationProxy, ITastingDataService tastingDataService)
		{
			this.tastingDataService = tastingDataService;
            this.configurationProxy = configurationProxy;
            this.champagneDataService = champagneDataService;
        }

        public async Task<IEnumerable<UIChampagneLight>> GetBottlesFromRoot(Guid champagneRootId)
        {
            var result = await champagneDataService.GetBottlesFromRoot(champagneRootId);
            
			if(result == null)
			{
				return null;
			}

            //New list to add mapped result;
            var uiResultList = new List<UIChampagneLight>();

            //Map each result in result;
            foreach(var champagne in result)
            {
                if(champagne.Id != champagneRootId)
                {
                    uiResultList.Add(GenericMapper<ChampagneLight, UIChampagneLight>.Map(champagne));
                }
            }

            return uiResultList;
        }

        public async Task<IEnumerable<UIChampagneRoot>> GetBrandChampagneRoots(Guid brandId)
        {
            var result = await champagneDataService.GetBrandChampagneRoots(brandId);

			if(result == null)
			{
				return null;
			}

            var uiList = new List<UIChampagneRoot>();

            foreach(var root in result)
            {
				uiList.Add(GenericMapper<ChampagneRoot, UIChampagneRoot>.Map(root));            
            }
            return uiList;
        }

        public async Task<IEnumerable<UIChampagneRoot>> GetBrandRootsShuffled(Guid brandId, bool isShuffled, int amount)
        {
            var result = await champagneDataService.GetBrandRootsShuffled(brandId, isShuffled, amount);

			if(result == null)
			{
				return null;
			}

			var uiList = new List<UIChampagneRoot>();
            
            foreach(var root in result)
            {
				uiList.Add(GenericMapper<ChampagneRoot, UIChampagneRoot>.Map(root));
            }

            return uiList;
        }

        public async Task<UIChampagne> GetChampagneProfile(Guid id)
        {
            //Result from backend
            var result = await champagneDataService.GetChampagenProfile(id);

			if(result == null)
			{
				return null;
			}

			var uiChampagne = GenericMapper<Champagne, UIChampagne>.Map(new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Champagne, UIChampagne>()
				.ForMember(x => x.RequesterTasting, opt => opt.Ignore())
				.ForMember(x => x.IsVintage, opt => opt.MapFrom(src => src.vintageInfo.isVintage))
				.ForMember(x => x.Year, opt => opt.MapFrom(src => src.vintageInfo.year))
				.ForMember(x => x.TechnicalIdentities, opt => opt.Ignore());
			}), result);
            
			if(result.IsTastedByRequester)//There is a tasting
			{
				if(result.RequesterTasting != null)
				{
					uiChampagne.RequesterTasting = GenericMapper<Tasting, UITasting>.Map(result.RequesterTasting);               
				}
				else
				{
					uiChampagne.RequesterTasting = null;
				}
			}
			else
			{
				uiChampagne.RequesterTasting = null;
			}
            
			return uiChampagne;
        }

		public async Task<IEnumerable<UIChampagneRoot>> GetChampagneRoots(int page, int pageSize)
        {
            //Result from backend
            var result = await champagneDataService.GetChampagneRoots(page, pageSize);

			if (result == null)
			{
				return null;
			}

			var uiList = new List<UIChampagneRoot>();

			foreach (var root in result)
			{            
				uiList.Add(GenericMapper<ChampagneRoot, UIChampagneRoot>.Map(root));
			}

			return uiList;
        }

        public async Task<IEnumerable<UIChampagneRoot>> GetChampagneRootsShuffled(bool isShuffled, int amount)
        {
            var result = await champagneDataService.GetChampagneRootsShuffled(isShuffled, amount);

			if(result == null)
			{
				return null;
			}

            var uiChampagneRootList = new List<UIChampagneRoot>();

            foreach(var root in result)
            {
				uiChampagneRootList.Add(GenericMapper<ChampagneRoot, UIChampagneRoot>.Map(root));
            }

            return uiChampagneRootList;
        }

		public async Task<IEnumerable<UIUserCellarChampagneModel>> GetChampagnesByFilterAsync(FilterSearchQuery filterSearchQuery, int page, int pageSize)
		{
			var result = await champagneDataService.GetChampagnesByFilterAsync(filterSearchQuery, page, pageSize);         
         
			if(result == null)
			{
				return null;
			}

			return ConvertToUIUserCellarChampagneModel(result);
		}

        public async Task<IEnumerable<UIChampagneSearchModel>> SearchChampagnes(string searchText, int page, int pageSize, bool forgetCurrentActiveCalls)
        {
            //Since we should forgetCurrentActive calls we should track all the calls
            Console.WriteLine("Start load: " + searchText);
            if(forgetCurrentActiveCalls)
            {
                var callId = Guid.NewGuid();
                App.ActiveCallTracker.AddCall(nameof(SearchChampagnes), callId);
            }

            var result = await champagneDataService.SearchChampagnes(searchText, page, pageSize);

            if(result == null)
            {
                return null;
            }

            //Check if we should forget call, if app made a new call since this callId invocation
            if(forgetCurrentActiveCalls)
            {

            }

            var convertedItems = new List<UIChampagneSearchModel>();

            foreach(var searchResult in result)
            {
                convertedItems.Add(GenericMapper<ChampagneSearchModel, UIChampagneSearchModel>.Map(searchResult));
            }

            return convertedItems;
        }

        private IEnumerable<UIUserCellarChampagneModel> ConvertToUIUserCellarChampagneModel(IEnumerable<ChampagneLight> listToConvert, bool isSavedModel = true)
        {
            var convertedItems = new List<UIUserCellarChampagneModel>();
            
			foreach (var champagne in listToConvert)
			{
				convertedItems.Add(MapToUIUserCellarChampagneModel.Map(champagne, isSavedModel));
			}

            return convertedItems;
        }
	}
}
