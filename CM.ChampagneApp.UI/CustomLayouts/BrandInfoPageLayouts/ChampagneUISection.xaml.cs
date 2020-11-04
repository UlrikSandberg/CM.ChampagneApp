using System;
using System.Collections.Generic;
using Xamarin.Forms;
using CM.ChampagneApp.UI.Resources.Effects;
using System.Linq.Expressions;
using System.Linq;
using FFImageLoading.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UIBrandModels;

namespace CM.ChampagneApp.UI.CustomLayouts.BrandInfoPageLayouts
{
    public partial class ChampagneUISection : ContentView
    {

		UIBrandInfoPageSection context = null;

        public ChampagneUISection()
        {
            InitializeComponent();
        }

		public void InjectBindingContext(object bindingContext)
        {
            this.BindingContext = bindingContext;

            if (BindingContext != null)
            {
                context = (UIBrandInfoPageSection)this.BindingContext;
            }

			if(context.Title == null)
			{
				Title.IsVisible = false;
			}

			if(context.SubTitle == null)
			{
				SubTitle.IsVisible = false;
			}

			if(context.Body == null)
			{
				Body.IsVisible = false;
			}

            if (context.Images.Count < 1)
            {
				ImageSlider.IsVisible = false;
				SingleImage.IsVisible = false;
            }
            else if (context.Images.Count > 1)
            {
				ImageSlider.IsVisible = true;
				SingleImage.IsVisible = false;

				foreach(var image in context.ImageUrls)
				{
					var champagneUIImageCard = new ChampagneUIImageCard();
                    champagneUIImageCard.Source = image.ToString();
					champagneUIImageCard.WidthRequest = App.DisplaySettings.Width - 40;
               
					ImageSliderContent.Children.Add(champagneUIImageCard);
				}
            }
            else
            {
				ImageSlider.IsVisible = false;
				SingleImage.IsVisible = true;
                SingleImage.Source = context.ImageUrls[0].ToString();
            }
        }
    }
}
