using System;
using CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure;

namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class FollowClickedEventArgs
    {

		public AbstractFollowModel FollowModel { get; private set; }

		public FollowClickedEventArgs(AbstractFollowModel followModel)
        {
			FollowModel = followModel;
        }
    }
}
