using System;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using CM.ChampagneApp.DTO.Models;
using AutoMapper;
using CM.ChampagneApp.UI.Elements.Cards;
using CM.ChampagneApp.DTO.Models.GETModels.UserModels;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.UIFacade.Mapper.CustomMapperConfigurations.ChampagneMapperConfigurations
{   
	public static class MapToUIUserCellarChampagneModel 
	{
		public static UIUserCellarChampagneModel Map(ChampagneLight source, bool isSavedModel = true)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<ChampagneLight, UIUserCellarChampagneModel>()
				.ForMember(x => x.IsSavedModel, opt => opt.MapFrom(src => isSavedModel))
				.ForMember(x => x.Dosage, opt => opt.MapFrom(src => ""))
				.ForMember(x => x.ChampagneId, opt => opt.MapFrom(scr => scr.Id))
				.ForMember(x => x.IsVintage, opt => opt.MapFrom(scr => scr.getVintageInfo.isVintage))
				.ForMember(x => x.Year, opt => opt.MapFrom(scr => scr.getVintageInfo.year));
			});

            IMapper mapper = config.CreateMapper();

			var model = mapper.Map<ChampagneLight, UIUserCellarChampagneModel>(source);

			if (source.NumberOfTastings == 0 || source.RatingSumOfTastings == 0)
            {
                model.PersonalRating = 0.0;
                model.NumberOfTastings = 0;
            }
            else
            {
                model.PersonalRating = source.RatingSumOfTastings / source.NumberOfTastings;
                model.NumberOfTastings = (int)source.NumberOfTastings;
            }

			return model;
		}

		public static UIUserCellarChampagneModel Map(UserCellarChampagneModel source, bool isSavedModel = false)
		{
			var config = new MapperConfiguration(cfg =>
            {
				cfg.CreateMap<UserCellarChampagneModel, UIUserCellarChampagneModel>()
				.ForMember(x => x.IsSavedModel, opt => opt.MapFrom(src => isSavedModel))
				.ForMember(x => x.NumberOfTastings, opt => opt.MapFrom(src => 0));
            });

            IMapper mapper = config.CreateMapper();

			var model = mapper.Map<UserCellarChampagneModel, UIUserCellarChampagneModel>(source);
         
            return model;
		}

	}
}
