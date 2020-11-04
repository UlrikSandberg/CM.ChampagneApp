using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.Elements.Typography
{
    public partial class GiveReviewTextField : ContentView
    {
		private string PlaceHolderText { get; set; } = null;

        public delegate void ReviewEntered(object sender, TextEnteredEventArgs args);
        public event ReviewEntered OnReviewEntered;

		public delegate void DidFocus(object sender, FocusEventArgs e);
		public event DidFocus OnDidFocus;

		public delegate void DidUnfocus(object sender, FocusEventArgs e);
        public event DidUnfocus OnDidUnfocus;

		public delegate void ReturnKeyPressed(object sender, System.EventArgs e);
        public event ReturnKeyPressed OnReturnKeyPressed;

		public ICommand DismissKeyboard { get; set; }
		public ICommand ClearText { get; set; }

		public string EnteredText { get; set; } = null;

		public bool IsKeyboardActive { get; set; } = false;

        public GiveReviewTextField()
        {
            InitializeComponent();

			TextArea.Text = "";
            TextArea.TextColor = Color.White;
			DismissKeyboard = new Command(OnDismissKeyboard);
			ClearText = new Command(OnClearText);
        }

        private void OnDismissKeyboard()
		{
			TextArea.Unfocus();
		}

        private void OnClearText()
		{
			TextArea.Text = "";
			Handle_Unfocused(this, new FocusEventArgs(this, false));
		}

        public string Title
		{
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}

		public static BindableProperty TitleProperty =
			BindableProperty.Create(nameof(Title), typeof(string), typeof(GiveReviewTextField));

        public string PlaceholderText
		{
			get { return (string)GetValue(PlaceholderTextProperty); }
			set { SetValue(PlaceholderTextProperty, value); }
		}

        public bool IsWordCountEnabled
		{
			get { return (bool)GetValue(IsWordCountEnabledProperty); }
			set { SetValue(IsWordCountEnabledProperty, value); }
		}

		public static BindableProperty IsWordCountEnabledProperty =
			BindableProperty.Create(nameof(IsWordCountEnabled), typeof(bool), typeof(GiveReviewTextField), false);

        public int AllowedWordCount
		{
			get { return (int)GetValue(AllowedWordCountProperty); }
			set { SetValue(AllowedWordCountProperty, value); }
		}

		public static BindableProperty AllowedWordCountProperty =
			BindableProperty.Create(nameof(AllowedWordCount), typeof(int), typeof(GiveReviewTextField), 0);

		public static BindableProperty PlaceholderTextProperty =
			BindableProperty.Create(nameof(PlaceholderText), typeof(string), typeof(GiveReviewTextField), propertyChanged: ReviewContentChanged);

        public string ReviewText
		{
			get { return (string)GetValue(ReviewTextProperty); }
			set { SetValue(ReviewTextProperty, value); }
		}

		public static BindableProperty ReviewTextProperty =
			BindableProperty.Create(nameof(ReviewText), typeof(string), typeof(GiveReviewTextField), propertyChanged: ReviewContentChanged);
		
        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Text Changes");

			if(IsWordCountEnabled)
			{
				var wordCount = ((TextArea.Text.Trim()).ToCharArray()).Length;

				var currentCount = AllowedWordCount - wordCount;
           
				if(AllowedWordCount - wordCount < 0)
				{
					var chars = TextArea.Text.Trim();
					var removedString = chars.Remove(TextArea.Text.Trim().ToCharArray().Length - 1);

					TextArea.Text = removedString;

					wordCountLabel.Text = "Words: 0";
				}
				else
				{
					wordCountLabel.Text = "Words: " + (AllowedWordCount - wordCount);
					EnteredText = e.NewTextValue;
				}	
			}
			EnteredText = TextArea.Text;
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Completed");
        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
			IsKeyboardActive = true;
			System.Diagnostics.Debug.WriteLine("Keyboard is active");
			if(PlaceholderText == null)
			{
				var trimmedText = TextArea.Text.Trim();
				if (trimmedText.Equals(""))
				{               
					TextArea.Text = "";
					TextArea.TextColor = Color.White;
				}
			}
			else if(TextArea.Text.Equals(PlaceholderText))
            {
                TextArea.Text = "";
                TextArea.TextColor = Color.White;
            }

			if(OnDidFocus != null)
			{
				OnDidFocus(sender, e);
			}
        }

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
			IsKeyboardActive = false;
			System.Diagnostics.Debug.WriteLine("Keyboard is inactive");
            var trimmedText = TextArea.Text.Trim();
            if(trimmedText.Equals(""))
			{
                TextArea.Text = PlaceholderText;
                TextArea.TextColor = Color.White;
				if(IsWordCountEnabled)
				{
					wordCountLabel.Text = "Words: " + AllowedWordCount;
				}
				var args = new TextEnteredEventArgs(null);
				if (OnReviewEntered != null)
				{
					OnReviewEntered(sender, args);
				}
            }
            else
            {
                if(OnReviewEntered != null)
                {
                    var args = new TextEnteredEventArgs(trimmedText);
                    OnReviewEntered(sender, args);
                }
            }

			if(OnDidUnfocus != null)
			{
				OnDidUnfocus(sender, e);
			}
        }

		private static void ReviewContentChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (GiveReviewTextField)bindable;

			if(control != null)
			{
				control.HandleReviewContentChanged();
			}
		}

        private void HandleReviewContentChanged()
		{
			if (ReviewText == null)
			{
				TextArea.Text = PlaceholderText;
			}
			else
			{
				if (ReviewText.Length > 0)
				{
					TextArea.Text = ReviewText;
				}
				else
				{
					TextArea.Text = PlaceholderText;
				}
			}
		}
      
		void Handle_OnReturnKeyPressed(object sender, System.EventArgs e)
		{
			if(OnReturnKeyPressed != null)
			{
				OnReturnKeyPressed(sender, e);
			}
		}
    }
}
