using System;
using System.Collections;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.UIFacade.Models;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class ProfileCard : ContentView
    {

        public delegate void ChosenPageLink(object sender, LinkPageEventArgs args);
        public event ChosenPageLink OnChosenPageLink;

        public ProfileCard()
        {
            InitializeComponent();
        }
        
        void Handle_Clicked(object sender, System.EventArgs e)
        {
			var uiBrandProfileCard = (AbstractProfileCard)this.BindingContext;
			LinkPageEventArgs args = new LinkPageEventArgs(uiBrandProfileCard.Url);
            if (OnChosenPageLink != null)
                OnChosenPageLink(this, args);
        }

        public IEnumerable<string> LabelsItemSource
        {
            get { return (IEnumerable<string>)GetValue(LabelsItemSourceProperty); }
            set { SetValue(LabelsItemSourceProperty, value); }
        }

        public static BindableProperty LabelsItemSourceProperty =
            BindableProperty.Create(nameof(LabelsItemSource), typeof(IEnumerable), typeof(ProfileCard), propertyChanged: LabelsItemSourceChanged);

        private static void LabelsItemSourceChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var ProfileCard = (ProfileCard)bindable;
            if(ProfileCard != null)
            {
                ProfileCard.UpdateCard();
            }

        }

        private void UpdateCard()
        {

            if(LabelsItemSource != null)
            {
                var ItemNumber = 1;
                var ItemSourceLength = ((List<string>)LabelsItemSource).Count;

                foreach(Object labelString in LabelsItemSource)
                {
                    LabelPresenter.Children.Add(new Label
                    {
                        Text = (string)labelString,
                        TextColor = Color.White,
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold
                    });

                    if(ItemNumber != ItemSourceLength)
                    {
                        LabelPresenter.Children.Add(new Image
                        {
                            Source = "OvalLightGold.png",
                            HeightRequest = 8
                        });
                    }

                    ItemNumber += 1;
                }
            }
        }
    }
}
