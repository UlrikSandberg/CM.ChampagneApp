using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class ProfileHeaderCard : ContentView
    {

        public static BindableProperty Field1x1CommandProperty =
            BindableProperty.Create(nameof(Field1x1Command), typeof(ICommand), typeof(ProfileHeaderCard));

        public static BindableProperty Field1x2CommandProperty =
            BindableProperty.Create(nameof(Field1x2Command), typeof(ICommand), typeof(ProfileHeaderCard));

        public static BindableProperty Field1x3CommandProperty =
            BindableProperty.Create(nameof(Field1x3Command), typeof(ICommand), typeof(ProfileHeaderCard));

        public static BindableProperty ProfileNameProperty =
            BindableProperty.Create(nameof(ProfileName), typeof(string), typeof(ProfileHeaderCard));

        public static BindableProperty ProfileTextProperty =
            BindableProperty.Create(nameof(ProfileText), typeof(string), typeof(ProfileHeaderCard));

        public static BindableProperty ProfileImgProperty =
            BindableProperty.Create(nameof(ProfileImg), typeof(string), typeof(ProfileHeaderCard));

        public static BindableProperty ProfileImgPlaceholderProperty =
            BindableProperty.Create(nameof(ProfileImgPlaceholder), typeof(string), typeof(ProfileHeaderCard), propertyChanged: ProfileImgPlaceholderChanged);

        public static BindableProperty Field1x1HeaderProperty =
            BindableProperty.Create(nameof(Field1x1Header), typeof(string), typeof(ProfileHeaderCard));

        public static BindableProperty Field1x1ValueProperty =
            BindableProperty.Create(nameof(Field1x1Value), typeof(string), typeof(ProfileHeaderCard));

        public static BindableProperty Field1x2HeaderProperty =
            BindableProperty.Create(nameof(Field1x2Header), typeof(string), typeof(ProfileHeaderCard));

        public static BindableProperty Field1x2ValueProperty =
            BindableProperty.Create(nameof(Field1x2Value), typeof(string), typeof(ProfileHeaderCard));

        public static BindableProperty Field1x3HeaderProperty =
            BindableProperty.Create(nameof(Field1x3Header), typeof(string), typeof(ProfileHeaderCard));

        public static BindableProperty Field1x3ValueProperty =
            BindableProperty.Create(nameof(Field1x3Value), typeof(string), typeof(ProfileHeaderCard));

        public delegate void ChosenField1x1(object sender, System.EventArgs e);
        public event ChosenField1x1 OnChosenField1x1;

        public delegate void ChosenField1x2(object sender, System.EventArgs e);
        public event ChosenField1x2 OnChosenField1x2;

        public delegate void ChosenField1x3(object sender, System.EventArgs e);
        public event ChosenField1x3 OnChosenField1x3;

        public ProfileHeaderCard()
        {
            InitializeComponent();

			Logo.Diameter = ((App.DisplaySettings.Width - 40) / 3)  / 2;
        }

        public ICommand Field1x1Command
        {
            get { return (ICommand)GetValue(Field1x1CommandProperty); }
            set { SetValue(Field1x1CommandProperty, value); }
        }

        public ICommand Field1x2Command
        {
            get { return (ICommand)GetValue(Field1x2CommandProperty); }
            set { SetValue(Field1x2CommandProperty, value); }
        }

        public ICommand Field1x3Command
        {
            get { return (ICommand)GetValue(Field1x3CommandProperty); }
            set { SetValue(Field1x3CommandProperty, value); }
        }

        public string ProfileName
        {
            get { return (string)GetValue(ProfileNameProperty); }
            set { SetValue(ProfileNameProperty, value); }
        }

        public string ProfileText
		{
			get { return (string)GetValue(ProfileTextProperty); }
			set { SetValue(ProfileTextProperty, value); }
		}

        public string ProfileImg
        {
            get { return (string)GetValue(ProfileImgProperty); }
            set { SetValue(ProfileImgProperty, value); }
        }

        public string ProfileImgPlaceholder
        {
            get { return (string)GetValue(ProfileImgPlaceholderProperty); }
            set { SetValue(ProfileImgPlaceholderProperty, value); }
        }

        public string Field1x1Header
        {
            get { return (string)GetValue(Field1x1HeaderProperty); }
            set { SetValue(Field1x1HeaderProperty, value); }
        }

        public string Field1x1Value
        {
            get { return (string)GetValue(Field1x1ValueProperty); }
            set { SetValue(Field1x1ValueProperty, value); }
        }

        public string Field1x2Header
        {
            get { return (string)GetValue(Field1x2HeaderProperty); }
            set { SetValue(Field1x2HeaderProperty, value); }
        }

        public string Field1x2Value
        {
            get { return (string)GetValue(Field1x2ValueProperty); }
            set { SetValue(Field1x2ValueProperty, value); }
        }

        public string Field1x3Header
        {
            get { return (string)GetValue(Field1x3HeaderProperty); }
            set { SetValue(Field1x3HeaderProperty, value); }
        }

        public string Field1x3Value
        {
            get { return (string)GetValue(Field1x3ValueProperty); }
            set { SetValue(Field1x3ValueProperty, value); }
        }

        private static void ProfileImgPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var profileHeaderCard = (ProfileHeaderCard)bindable;

            if (profileHeaderCard != null)
            {
                if (string.IsNullOrEmpty(profileHeaderCard.ProfileImg))
                {
                    profileHeaderCard.ProfileImg = (string)newValue;
                }
            }
        }

        void Handle_ClickedField1x1(object sender, System.EventArgs e)
        {
            if (OnChosenField1x1 != null)
            {
                OnChosenField1x1(sender, e);
            }

            if(Field1x1Command != null)
            {
                if(Field1x1Command.CanExecute(null))
                {
                    Field1x1Command.Execute(null);
                }
            }
        }

        void Handle_ClickedField1x2(object sender, System.EventArgs e)
        {
            if (OnChosenField1x2 != null)
            {
                OnChosenField1x2(sender, e);
            }

            if (Field1x2Command != null)
            {
                if (Field1x2Command.CanExecute(null))
                {
                    Field1x2Command.Execute(null);
                }
            }
        }

        void Handle_ClickedField1x3(object sender, System.EventArgs e)
        {
            if (OnChosenField1x3 != null)
            {
                OnChosenField1x3(sender, e);
            }

            if (Field1x3Command != null)
            {
                if (Field1x3Command.CanExecute(null))
                {
                    Field1x3Command.Execute(null);
                }
            }
        }
    }
}
