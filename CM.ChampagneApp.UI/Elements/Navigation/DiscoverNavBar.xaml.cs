using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Navigation
{
    public partial class DiscoverNavBar : ContentView
    {

        public delegate void OnTitleTapped(object sender, System.EventArgs e);
        public event OnTitleTapped TitleTapped;

        public static BindableProperty LeftButtonCommandProperty =
            BindableProperty.Create(nameof(LeftButtonCommand), typeof(ICommand), typeof(DiscoverNavBar));

        public static BindableProperty RightButtonCommandProperty =
            BindableProperty.Create(nameof(RightButtonCommand), typeof(ICommand), typeof(DiscoverNavBar));

        public static BindableProperty TitleDoubleTappedCommandProperty =
            BindableProperty.Create(nameof(TitleDoubleTappedCommand), typeof(ICommand), typeof(DiscoverNavBar));

        public DiscoverNavBar()
        {
            InitializeComponent();
            
        }

        public ICommand LeftButtonCommand
        {
            get { return (ICommand)GetValue(LeftButtonCommandProperty); }
            set { SetValue(LeftButtonCommandProperty, value); }
        }

        public ICommand RightButtonCommand
        {
            get { return (ICommand)GetValue(RightButtonCommandProperty); }
            set { SetValue(RightButtonCommandProperty, value); }
        }

        public ICommand TitleDoubleTappedCommand
        {
            get { return (ICommand)GetValue(TitleDoubleTappedCommandProperty); }
            set { SetValue(TitleDoubleTappedCommandProperty, value); }
        }

        public bool HasShadow
        {
            get { return (bool)GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        public static BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(bool), typeof(DiscoverNavBar), false);

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            if(TitleTapped != null)
            {
                TitleTapped(sender, e);
            }
        }
    }
}
