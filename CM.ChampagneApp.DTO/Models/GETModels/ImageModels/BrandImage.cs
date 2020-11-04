using System;
namespace CM.ChampagneApp.DTO.Models.GETModels.ImageModels
{
    public class BrandImage
    {
		public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public string Name { get; set; }
        public string TypeOfBrandImage { get; set; }
        public string FileType { get; set; }
    }
}
