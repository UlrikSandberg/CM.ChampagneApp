using System;
using Xamarin.Forms;
namespace CM.ChampagneApp.UI.Elements.Typography
{
	public class CommentEditor : Editor
	{
        public delegate void InvalidatedMeasure(object sender, System.EventArgs e);
        public event InvalidatedMeasure OnInvalidateMeasure;


        public CommentEditor()
        {         

            this.TextChanged += (sender, e) =>
            {

                if (e.NewTextValue.Equals("\n") || e.NewTextValue.EndsWith("\n"))
                {
                    this.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                    //this.Unfocus();
                }
                else
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        System.Diagnostics.Debug.WriteLine(this.Height);
                        if (this.Height < 50)
                        {
                            //this.InvalidateMeasure();
                            if (OnInvalidateMeasure != null)
                            {
                                OnInvalidateMeasure(this, new EventArgs());
                            }
                        }
                        else
                        {
                            if (e.NewTextValue.Length < e.OldTextValue.Length)
                            {
                                //this.InvalidateMeasure();
                                if (OnInvalidateMeasure != null)
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
