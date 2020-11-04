using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.UIFacade.Models;
using Xamarin.Forms;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class ChampagneCard : ContentView
    {

        public delegate void ChosenChampagne(object sender, ChampagneClickedEventArgs args);
        public event ChosenChampagne OnChosenChampagne;

		public delegate void ChosenPersonalNotes(object sender, ChampagneClickedEventArgs args);
		public event ChosenPersonalNotes OnChosenPersonalNotes;
      
        public ChampagneCard()
        {
            InitializeComponent();

        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
			if (!IsFlipEnabled)
			{
				var champagne = (UIChampagneRoot)this.BindingContext;
				System.Diagnostics.Debug.WriteLine("Its working for pagne: " + champagne.FolderName);
				ChampagneClickedEventArgs args = new ChampagneClickedEventArgs(champagne);
				if (OnChosenChampagne != null)
					OnChosenChampagne(this, args);
			}
			else
			{
				FlipButton(sender, e);
			}
        }

		async void Handle_OnFlippedBtn_ChampagneProfile(object sender, System.EventArgs e)
		{
			var champagne = (UIChampagneRoot)this.BindingContext;
			System.Diagnostics.Debug.WriteLine("Its working for pagne: " + champagne.FolderName);
            ChampagneClickedEventArgs args = new ChampagneClickedEventArgs(champagne);
            if (OnChosenChampagne != null)
                OnChosenChampagne(this, args);
		}

		async void Handle_OnFlippedBtn_PersonalNotes(object sender, System.EventArgs e)
        {
			System.Diagnostics.Debug.WriteLine("Should choose personal notes for champagne");
			var champagne = (UIChampagneRoot)this.BindingContext;
			ChampagneClickedEventArgs args = new ChampagneClickedEventArgs(champagne);
			if(OnChosenPersonalNotes != null)
			{
				OnChosenPersonalNotes(this, args);
			}
        }

		private bool AnimationInProgress = false;

		async void FlipButton(object sender, EventArgs e)
        {
            uint timeout = 500;
			if (!AnimationInProgress)
			{
				AnimationInProgress = true;
				if (ChampagneFront.IsVisible)
				{
					ChampagneBack.RotationY = -270;
					await ChampagneFront.RotateYTo(-90, timeout, Easing.SpringIn);
					ChampagneFront.IsVisible = false;
					ChampagneBack.IsVisible = true;
                    await ChampagneBack.RotateYTo(-360, timeout, Easing.SpringOut);
					ChampagneBack.RotationY = 0;
				}
				else
				{
					ChampagneFront.RotationY = -270;
                    await ChampagneBack.RotateYTo(-90, timeout, Easing.SpringIn);
					ChampagneBack.IsVisible = false;
					ChampagneFront.IsVisible = true;
                    await ChampagneFront.RotateYTo(-360, timeout, Easing.SpringOut);
					ChampagneFront.RotationY = 0;
				}
				AnimationInProgress = false;
			}
        }

        public bool IsFlipEnabled
		{
			get { return (bool)GetValue(IsFlipEnabledProperty); }
			set { SetValue(IsFlipEnabledProperty, value); }
		}

		public static BindableProperty IsFlipEnabledProperty =
			BindableProperty.Create(nameof(IsFlipEnabled), typeof(bool), typeof(ChampagneCard), false);
    }
}
