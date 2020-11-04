using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;
using CM.ChampagneApp.UI.Elements.Helpers.ElementEnum;

namespace CM.ChampagneApp.UI.Elements.Typography
{
    public partial class SignUpEntryField : ContentView
    {

        public delegate void EntryFieldCompleted(object sender, TextEnteredEventArgs args);
        public event EntryFieldCompleted OnEntryFieldCompleted;

		public delegate void TextChange(object sender, TextEnteredEventArgs args);
		public event TextChange OnTextChange;

        public SignUpEntryField()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(SignUpEntryField));

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (TextField.Text != null)
            {
				if(OnEntryFieldCompleted != null)
				{               
                    var args = new TextEnteredEventArgs(TextField.Text);
                    OnEntryFieldCompleted(sender, args);
				}
            }
        }


        public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(SignUpEntryField));

        public Color TextColor
		{
			get { return (Color)GetValue(TextColorProperty); }
			set { SetValue(TextColorProperty, value); }
		}

		public static BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(SignUpEntryField), Color.White);

        public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set { SetValue(KeyboardProperty, value); }
        }

        public static BindableProperty KeyboardProperty =
            BindableProperty.Create(nameof(Keyboard), typeof(Keyboard), typeof(SignUpEntryField), Keyboard.Default);

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); }
        }

        public static BindableProperty IsPasswordProperty =
            BindableProperty.Create(nameof(IsPassword), typeof(bool), typeof(SignUpEntryField), false);
    
	    public bool IsValidating
		{
			get { return (bool)GetValue(IsValidatingProperty); }
			set { SetValue(IsValidatingProperty, value); }
		}

		public static BindableProperty IsValidatingProperty =
			BindableProperty.Create(nameof(IsValidating), typeof(bool), typeof(SignUpEntryField), false, propertyChanged: IsValidatingChanged);

		private static void IsValidatingChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (SignUpEntryField)bindable;

			if(control != null)
			{
				control.ValidationIcon.IsVisible = false;
			}
		}

		public SuccesStateEnum ValidationState
		{
			get { return (SuccesStateEnum)GetValue(ValidationStateProperty); }
			set { SetValue(ValidationStateProperty, value); }
		}

		public static BindableProperty ValidationStateProperty =
			BindableProperty.Create(nameof(ValidationState), typeof(SuccesStateEnum), typeof(SignUpEntryField), SuccesStateEnum.Default, propertyChanged: ValidationStateChanged);

		private static void ValidationStateChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (SignUpEntryField)bindable;

			if(control != null)
			{
				control.SetValidationIcon((SuccesStateEnum)newValue);
			}
		}

		private void SetValidationIcon(SuccesStateEnum state)
		{
         
			if(state.Equals(SuccesStateEnum.Succes))
			{
				ValidationIcon.Source = "SignUpValidationSucces.png";
				ValidationIcon.IsVisible = true;
			}
			else if(state.Equals(SuccesStateEnum.Error))
			{
				ValidationIcon.Source = "SignUpValidationError.png";
				ValidationIcon.IsVisible = true;
			}
			else if(state.Equals(SuccesStateEnum.Default))
			{
				ValidationIcon.Source = "";
				ValidationIcon.IsVisible = false;
			}

		}


		void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			if(OnTextChange != null)
			{
				OnTextChange(sender, new TextEnteredEventArgs(TextField.Text));
				System.Diagnostics.Debug.WriteLine(TextField.Text);
			}
		}
	}
}
