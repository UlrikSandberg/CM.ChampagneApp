using System;
using System.Collections.Generic;
using CM.ChampagneApp.Business.Configuration;
using System.ComponentModel;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels
{
	public class UIChampagne : BaseUIModel
    {
        public const string BrutNature = "BrutNature";
        public const string ExtraBrut = "ExtraBrut";
        public const string Brut = "Brut";
        public const string ExtraDry = "ExtraDry";
        public const string Sec = "Sec";
        public const string DemiSec = "DemiSec";
        public const string Doux = "Doux";

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
		//public double AverageRating { get; set; }

        public bool IsVintage { get; set; }
        public int Year { get; set; }

        public UIChampagneProfile ChampagneProfile { get; set; }
        public UIFilterSearchParameters FilterSearchParameters { get; set; }

		public bool IsBookmarkedByRequester { get; set; }
		public bool IsTastedByRequester { get; set; }

		public UITasting RequesterTasting { get; set; }

        //Inner helper classes

        public class UIFilterSearchParameters
        {
            public string Dosage { get; set; }
            public List<string> Style { get; set; }
            public List<string> Character { get; set; }
        }

        public class UIChampagneProfile
        {
            public string Appearance { get; set; }
            public string BlendInfo { get; set; }
            public string Taste { get; set; }
            public string FoodPairing { get; set; }
            public string Aroma { get; set; }
            public string BottleHistory { get; set; }
			public string Dosage { get; set; }
			public string Style { get; set; }
			public string Character { get; set; }
            public double RedWineAmount { get; set; }
            public double ServingTemp { get; set; }
            public double AgeingPotential { get; set; }
            public double ReserveWineAmount { get; set; }
            public double DosageAmount { get; set; }
            public double AlchoholVol { get; set; }
            public double PinotNoir { get; set; }
            public double PinotMeunier { get; set; }
            public double Chardonnay { get; set; }
        }

        public string DisplayCharacter //<-- This is only relevant as long as we dont want to show the character/quality of the wine in the 2x3 grid on the champagneProfile
        {
            get
            {
                return "-";
            }
        }

        public string Alchohol
        {
            get
            {
                if(ChampagneProfile != null)
                {
                    if((ChampagneProfile.AlchoholVol > 0.0))
                    {
                        return AlchoholVol;
                    }
                }
                return " - ";
            }
        }

        public bool IsTastingNotesSectionVisible
        {
            get
            {
                if(IsAppearanceVisible || IsAromaVisible || IsTasteVisible)
                {
                    return true;
                }
                return false;
            }
        }

        public bool IsBlendInformationVisible
        {
            get
            {
                if(ChampagneProfile != null)
                {
                    if(!string.IsNullOrEmpty(ChampagneProfile.BlendInfo))
                    {
                        if(!ChampagneProfile.BlendInfo.Equals("There are no notes available") && !ChampagneProfile.BlendInfo.Equals("There are no notes available."))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public bool IsTasteVisible
        {
            get
            {
                if(ChampagneProfile != null)
                {
                    if(!string.IsNullOrEmpty(ChampagneProfile.Taste))
                    {
                        if(!ChampagneProfile.Taste.Equals("There are no notes available") && !ChampagneProfile.Taste.Equals("There are no notes available."))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public bool IsAromaVisible
        {
            get
            {
                if(ChampagneProfile != null)
                {
                    if(!string.IsNullOrEmpty(ChampagneProfile.Aroma))
                    {
                        if(!ChampagneProfile.Aroma.Equals("There are no notes available") && !ChampagneProfile.Aroma.Equals("There are no notes available."))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public bool IsAppearanceVisible
        {
            get
            {
                if(ChampagneProfile != null)
                {
                    if(!string.IsNullOrEmpty(ChampagneProfile.Appearance))
                    {
                        if(!ChampagneProfile.Appearance.Equals("There are no notes available") && !ChampagneProfile.Appearance.Equals("There are no notes available."))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public bool IsFoodPairingVisible
        {
            get
            {
                if(ChampagneProfile != null)
                {
                    if(!string.IsNullOrEmpty(ChampagneProfile.FoodPairing))
                    {
                        if (!ChampagneProfile.FoodPairing.Equals("There are no notes available") && !ChampagneProfile.FoodPairing.Equals("There are no notes available."))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        //Properties to complement UI needs!

        public string UserRating
		{
            get
			{
				if(RequesterTasting != null)
				{
					return $"{RequesterTasting.Rating}";
				}
				else
				{
					return "0.0";
				}
			}
		}

        public string BottleNameWithYear
        {
            get
            {
                if(IsVintage)
                {
                    return BottleName + " - " + Year;
                }
                else
                {
                    return BottleName;
                }
            }
        }

        public string BottleYear
		{
			get
			{
				if(IsVintage)
				{
					return "" + Year;
				}
				else
				{
					return "-".ToString();
				}
			}
		}

        public string BottleImgUrl => new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{BottleImgId}/large.png").ToString();

        public string BottleCoverImgUrl => new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{BottleCoverImgId}/large.jpg").ToString();
        
        public string AverageRating
        {
            get 
            {

				if (NumberOfTastings.Equals(0.0) || RatingSumOfTastings.Equals(0.0))
				{
					return "0.0";
				}
				else
				{
					return (RatingSumOfTastings / NumberOfTastings).ToString("0.0");
				}

            }
        }

        public string Character
        {
            get
            {
				if(ChampagneProfile != null)
				{
					return ChampagneProfile.Character;
				}
				else
				{
					return "-";
				}
            }
        }

        public bool IsTechnicalIdentitiesVisible
        {
            get
            {
                if(ChampagneProfile != null)
                {
                    if(ChampagneProfile.PinotNoir > 0 || ChampagneProfile.PinotMeunier > 0 || ChampagneProfile.Chardonnay > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public string Dosage
        {
            get
            {
                if(FilterSearchParameters != null)
                {
                    if(string.IsNullOrEmpty(FilterSearchParameters.Dosage))
                    {
                        return "-";
                    }
                    else
                    {
                        switch(FilterSearchParameters.Dosage)
                        {
                            case BrutNature:
                                return "Brut Nature";
                            case ExtraBrut:
                                return "Extra Brut";
                            case Brut:
                                return Brut;
                            case ExtraDry:
                                return "Extra Dry";
                            case Sec:
                                return "Sec";
                            case DemiSec:
                                return "Demi-Sec";
                            case Doux:
                                return Doux;
                        }
                    }
                }
                return "_";
            }
        }

        public string AlchoholVol => ChampagneProfile.AlchoholVol.ToString("0.0");

        public List<UITechnicalIdentity> TechnicalIdentities
        {
            get
            {
                var technicalIdentityList = new List<UITechnicalIdentity>();
                var structure = new UITechnicalIdentity
                {
                    Identity = "Structure",
                    GrapeVariant = "Pinot Noir",
                    GrapePercentage = ChampagneProfile.PinotNoir
                };
                var fruitness = new UITechnicalIdentity
                {
                    Identity = "Fruitness",
                    GrapeVariant = "Pinot Meunier",
                    GrapePercentage = ChampagneProfile.PinotMeunier
                };
                var freshness = new UITechnicalIdentity
                {
                    Identity = "Freshness",
                    GrapeVariant = "Chardonnay",
                    GrapePercentage = ChampagneProfile.Chardonnay
                };

                technicalIdentityList.Add(structure);
                technicalIdentityList.Add(fruitness);
                technicalIdentityList.Add(freshness);

                return technicalIdentityList;

            }
        }      
    }
}
