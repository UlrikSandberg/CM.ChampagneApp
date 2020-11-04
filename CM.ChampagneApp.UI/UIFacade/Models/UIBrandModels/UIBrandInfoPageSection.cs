using System;
using System.Linq;
using System.Collections.Generic;
using CM.ChampagneApp.UI.UIFacade.Models.UIImageModels;
namespace CM.ChampagneApp.UI.UIFacade.Models.UIBrandModels
{
	public class UIBrandInfoPageSection : BaseUIModel
    {
		public Guid Id { get; set; }

        public Guid BrandId { get; set; }

        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Body { get; set; }

        public Guid[] Champagnes { get; set; } = new Guid[0];
        
        public Guid[] ImageIds { get; set; } = new Guid[0];

		public List<UIBrandImageModel> Images { get; set; } = new List<UIBrandImageModel>();

        public List<Uri> ImageUrls => Images.Select(x => new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{x.Id}/large.{x.FileType}")).ToList();
    }
}
