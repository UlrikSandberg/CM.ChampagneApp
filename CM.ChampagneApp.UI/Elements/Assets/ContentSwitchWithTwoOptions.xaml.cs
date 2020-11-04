using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class ContentSwitchWithTwoOptions : ContentView
    {

		public delegate void BugAction(object sender, System.EventArgs e);
		public event BugAction OnBugActionClicked;

		public delegate void FeedbackAction(object sender, System.EventArgs e);
		public event FeedbackAction OnFeedbackActionClicked;

		private bool IsBugClicked = false;
		private bool IsFeedbackClicked = false;

		private const string DisabledColor = "#333333";
		private const string EnabledColor = "#A68F68";

        public ContentSwitchWithTwoOptions()
        {
            InitializeComponent();
		}

		void Handle_BugClicked(object sender, System.EventArgs e)
		{
			IsBugClicked = true;
			IsFeedbackClicked = false;
			SetBugMode();
			if(OnBugActionClicked != null)
			{
				OnBugActionClicked(sender, e);
			}
		}

		void Handle_FeedbackClicked(object sender, System.EventArgs e)
		{
			IsFeedbackClicked = true;
			IsBugClicked = false;
			SetFeedbackMode();
			if(OnFeedbackActionClicked != null)
			{
				OnFeedbackActionClicked(sender, e);
			}
		}


        private void SetBugMode()
		{
			BugForground.BackgroundColor = Color.FromHex(EnabledColor);
			BugBackground.BackgroundColor = Color.FromHex(EnabledColor);
			FeedbackForground.BackgroundColor = Color.FromHex(DisabledColor);
			FeedbackBackground.BackgroundColor = Color.FromHex(DisabledColor);
		}

        private void SetFeedbackMode()
		{
			BugForground.BackgroundColor = Color.FromHex(DisabledColor);
			BugBackground.BackgroundColor = Color.FromHex(DisabledColor);
			FeedbackBackground.BackgroundColor = Color.FromHex(EnabledColor);
			FeedbackForground.BackgroundColor = Color.FromHex(EnabledColor);
		}
    }
}
