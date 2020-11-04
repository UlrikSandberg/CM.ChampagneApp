using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Buttons
{
    public partial class SocialButtons : ContentView
    {
        public delegate void SocialButtonClicked(object sender, SocialLinkEventArgs args);
        public event SocialButtonClicked OnSocialButtonClicked;

        public SocialButtons()
        {
            InitializeComponent();
        }

        void Handle_OnFacebookClicked(object sender, System.EventArgs e)
        {
            if(OnSocialButtonClicked != null)
            {
                var args = new SocialLinkEventArgs(FacebookUrl);
                OnSocialButtonClicked(sender, args);
            }
        }

        void Handle_OnInstagramClicked(object sender, System.EventArgs e)
        {
            if (OnSocialButtonClicked != null)
            {
                var args = new SocialLinkEventArgs(InstagramUrl);
                OnSocialButtonClicked(sender, args);
            }
        }

        void Handle_OnPinterestClicked(object sender, System.EventArgs e)
        {
            if (OnSocialButtonClicked != null)
            {
                var args = new SocialLinkEventArgs(PinterestUrl);
                OnSocialButtonClicked(sender, args);
            }
        }

        void Handle_OnTwitterClicked(object sender, System.EventArgs e)
        {
            if (OnSocialButtonClicked != null)
            {
                var args = new SocialLinkEventArgs(TwitterUrl);
                OnSocialButtonClicked(sender, args);
            }
        }

        void Handle_OnwwwClicked(object sender, System.EventArgs e)
        {
            if (OnSocialButtonClicked != null)
            {
                var args = new SocialLinkEventArgs(wwwUrl);
                OnSocialButtonClicked(sender, args);
            }
        }

        public string FacebookUrl
        {
            get { return (string)GetValue(FacebookUrlProperty); }
            set { SetValue(FacebookUrlProperty, value); }
        }

        public static BindableProperty FacebookUrlProperty =
            BindableProperty.Create(nameof(FacebookUrl), typeof(string), typeof(SocialButtons), propertyChanged: UrlChanged);

        public string InstagramUrl
        {
            get { return (string)GetValue(InstagramUrlProperty); }
            set { SetValue(InstagramUrlProperty, value); }
        }

        public static BindableProperty InstagramUrlProperty =
            BindableProperty.Create(nameof(InstagramUrl), typeof(string), typeof(SocialButtons), propertyChanged: UrlChanged);

        public string PinterestUrl
        {
            get { return (string)GetValue(PinterestUrlProperty); }
            set { SetValue(PinterestUrlProperty, value); }
        }

        public static BindableProperty PinterestUrlProperty =
            BindableProperty.Create(nameof(PinterestUrl), typeof(string), typeof(SocialButtons), propertyChanged: UrlChanged);

        public string TwitterUrl
        {
            get { return (string)GetValue(TwitterUrlProperty); }
            set { SetValue(TwitterUrlProperty, value); }
        }

        public static BindableProperty TwitterUrlProperty =
            BindableProperty.Create(nameof(TwitterUrl), typeof(string), typeof(SocialButtons), propertyChanged: UrlChanged);

        public string wwwUrl
        {
            get { return (string)GetValue(wwwUrlProperty); }
            set { SetValue(wwwUrlProperty, value); }
        }

        public static BindableProperty wwwUrlProperty =
            BindableProperty.Create(nameof(wwwUrl), typeof(string), typeof(SocialButtons), propertyChanged: UrlChanged);

        private static void UrlChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var SocialButtons = (SocialButtons)bindable;

            if(SocialButtons != null)
            {
                SocialButtons.UpdateUrlButtons();
            }
        }

        private void UpdateUrlButtons()
        {
            if(FacebookUrl == null || FacebookUrl == "")
            {
                FacebookButton.IsVisible = false;
            }
            else
            {
                FacebookButton.IsVisible = true;
            }

            if (InstagramUrl == null || InstagramUrl == "")
            {
                InstagramButton.IsVisible = false;
            }
            else
            {
                InstagramButton.IsVisible = true;
            }

            if ( PinterestUrl == null || PinterestUrl == "")
            {
                PinterestButton.IsVisible = false;
            }
            else
            {
                PinterestButton.IsVisible = true;
            }

            if (TwitterUrl == null || TwitterUrl == "")
            {
                TwitterButton.IsVisible = false;
            }
            else
            {
                TwitterButton.IsVisible = true;
            }

            if (wwwUrl == null || wwwUrl == "")
            {
                wwwButton.IsVisible = false;
            }
            else
            {
                wwwButton.IsVisible = true;
            }

        }
    }
}
