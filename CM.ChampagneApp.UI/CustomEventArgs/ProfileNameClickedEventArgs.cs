using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class ProfileNameClickedEventArgs
    {

		public Guid UserId { get; set; }

        public ProfileNameClickedEventArgs(Guid userId)
        {
			UserId = userId;
        }
    }
}
