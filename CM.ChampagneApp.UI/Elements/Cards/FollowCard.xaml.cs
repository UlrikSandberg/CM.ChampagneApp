using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class FollowCard : ContentView
    {

        public delegate void FollowButtonClicked(object sender, FollowClickedEventArgs args);
        public event FollowButtonClicked OnFollowButtonClicked;

        public delegate void CardClicked(object sender, FollowClickedEventArgs args);
        public event CardClicked OnCardClicked;

        public FollowCard()
        {
            InitializeComponent();

        }

        void Handle_OnButtonClicked(object sender, System.EventArgs e)
        {
			var followModel = (AbstractFollowModel)this.BindingContext;
			FollowClickedEventArgs args = new FollowClickedEventArgs(followModel);
            if (OnFollowButtonClicked != null)
            {
                OnFollowButtonClicked(sender, args);
            }
        }

        void Handle_OnCardClicked(object sender, System.EventArgs e)
        {
			var followModel = (AbstractFollowModel)this.BindingContext;
			FollowClickedEventArgs args = new FollowClickedEventArgs(followModel);
            if(OnCardClicked != null)
            {
                OnCardClicked(sender, args);
            }
        }

		void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			//Each time propertyChange change cornerRadis
			if(e.PropertyName.Equals("Height") || e.PropertyName.Equals("Width"))
			{
				RoundImage.CornerRadius = (float)RoundImage.Height / 2;
			}
		}
    }
}
