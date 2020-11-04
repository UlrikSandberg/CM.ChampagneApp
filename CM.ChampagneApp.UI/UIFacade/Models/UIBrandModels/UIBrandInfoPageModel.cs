using System;
//using CM.ChampagneApp.Business.Models.GETModels.BrandModels;
using CM.ChampagneApp.Business.Configuration;
using System.Collections.Generic;
using CM.ChampagneApp.UI.UIFacade.Models.UIImageModels;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIBrandModels
{
	public class UIBrandInfoPageModel : BaseUIModel
    {      
		public Guid BrandId { get; set; }

        public Guid BrandPageId { get; set; }

        public string Title { get; set; }

		public string BrandName { get; set; }

        public string uiTemplateIdentifier { get; set; }

        public Guid CardImgId { get; set; }

        public Guid HeaderImgId { get; set; }

		public UIBrandImageModel HeaderImg { get; set; }

        public bool Published { get; set; }

        public string Url { get; set; }

		public List<UIBrandInfoPageSection> Sections { get; set; } = new List<UIBrandInfoPageSection>();


        public string HeaderImgUrl
		{
            get
			{
                return HeaderImg == null
                    ? ""
                        : new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{HeaderImg.Id}/large.{HeaderImg.FileType}").ToString();
            }
		}
         
    }
}
