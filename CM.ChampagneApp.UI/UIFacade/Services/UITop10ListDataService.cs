using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.DTO.Models;
using CM.ChampagneApp.DTO.Models.GETModels.Top10ListModels;
using CM.ChampagneApp.DTO.Models.POSTModels;
using CM.ChampagneApp.UI.UIFacade.Mapper;
using CM.ChampagneApp.UI.UIFacade.Models.UITop10;

namespace CM.ChampagneApp.UI.UIFacade.Services
{
    public interface IUITop10ListDataService
    {
        Task<IEnumerable<UITop10ListCardModel>> GetTop10Lists();
        Task<IEnumerable<UITop10CardModel>> GetTop10List(string configurationKey, bool filterByVintage, bool filterByHighestRating);
    }

    public class UITop10ListDataService : IUITop10ListDataService
    {
        private readonly ITop10ListDataService _dataService;

        public UITop10ListDataService(ITop10ListDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IEnumerable<UITop10CardModel>> GetTop10List(string configurationkey, bool filterByVintage, bool filterByHighestRating)
        {
            var result = await _dataService.GetStandardTop10List(configurationkey, filterByVintage, filterByHighestRating);

            if(result == null)
            {
                return null;
            }

            var list = new List<UITop10CardModel>();

            var index = 0;
            foreach(var champagne in result)
            {
                index++;
                var mapping = GenericMapper<ChampagneLight, UITop10CardModel>.Map(champagne);
                mapping.Top10Position = index;
                list.Add(mapping);
            }

            return list;
        }

        public async Task<IEnumerable<UITop10ListCardModel>> GetTop10Lists()
        {
            var result = await _dataService.GetStandardTop10Lists();

            if(result == null)
            {
                return null;
            }

            var convertedResult = result.Select(x => GenericMapper<StandardTop10ListModel, UITop10ListCardModel>.Map(x)).ToList();

            convertedResult.ForEach(x => 
            {
                x.AugmentedTitle = x.Title.ToUpper();
                x.Subtitle = "Non-Vintage";
                x.AugmentedSubtitle = "Non-Vintage";
            });

            return convertedResult;
        }
    }
}
