using System;
namespace CM.ChampagneApp.DTO.Models.GETModels.TastingModels
{
    public class EditTastingModel 
    {
        public EditTastingModel()
        {
        }

		public Guid Id { get; set; }
        public string Review { get; set; }
        public double Rating { get; set; }

        public bool IsTastingNull { get; set; }

        public Guid BrandId { get; set; }
        public Guid ChampagneId { get; set; }
		public string BottleName { get; set; }
		public string BrandName { get; set; }
        public Guid ChampagneBottleImgId { get; set; }
        public Guid ChampagneCoverImgId { get; set; }

    }
}
