using System;

namespace CM.ChampagneApp.DTO.Models
{
    public class BrandLight
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool Published { get; set; }
		public Guid BrandCoverImageId { get; set; }
		public Guid BottleCoverImageId { get; set; }
		public Guid BrandListImageId { get; set; }
		public Guid BrandLogoImageId { get; set; }
		public Guid[] ChampagneIds { get; set; }
		public int NumberOfTastings { get; set; }
		public int NumberOfFollowers { get; set; }
        public int NumberOfChampagnes { get; set; }
    }
}
