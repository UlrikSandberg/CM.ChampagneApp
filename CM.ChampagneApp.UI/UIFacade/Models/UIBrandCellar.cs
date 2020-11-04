using System;
using System.Collections.Generic;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;

namespace CM.ChampagneApp.UI.UIFacade.Models
{
	public class UIBrandCellar : BaseUIModel
    {
		public string Title { get; set; }
        public Guid BrandId { get; set; }
        public Guid CardImgId { get; set; }
        public Guid HeaderImgId { get; set; }
		public string Url { get; set; }
		public List<UISection> sections { get; set; } = new List<UISection>();
		      
		//Custom UI!      

        public string HeaderImgUrl => new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{HeaderImgId}/small.jpg").ToString();
      
        //***** Inner Dependency classes *****
        public class UISection
		{
			public string Title { get; set; }
			public string Body { get; set; }
			public List<UIChampagneRoot> Champagnes { get; set; } = new List<UIChampagneRoot>();

            public bool IsTitleEnabled => Title != null;
        }
    }
}
