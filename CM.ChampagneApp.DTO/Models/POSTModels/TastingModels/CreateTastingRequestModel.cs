using System;
namespace CM.ChampagneApp.DTO.Models.POSTModels.TastingModels
{
    public class CreateTastingRequestModel
    {
        public CreateTastingRequestModel()
        {
        }

		public string Review { get; set; }
		public double Rating { get; set; }
		public DateTime TimeStamp { get; set; }
    }
}
