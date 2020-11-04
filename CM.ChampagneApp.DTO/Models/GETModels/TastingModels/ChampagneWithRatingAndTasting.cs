using System;
using System.Collections.Generic;
namespace CM.ChampagneApp.DTO.Models.GETModels.TastingModels
{
    public class ChampagneWithRatingAndTasting
    {

		public Guid Id { get; set; }
		public Guid BrandId { get; set; }

		public string BottleName { get; set; }
		public string BrandName { get; set; }

		public List<double> Ratings { get; set; }

		public bool IsBookmarkedByRequester { get; set; }
		public bool IsTastedByRequester { get; set; }

		public IEnumerable<Tasting> Tastings { get; set; }

        public ChampagneWithRatingAndTasting()
        {
        }
    }
}
