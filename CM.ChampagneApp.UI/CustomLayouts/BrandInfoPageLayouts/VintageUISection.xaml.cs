using System;
using System.Collections.Generic;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UIBrandModels;
using System.Linq;

namespace CM.ChampagneApp.UI.CustomLayouts.BrandInfoPageLayouts
{
    public partial class VintageUISection : ContentView
    {

		UIBrandInfoPageSection context = null;


      
        public VintageUISection()
        {
            InitializeComponent();      
        }

        public void InjectBindingContext(object bindingContext)
		{
			this.BindingContext = bindingContext;

			if(BindingContext != null)
            {
                context = (UIBrandInfoPageSection)this.BindingContext;
            }

			if(context.Images.Count < 1)
            {
                SingleImage.IsVisible = false;
                ImageSlider.IsVisible = false;

            }
			else if(context.Images.Count > 1)
            {
                SingleImage.IsVisible = false;
                ImageSlider.IsVisible = true;
				foreach(var image in context.ImageUrls)
                {
					ImageSliderContent.Children.Add(new Frame
					{
						WidthRequest = App.DisplaySettings.Width - 40,
						Padding = new Thickness(0,0,0,0),
						Content = new Image { Source = ImageSource.FromUri(image) }
					});
                }
            }
            else
            {
                SingleImage.IsVisible = true;
                ImageSlider.IsVisible = false;

				SingleImage.Source = ImageSource.FromUri(context.ImageUrls[0]);
            } 
		}
    }
}
