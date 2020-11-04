using System;
using System.Collections.Generic;
using CM.ChampagneApp.Business.Configuration;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIProfileCardStructure
{
	public class UIUserProfileCard : AbstractProfileCard
	{      
		public override string CardImgUrl
		{
			get
			{
                return CardImgId == Guid.Empty
                    ? "CellarCardDefault.jpg"
                    : new Uri(Configuration.BlobUserStorageUrl, $"{UserId}/images/{CardImgId}/original.jpg").ToString();
            }
		}

		public Guid UserId
		{
			get
			{
				return AuthorId;
			}
		}
	}
}
