using System;
using CM.ChampagneApp.Business.Configuration;
using CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure;
namespace CM.ChampagneApp.UI.UIFacade.Models.UIUser
{
	public class UIUserSearchModel : BaseUIModel
	{
		public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProfileName { get; set; }
        public Guid ImageId { get; set; }

        public string ProfileImgUrl
        {
            get
            {
                if (ImageId == Guid.Empty)
                {
                    return "PlaceholderProfileImg.png";
                }
                else
                {
                    return new Uri(Configuration.BlobUserStorageUrl, $"{Id}/images/{ImageId}/small.jpg").ToString();
                }
            }
        }
    }
}
