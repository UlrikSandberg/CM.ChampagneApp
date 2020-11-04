using System;
using CM.ChampagneApp.Business.Configuration;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIProfileCardStructure
{
	public class UIBrandProfileCard : AbstractProfileCard
    {      
        public override string CardImgUrl
		{
            get
			{
                if(CardImgId == Guid.Empty)
				{
					return "PlaceholderCellar.jpg";
				}
                else
				{
                    return new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{CardImgId}/small.jpg").ToString();
				}
			}
		}

        public Guid BrandId
		{
            get
			{
				return AuthorId;
			}
		}      
    }
}
