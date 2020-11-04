using System;
using System.Linq;
using System.Collections.Generic;
using CM.ChampagneApp.DTO.Builders.Helpers;

namespace CM.ChampagneApp.DTO.Builders
{
    public class FilterSearchQuery
	{
        
		public VintageInfo IsVintage { get; }
		public List<string> ChampagneStyle { get; }
		public List<string> ChampagneDosage { get; }
		public double LowerRating { get; }
		public double UpperRating { get; }

		public class VintageInfo
		{
			public bool Vintage { get; set; }
			public bool NonVintage { get; set; }
		}
      
		private FilterSearchQuery(VintageInfo isVintage, IEnumerable<ChampagneStyleEnum> champagneStyle, IEnumerable<ChampagneDosageEnum> champagneDosage, double lowerRating, double upperRating)
        {
			IsVintage = isVintage;

			if (champagneStyle != null)
			{
                ChampagneStyle = champagneStyle.Select(x => x.ToString()).ToList();
			}
			else
			{
				ChampagneStyle = null;
			}

			if (champagneDosage != null)
			{
                ChampagneDosage = champagneDosage.Select(x => x.ToString()).ToList();
			}
			else
			{
				ChampagneDosage = null;
			}

			LowerRating = lowerRating;
			UpperRating = upperRating;
		}

		public class FilterSearchQueryBuilder : IBuilder<FilterSearchQuery>
		{
            
			private VintageInfo IsVintage = null;
            
			private List<ChampagneStyleEnum> ChampagneStyle = null;

			private List<ChampagneDosageEnum> ChampagneDosage = null;

			private double LowerRating = 0.0;

			private double UpperRating = 5.0;


			public FilterSearchQueryBuilder SetIsVintage(bool isVintage)
			{
				if(IsVintage == null)
				{
					IsVintage = new VintageInfo();
				}
				IsVintage.Vintage = isVintage;
				return this;
			}

			public FilterSearchQueryBuilder SetIsNonVintage(bool isNonVintage)
			{
				if(IsVintage == null)
				{
					IsVintage = new VintageInfo();
				}
				IsVintage.NonVintage = isNonVintage;
				return this;
			}

			public FilterSearchQueryBuilder SetChampagneStyle(List<ChampagneStyleEnum> champagneStyle)
			{            
				ChampagneStyle = champagneStyle;
				return this;
			}

			public FilterSearchQueryBuilder SetChampagneStyle(ChampagneStyleEnum champagneStyle)
			{
				if(ChampagneStyle == null)
				{
					ChampagneStyle = new List<ChampagneStyleEnum>();
					ChampagneStyle.Add(champagneStyle);
				}
				else
				{
					ChampagneStyle.Add(champagneStyle);
				}

				return this;
			}

			public FilterSearchQueryBuilder SetChampagneDosage(List<ChampagneDosageEnum> champagneDosage)
			{
				ChampagneDosage = champagneDosage;
				return this;
			}

			public FilterSearchQueryBuilder SetChampagneDosage(ChampagneDosageEnum champagneDosage)
			{
				if(ChampagneDosage == null)
				{
					ChampagneDosage = new List<ChampagneDosageEnum>();
					ChampagneDosage.Add(champagneDosage);
				}
				else
				{
					ChampagneDosage.Add(champagneDosage);
				}

				return this;
			}

			public FilterSearchQueryBuilder SetLowerRating(double lowerRating)
			{
				LowerRating = lowerRating;
				return this;
			}

			public FilterSearchQueryBuilder SetUpperRating(double upperRating)
			{
				UpperRating = upperRating;
				return this;
			}


			public FilterSearchQuery Build()
			{
				if (IsVintage != null)
				{
					if (IsVintage.NonVintage && IsVintage.Vintage)
					{
						IsVintage = null;
					}
				}

				return new FilterSearchQuery(IsVintage, ChampagneStyle, ChampagneDosage, LowerRating, UpperRating);
			}
		}
	}
}
