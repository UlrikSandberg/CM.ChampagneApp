using System;
using Xamarin.Forms;
using CM.ChampagneApp.UI.Elements.Buttons;

namespace CM.ChampagneApp.UI.Elements.Typography
{
    public class CustomEditor : Editor
    {

        public delegate void InvalidatedMeasure(object sender, System.EventArgs e);
        public event InvalidatedMeasure OnInvalidateMeasure;

        public delegate void ReturnKeyPressed(object sender, System.EventArgs e);
        public event ReturnKeyPressed OnReturnKeyPressed;

        public CustomEditor()
        {       
            this.TextChanged += (sender, e) =>
            {
                if (e.NewTextValue.Equals("\n") || e.NewTextValue.EndsWith("\n"))
                {
                    this.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                    if(OnReturnKeyPressed != null)
                    {
                        OnReturnKeyPressed(this, new System.EventArgs());
                    }
                    this.Unfocus();
                }
                else
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        System.Diagnostics.Debug.WriteLine(this.Height);
                        if (this.Height < 50)
                        {                     
                            //this.InvalidateMeasure();
                            if(OnInvalidateMeasure != null)
                            {
                                OnInvalidateMeasure(this, new EventArgs());
                            }
                        }
                        else
                        {
                            if(e.NewTextValue.Length < e.OldTextValue.Length)
                            {
                                //this.InvalidateMeasure();
                                if(OnInvalidateMeasure != null)
                                {
                                    OnInvalidateMeasure(this, new EventArgs());
                                }
                            }
                        }
                    }
                }
            };

            this.Completed += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Enper pressed:");
            };
        }
    }
}
