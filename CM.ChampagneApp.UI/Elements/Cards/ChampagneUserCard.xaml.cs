using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class ChampagneUserCard : ContentView
	{
        public delegate void ChosenChampagne(object sender, ModelClickedEventArgs<UIUserCellarChampagneModel> args);
        public event ChosenChampagne OnChosenChampagne;

        public delegate void ChosenPersonalNotes(object sender, ModelClickedEventArgs<UIUserCellarChampagneModel> args);
        public event ChosenPersonalNotes OnChosenPersonalNotes;
      
        public ChampagneUserCard()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            var model = this.BindingContext as UIUserCellarChampagneModel;

            if(model != null)
            {
                if (model.IsFlipped)
                {
                    ChampagneBack.RotationY = 0;
                    ChampagneFront.RotationY = -360;
                    IsFlipped = true;
                }
                else
                {
                    IsFlipped = false;
                    ChampagneFront.RotationY = 0;
                    ChampagneBack.RotationY = -360;
                }
            }

            base.OnBindingContextChanged();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (!IsFlipEnabled)
            {
                OnChosenChampagne?.Invoke(this, new ModelClickedEventArgs<UIUserCellarChampagneModel>((UIUserCellarChampagneModel)this.BindingContext));
            }
            else
            {
                FlipButton(sender, e);
            }
        }

        async void Handle_OnFlippedBtn_ChampagneProfile(object sender, System.EventArgs e)
        {
            OnChosenChampagne?.Invoke(this, new ModelClickedEventArgs<UIUserCellarChampagneModel>((UIUserCellarChampagneModel)this.BindingContext));
        }

        async void Handle_OnFlippedBtn_PersonalNotes(object sender, System.EventArgs e)
        {
            OnChosenPersonalNotes?.Invoke(this, new ModelClickedEventArgs<UIUserCellarChampagneModel>((UIUserCellarChampagneModel)this.BindingContext));
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
                    //May not be packed into a Task because then this is not awaited need to be async void
                    await ChampagneFront.RotateYTo(-90, timeout, Easing.SpringIn);
                    IsFlipped = true;
                    await ChampagneBack.RotateYTo(-360, timeout, Easing.SpringOut);
                    ChampagneBack.RotationY = 0;
                }
                else
                {
                    ChampagneFront.RotationY = -270;
                    await ChampagneBack.RotateYTo(-90, timeout, Easing.SpringIn);
                    IsFlipped = false;
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
			BindableProperty.Create(nameof(IsFlipEnabled), typeof(bool), typeof(ChampagneUserCard), false);

        public bool IsFlipped
        {
            get { return (bool)GetValue(IsFlippedProperty); }
            set { SetValue(IsFlippedProperty, value); }
        }

        public static BindableProperty IsFlippedProperty =
            BindableProperty.Create(nameof(IsFlipped), typeof(bool), typeof(ChampagneUserCard), false, propertyChanged: IsFlippedChanged);

        private static void IsFlippedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ChampagneUserCard)bindable;

            if(control != null)
            {
                var bindingContext = control.BindingContext as UIUserCellarChampagneModel;
                if(bindingContext != null)
                {
                    bindingContext.IsFlipped = (bool)newValue;
                }
            }
        }
    }
}
