using System;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.DTO.Models;
using System.ComponentModel;

namespace CM.ChampagneApp.UI.UIFacade.Models
{
	public class UIBrand : BaseUIModel
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

        public int NumberOfBottles => ChampagneIds.Length;

        public string NumberOfEditions
        {
            get
            {
                return NumberOfChampagnes.ToString();
            }
        }

        public string BrandCoverImageUrl => new Uri(Configuration.BlobBrandStorageUrl, $"{Id}/images/{BrandCoverImageId}/small.jpg").ToString();
          
        public string BrandListImageUrl
		{
            get
			{
                return BrandListImageId.Equals(Guid.Empty)
                    ? BrandCoverImageUrl
                    : new Uri(Configuration.BlobBrandStorageUrl, $"{Id}/images/{BrandListImageId}/large.jpg").ToString();
            }
		}
    }
}
