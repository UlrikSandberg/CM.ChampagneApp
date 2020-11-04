using System;
using System.Collections.Generic;

namespace CM.ChampagneApp.UI.UIFacade.Models.UINotifications
{
	public abstract class UIAbstractNotification : BaseUIModel
    {
		public Guid Id { get; set; }

        public Guid InvokerId { get; set; }
        public string InvokerName { get; set; }
        public Guid InvokerProfileImgId { get; set; }

        public string InvokedByMethod { get; set; }

        public List<Guid> Receivers { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }

		public abstract string ProfileImgUrl { get; }
    }
}
