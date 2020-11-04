using System;

using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure;
using CM.ChampagneApp.DTO.Models.GETModels.FollowModels;
using AutoMapper;

namespace CM.ChampagneApp.UI.UIFacade.Mapper.CustomMapperConfigurations.UserMapperConfiguration
{
    public static class MapToUIUserFollowModel 
    {
        public static UIUserFollowModel Map(FollowingModel source, Guid currentUserId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FollowingModel, UIUserFollowModel>()
                .ForMember(x => x.ProfileId, opt => opt.MapFrom(scr => scr.FollowToId))
                .ForMember(x => x.ProfileName, opt => opt.MapFrom(scr => scr.FollowToName))
                .ForMember(x => x.ProfileImgId, opt => opt.MapFrom(scr => scr.FollowToProfileImg));
            });

            IMapper mapper = config.CreateMapper();

            var model = mapper.Map<FollowingModel, UIUserFollowModel>(source);

            if (currentUserId.Equals(source.FollowToId))
            {
                model.ProfileName = source.FollowToName + " (You)";
                model.IsEnabled = false;
            }
            else
            {
                model.IsEnabled = true;
            }

            model.IsRequesterFollowing = source.IsRequesterFollowing;

            return model;
        }
        
        public static UIUserFollowModel Map(FollowersModel source, Guid currentUserId)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FollowersModel, UIUserFollowModel>()
                .ForMember(x => x.ProfileId, opt => opt.MapFrom(scr => scr.FollowById))
                .ForMember(x => x.ProfileImgId, opt => opt.MapFrom(scr => scr.FollowByProfileImgId)); 
            });

            IMapper mapper = config.CreateMapper();

            var model = mapper.Map<FollowersModel, UIUserFollowModel>(source);
            
            if (currentUserId.Equals(source.FollowById))
            {
                model.ProfileName = source.FollowByName + " (You)";
                model.IsEnabled = false;
            }
            else
            {
                model.ProfileName = source.FollowByName;
            }

            model.IsRequesterFollowing = source.IsRequesterFollowing;

            return model;
        }
    }
}

