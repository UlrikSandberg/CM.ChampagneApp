using System;
using CM.ChampagneApp.DTO.Models.GETModels.ImageModels;
namespace CM.ChampagneApp.UI.UIFacade.Models.UIImageModels
{
	public class UIBrandImageModel : BaseUIModel
    {      
		public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string TypeOfBrandImage { get; set; }
        public string FileType { get; set; }
    }
}
