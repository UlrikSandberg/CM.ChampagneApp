using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.DTO.Models
{
    public class BrandProfile
    {

        public Guid Id { get; set; }
        public bool Published { get; set; }
        public string Name { get; set; }
        public string BrandProfileText { get; set; }
        public Guid BrandCoverImageId { get; set; }
        public Guid BottleCoverImageId { get; set; }
		public Guid BrandListImageId { get; set; }
        public Guid LogoImageId { get; set; }
        public List<Guid> ChampagneIds { get; set; }
        public Cellar cellar { get; set; }
        public List<ProfileCardModel> Pages { get; set; }
        public Social social { get; set; }
        public int NumberOfTastings { get; set; }
        public int NumberOfFollowers { get; set; }
        public int NumberOfEditions { get; set; }
		public bool IsRequesterFollowing { get; set; }

        public class Cellar
        {
            public string Title { get; set; }
            public Guid CardImgId { get; set; }
            public string Url { get; set; }
            public List<string> Labels { get; set; }
        }

        public class Social
        {
			public string FacebookUrl { get; set; }
            public string TwitterUrl { get; set; }
            public string InstagramUrl { get; set; }
            public string PinterestUrl { get; set; }
            public string WebsiteUrl { get; set; }
        }
    }
}