using System;
using CM.ChampagneApp.UI.UIFacade.Models;

namespace CM.ChampagneApp.UI.UIReadModels
{
    public class OnboardingCardModel : BaseUIModel
    {
		public string ImageUrl { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
    }
}
