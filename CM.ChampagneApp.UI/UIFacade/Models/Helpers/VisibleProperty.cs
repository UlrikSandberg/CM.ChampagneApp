using System;
namespace CM.ChampagneApp.UI.UIFacade.Models.Helpers
{
    public class VisibleProperty<T>
    {
        public T Value { get; set; }
        public bool IsVisible
        {
            get
            {
                if(Value != null)
                {
                    var sValue = Value as string;
                    if(sValue != null)
                    {
                        if(string.IsNullOrEmpty(sValue))
                        {
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
