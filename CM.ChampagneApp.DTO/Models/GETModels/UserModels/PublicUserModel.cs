using System;

namespace CM.ChampagneApp.DTO.Models.UserModels
{
    public class PublicUserModel
    {
      
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
		public string ProfileName { get; set; }
		public string Biography { get; set; }
        
        public int BookmarkedChampagnes { get; set; }
        
        public int TastedChampagnes { get; set; }
        
        public int Following { get; set; }
        
        public int Followers { get; set; }

		public ImageCustomization ImageCustomization { get; set; }
        
        public bool IsRequesterFollowing { get; set; }
        
    }
}