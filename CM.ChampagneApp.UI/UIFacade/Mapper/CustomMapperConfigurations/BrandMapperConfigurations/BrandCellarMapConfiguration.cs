using System;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.DTO.Models;
using AutoMapper;
using System.Globalization;
namespace CM.ChampagneApp.UI.UIFacade.Mapper.CustomMapperConfigurations
{
    public static class BrandCellarMapConfiguration
    {
		public static UIBrandCellar Map(BrandCellarModel source)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<BrandCellarModel, UIBrandCellar>()
				.ForMember(x => x.sections, opt => opt.Ignore())
				.ForMember(x => x.HeaderImgUrl, opt => opt.Ignore());
			});

			IMapper mapper = config.CreateMapper();

			return mapper.Map<BrandCellarModel, UIBrandCellar>(source);
		}      
    }
}
