using System;
namespace CM.ChampagneApp.DTO.Models.UserModels
{
    public class UserModel
    {

		public Guid id { get; set; }

        public string email { get; set; }
        public string name { get; set; }
        public string profileName { get; set; }
        public string biography { get; set; }
        public int bookmarkedChampagnes { get; set; }
        public int tastedChampagnes { get; set; }
        public int following { get; set; }
		public int followers { get; set; }

        public ImageCustomization ImageCustomization { get; set; }
        public Settings settings { get; set; }

        public class Settings
        {
            public string countryCode { get; set; }
            public string language { get; set; }

			public bool isEmailVerified { get; set; }
        }  
    }
}
