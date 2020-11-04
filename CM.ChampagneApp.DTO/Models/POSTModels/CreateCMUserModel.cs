using System;
namespace CM.ChampagneApp.DTO.Models.POSTModels
{
    public class CreateCMUserModel
    {
		public string Email { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string ProfileName { get; set; }
		public string Biography { get; set; }
		public long UTCOffSet { get; set; }
    }
}
