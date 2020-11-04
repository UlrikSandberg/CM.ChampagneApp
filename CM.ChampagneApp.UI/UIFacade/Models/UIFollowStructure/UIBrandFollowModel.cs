using System;
using CM.ChampagneApp.Business.Configuration;
//using CM.ChampagneApp.Business.Models;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure
{
    public class UIBrandFollowModel : AbstractFollowModel
    {
              
        public override string ProfileImgUrl
        {
            get
            {
                if (ProfileImgId == Guid.Empty)
                {
                    return "PlaceholderBrandLogoImg.png";
                }
                else
                {
                    return new Uri(Configuration.BlobBrandStorageUrl, $"{BrandId}/images/{ProfileImgId}/small.jpg").ToString();
                }
            }
        }

        private Guid BrandId => ProfileId;
    }
}