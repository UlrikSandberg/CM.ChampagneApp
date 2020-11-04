using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Elements.Helpers.ElementEnum;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists.FooterElements
{
    public partial class BrandSubmissionFooter : ContentView
    {
        public delegate void SubmissionEntered(object sender, TextEnteredEventArgs args);
        public event SubmissionEntered OnSubmissionEntered;

        public static BindableProperty SuccesStateProperty =
            BindableProperty.Create(nameof(SuccesState), typeof(SuccesStateEnum), typeof(BrandSubmissionFooter), propertyChanged: SuccesStateChanged);

        public string SubmissionButtonText { get; set; }

        public const string StartSubmissionButtonText = "Submit a Brand";
        public const string EndSubmissionButtonText = "Submit";

        public bool IsKeyboardActive { get; set; } = false;

        public BrandSubmissionFooter()
        {
            InitializeComponent();
            SubmissionButtonText = StartSubmissionButtonText;
        }

        public SuccesStateEnum SuccesState
        {
            get { return (SuccesStateEnum)GetValue(SuccesStateProperty); }
            set { SetValue(SuccesStateProperty, value); }
        }

        private static void SuccesStateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (BrandSubmissionFooter)bindable;

            if(control != null)
            {
                var nValue = (SuccesStateEnum)newValue;

                if(nValue.Equals(SuccesStateEnum.Succes))
                {
                    control.SubmitForm.IsVisible = false;
                    control.SubmissionButtonText = StartSubmissionButtonText;
                    if (control.TextField.ClearText.CanExecute(null))
                    {
                        control.TextField.ClearText.Execute(null);
                    }
                }
            }
        }

        void Handle_OnSubmissionClicked(object sender, System.EventArgs e)
        {
            if (!SubmitForm.IsVisible) //Open submission dialog
            {
                SubmitForm.IsVisible = true;
                SubmissionButtonText = EndSubmissionButtonText;
            }
            else //
            {
                if (!string.IsNullOrEmpty(TextField.EnteredText))
                {
                    if (!TextField.EnteredText.Equals(TextField.PlaceholderText))
                    {
                        //Animate submission
                        if (OnSubmissionEntered != null)
                        {
                            OnSubmissionEntered(this, new TextEnteredEventArgs(TextField.EnteredText));
                        }
                    }
                    else
                    {
                        if (TextField.DismissKeyboard.CanExecute(null))
                        {
                            TextField.DismissKeyboard.Execute(null);
                        }
                        IsKeyboardActive = false;
                    }
                }
                else
                {
                    if (TextField.DismissKeyboard.CanExecute(null))
                    {
                        TextField.DismissKeyboard.Execute(null);
                    }
                    IsKeyboardActive = false;
                }
            }
        }
    }
}
