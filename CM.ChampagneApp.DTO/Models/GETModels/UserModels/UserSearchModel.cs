using System;
namespace CM.ChampagneApp.DTO.Models.GETModels.UserModels
{
    public class UserSearchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProfileName { get; set; }
        public Guid ImageId { get; set; }
    }
}
