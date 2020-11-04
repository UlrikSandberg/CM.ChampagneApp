using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using CM.ChampagneApp.DTO.Models.GETModels.TastingModels;

namespace CM.ChampagneApp.DTO.Models
{
    public class Champagne
    {
        public Champagne()
        {
        }
        
        public Guid Id { get; set; }
        public string BottleName { get; set; }
        public string BrandName { get; set; }
        public string BrandProfileText { get; set; }
        public Guid BrandId { get; set; }
        public Guid BottleImgId { get; set; }
        public Guid BottleCoverImgId { get; set; }
        public Guid BrandCoverImgId { get; set; }
        public bool IsPublished { get; set; }

        public Guid ChampagneRootId { get; set; }
        public bool RootIsSingleton { get; set; }

        public double NumberOfTastings { get; set; }
        public double RatingSumOfTastings { get; set; }
		public double AverageRating { get; set; }
        
        public VintageInfo vintageInfo { get; set; }
        public ChampagneProfile champagneProfile { get; set; }
        public FilterSearchParameters filterSearchParameters { get; set; }

		public bool IsBookmarkedByRequester { get; set; }
		public bool IsTastedByRequester { get; set; }

		public Tasting RequesterTasting { get; set; }

        public class VintageInfo
        {
            public bool isVintage { get; set; }
            public int year { get; set; }
        }

        public class FilterSearchParameters
        {
            public string dosage { get; set; }
            public List<string> style { get; set; }
            public List<string> character { get; set; }
        }

        public class ChampagneProfile
        {
            public string appearance { get; set; }
            public string blendInfo { get; set; }
            public string taste { get; set; }
            public string foodPairing { get; set; }
            public string aroma { get; set; }
            public string bottleHistory { get; set; }
			public string dosage { get; set; }
			public string style { get; set; }
			public string character { get; set; }
            public double redWineAmount { get; set; }
            public double servingTemp { get; set; }
            public double ageingPotential { get; set; }
            public double reserveWineAmount { get; set; }
            public double dosageAmount { get; set; }
            public double alchoholVol { get; set; }
            public double pinotNoir { get; set; }
            public double pinotMeunier { get; set; }
            public double chardonnay { get; set; }
        }
    }
}
