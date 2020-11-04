using System;
namespace CM.ChampagneApp.DTO.Models
{
    public class ChampagneLight
    {
		public Guid Id { get; set; }
        public string BottleName { get; set; }
		public string BrandName { get; set; }
		public Guid BrandId { get; set; }
		public Guid BottleImgId { get; set; }
        public bool IsPublished { get; set; }
		public Guid ChampagneRootId { get; set; }
        public double NumberOfTastings { get; set; }
        public double RatingSumOfTastings { get; set; }
        public GetVintageInfo getVintageInfo { get; set; }

        public class GetVintageInfo
        {
            public bool isVintage { get; set; }
            public int year { get; set; }
        }
    }
}
