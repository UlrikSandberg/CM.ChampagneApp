using System;
using System.Collections;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Elements.Cards;
using CM.ChampagneApp.UI.UIFacade.Models;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists
{
    public partial class ProfileCardList : ContentView
    {

        public delegate void ItemClicked(object sender, LinkPageEventArgs args);
        public event ItemClicked OnItemClicked;


        public ProfileCardList()
        {
            InitializeComponent();

            System.Diagnostics.Debug.WriteLine(App.DisplaySettings.Width - 40);

        }

        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }


        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(ProfileCardList), propertyChanged: ItemSourceChanged);


        private static void ItemSourceChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var ProfileCardList = (ProfileCardList)bindable;

            if(ProfileCardList != null)
            {
                ProfileCardList.UpdateList();
            }
        }


        private void UpdateList()
        {         
            if(ItemSource != null)
            {
				ContentPresenter.Children.Clear();
				var index = 0;
                foreach(Object ProfileCard in ItemSource)
                {
                    var Child = new ProfileCard();
                    Child.BindingContext = ProfileCard;
                    Child.WidthRequest = App.DisplaySettings.Width - 40;
					Child.OnChosenPageLink += new ProfileCard.ChosenPageLink(OnChosenPageLink);

					//Child.LabelsItemSource = ((pageLink)ProfileCard).Labels;

					var cast = (AbstractProfileCard)ProfileCard;

					Child.LabelsItemSource = ((AbstractProfileCard)ProfileCard).Labels;

					ContentPresenter.Children.Add(Child);
					index++;       
                }
				if (index > 1)
				{
					CardContainer.ScrollToAsync(20, 0, false);
				}
            }
        }

        private void OnChosenPageLink(object sender, LinkPageEventArgs args)
        {
            OnItemClicked(sender, args);
        }
    }
}
