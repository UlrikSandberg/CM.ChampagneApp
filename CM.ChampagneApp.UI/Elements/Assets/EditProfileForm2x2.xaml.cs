﻿using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class EditProfileForm2x2 : ContentView
    {
		public delegate void Entry1Entered(object sender, TextEnteredEventArgs args);
        public event Entry1Entered OnEntry1Entered;

        public delegate void Entry2Entered(object sender, TextEnteredEventArgs args);
        public event Entry1Entered OnEntry2Entered;

        public EditProfileForm2x2()
        {
            InitializeComponent();
        }

        public string Entry1Text
        {
            get { return (string)GetValue(Entry1TextProperty); }
            set { SetValue(Entry1TextProperty, value); }
        }

        public static BindableProperty Entry1TextProperty =
            BindableProperty.Create(nameof(Entry1Text), typeof(string), typeof(EditProfileForm));

        public string Entry2Text
        {
            get { return (string)GetValue(Entry2TextProperty); }
            set { SetValue(Entry2TextProperty, value); }
        }

        public static BindableProperty Entry2TextProperty =
            BindableProperty.Create(nameof(Entry2Text), typeof(string), typeof(EditProfileForm));

              
        void Handle_Entry1Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (Entry1.Text != null)
            {
                var args = new TextEnteredEventArgs(Entry1.Text);
                if (OnEntry1Entered != null)
                {
                    OnEntry1Entered(sender, args);
                }
            }
        }

        void Handle_Entry2Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (Entry2.Text != null)
            {
                var args = new TextEnteredEventArgs(Entry2.Text);
                if (OnEntry2Entered != null)
                {
                    OnEntry2Entered(sender, args);
                }
            }
        }
    }
}
