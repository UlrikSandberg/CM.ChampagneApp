using System;
namespace CM.ChampagneApp.DTO.Models.Helpers.EnumOptions
{
    public class TastingOrderByOptions
    {

        public enum OrderByOptions
		{
            AcendingByRating,
            DecendingByRating,
            AcendingByDate,
            DecendingByDate
		}


        public TastingOrderByOptions()
        {
        }
    }
}
