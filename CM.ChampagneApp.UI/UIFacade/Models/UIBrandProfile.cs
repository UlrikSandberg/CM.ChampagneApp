using System;
using System.Collections.Generic;
using CM.ChampagneApp.Business.Configuration;
//using CM.ChampagneApp.Business.Models;
using System.ComponentModel;

namespace CM.ChampagneApp.UI.UIFacade.Models
{
	public class UIBrandProfile : BaseUIModel
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
		public List<AbstractProfileCard> Pages { get; set; } = new List<AbstractProfileCard>();
        public UISocial Social { get; set; }
        public int NumberOfTastings { get; set; }
        public int NumberOfFollowers { get; set; }
        public int NumberOfEditions { get; set; }

		public bool IsRequesterFollowing { get; set; }
              
        public string BrandCoverImageUrl => new Uri(Configuration.BlobBrandStorageUrl, $"{Id}/images/{BrandCoverImageId}/small.jpg").ToString();
            
        public string BrandLogoImageUrl
		{
            get
			{
                return LogoImageId != Guid.Empty
                    ? new Uri(Configuration.BlobBrandStorageUrl, $"{Id}/images/{LogoImageId}/small.jpg").ToString()
                    : "PlaceholderBrandLogoImg.png";
            }
		}

        public bool IsSocialLinksVisible
        {
            get
            {
                if(Social != null)
                {
                    if(!string.IsNullOrEmpty(Social.FacebookUrl) || !string.IsNullOrEmpty(Social.InstagramUrl) || !string.IsNullOrEmpty(Social.PinterestUrl) || !string.IsNullOrEmpty(Social.TwitterUrl) || !string.IsNullOrEmpty(Social.WebsiteUrl))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

		public string NumberOfChampagnesString => $"{ChampagneIds.Count}";

		public int NumberOfChampagnes()
        {
            return ChampagneIds.Count;
        }

        public class UISocial
        {
			public string FacebookUrl { get; set; }
            public string TwitterUrl { get; set; }
            public string InstagramUrl { get; set; }
            public string PinterestUrl { get; set; }
            public string WebsiteUrl { get; set; }
        }      
    }
}
