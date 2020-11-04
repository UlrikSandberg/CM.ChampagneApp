using System;
namespace CM.ChampagneApp.DTO.Models.GETModels.UserModels
{
    public class UserCellarChampagneModel
    {
		public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid TastingId { get; set; }
        public Guid ChampagneId { get; set; }
        public Guid BrandId { get; set; }

        public string BrandName { get; set; }
        public string BottleName { get; set; }
        public Guid BottleImgId { get; set; }
        public bool IsVintage { get; set; }
		public int Year { get; set; }

        public string Dosage { get; set; }
        public double PersonalRating { get; set; }
    }
}
