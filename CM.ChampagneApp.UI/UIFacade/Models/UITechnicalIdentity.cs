using System;
namespace CM.ChampagneApp.UI.UIFacade.Models
{
    public class UITechnicalIdentity
    {
        public UITechnicalIdentity()
        {
        }
              
        public string Identity
        {
            get;
            set;
        }

        public string GrapeVariant
        {
            get;
            set;
        }

        public double GrapePercentage
        {
            get;
            set;
        }

        public string GrapeValue => (string)GrapePercentage.ToString() + "%";
    }
}
