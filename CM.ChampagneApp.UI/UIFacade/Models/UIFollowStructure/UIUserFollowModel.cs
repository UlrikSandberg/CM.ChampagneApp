using System;
using CM.ChampagneApp.Business.Configuration;

namespace CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure
{
    public class UIUserFollowModel : AbstractFollowModel
    {    
        public override string ProfileImgUrl
        {
            get
            {
                if (ProfileImgId == Guid.Empty)
                {
                    return "PlaceholderProfileImg.png";
                }
                else
                {
                    return new Uri(Configuration.BlobUserStorageUrl, $"{UserId}/images/{ProfileImgId}/small.jpg").ToString();
                }
            }
        }

        private Guid UserId => ProfileId;
    }
}