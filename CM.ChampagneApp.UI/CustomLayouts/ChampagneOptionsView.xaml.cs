using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.CustomLayouts
{
    public partial class ChampagneOptionsView : ContentView
    {
        public static BindableProperty Option1IconProperty =
            BindableProperty.Create(nameof(Option1Icon), typeof(string), typeof(ChampagneOptionsView));

        public static BindableProperty Option1TitleProperty =
            BindableProperty.Create(nameof(Option1Title), typeof(string), typeof(ChampagneOptionsView));

        public static BindableProperty Option2IconProperty =
            BindableProperty.Create(nameof(Option2Icon), typeof(string), typeof(ChampagneOptionsView));

        public static BindableProperty Option2TitleProperty =
            BindableProperty.Create(nameof(Option2Title), typeof(string), typeof(ChampagneOptionsView));

        public static BindableProperty Option1IsVisibleProperty =
            BindableProperty.Create(nameof(Option1IsVisible), typeof(bool), typeof(ChampagneOptionsView), false);

        public static BindableProperty Option2IsVisibleProperty =
            BindableProperty.Create(nameof(Option2IsVisible), typeof(bool), typeof(ChampagneOptionsView), false);

        public static BindableProperty ShouldAnimateViewProperty =
            BindableProperty.Create(nameof(ShouldAnimateView), typeof(bool), typeof(ChampagneOptionsView), false, propertyChanged: ShouldAnimateViewChanged);

        public static BindableProperty Option1CommandProperty =
            BindableProperty.Create(nameof(Option1Command), typeof(ICommand), typeof(ChampagneOptionsView));

        public static BindableProperty Option2CommandProperty =
            BindableProperty.Create(nameof(Option2Command), typeof(ICommand), typeof(ChampagneOptionsView));

        public static BindableProperty BackgroundClickedCommandProperty =
            BindableProperty.Create(nameof(BackgroundClickedCommand), typeof(ICommand), typeof(ChampagneOptionsView));

        public static BindableProperty IsFakeNavigationBarVisibleProperty =
            BindableProperty.Create(nameof(IsFakeNavigationBarVisible), typeof(bool), typeof(ChampagneOptionsView), true);

        public delegate void BackgroundClicked(object sender, System.EventArgs e);
        public event BackgroundClicked OnBackgroundClicked;

        public delegate void Option1Clicked(object sender, System.EventArgs e);
        public event Option1Clicked OnOption1Clicked;

        public delegate void Option2Clicked(object sender, System.EventArgs e);
        public event Option2Clicked OnOption2Clicked;

        public delegate void AnimationDone(object sender, System.EventArgs e);
        public event AnimationDone AnimationDoneEvent;

        public ChampagneOptionsView()
        {
            InitializeComponent();
        }

        public string Option1Icon
        {
            get { return (string)GetValue(Option1IconProperty); }
            set { SetValue(Option1IconProperty, value); }
        }

        public string Option1Title
        {
            get { return (string)GetValue(Option1TitleProperty); }
            set { SetValue(Option1TitleProperty, value); }
        }

        public string Option2Icon
        {
            get { return (string)GetValue(Option2IconProperty); }
            set { SetValue(Option2IconProperty, value); }
        }

        public string Option2Title
        {
            get { return (string)GetValue(Option2TitleProperty); }
            set { SetValue(Option2TitleProperty, value); }
        }

        public bool ShouldAnimateView
		{
			get { return (bool)GetValue(ShouldAnimateViewProperty); }
			set { SetValue(ShouldAnimateViewProperty, value); }
		}

        public bool Option1IsVisible
        {
            get { return (bool)GetValue(Option1IsVisibleProperty); }
            set { SetValue(Option1IsVisibleProperty, value); }
        }

        public bool Option2IsVisible
        {
            get { return (bool)GetValue(Option2IsVisibleProperty); }
            set { SetValue(Option2IsVisibleProperty, value); }
        }

        public ICommand Option1Command
        {
            get { return (ICommand)GetValue(Option1CommandProperty); }
            set { SetValue(Option1CommandProperty, value); }
        }

        public ICommand Option2Command
        {
            get { return (ICommand)GetValue(Option2CommandProperty); }
            set { SetValue(Option2CommandProperty, value); }
        }

        public ICommand BackgroundClickedCommand
        {
            get { return (ICommand)GetValue(BackgroundClickedCommandProperty); }
            set { SetValue(BackgroundClickedCommandProperty, value); }
        }

        public bool IsFakeNavigationBarVisible
        {
            get { return (bool)GetValue(IsFakeNavigationBarVisibleProperty); }
            set { SetValue(IsFakeNavigationBarVisibleProperty, value); }
        }

        private static void ShouldAnimateViewChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (ChampagneOptionsView)bindable;

			if(control != null)
			{
				System.Diagnostics.Debug.WriteLine("Should animate view!");

                control.AnimateView((bool)newValue);
			}
		}

        private async Task AnimateView(bool ShouldAnimateForth)
		{
			if(ShouldAnimateForth)
			{
				OptionMenu.IsVisible = true;
				await OptionMenu.FadeTo(1, 200);
			}
			else
			{
				await OptionMenu.FadeTo(0, 200);
				OptionMenu.IsVisible = false;
                AnimationDoneEvent?.Invoke(this, new System.EventArgs());
            }
        }

		void Handle_Clicked(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Should handle cancel vintage archive");
			if (OnBackgroundClicked != null)
			{
				OnBackgroundClicked(sender, e);
			}
            if(BackgroundClickedCommand != null)
            {
                if(BackgroundClickedCommand.CanExecute(null))
                {
                    BackgroundClickedCommand.Execute(null);
                }
            }
        }

		void Handle_Option1Clicked(object sender, System.EventArgs e)
		{
			if(OnOption1Clicked != null)
            {
                OnOption1Clicked?.Invoke(sender, e);
            }
            if(Option1Command != null)
            {
                if(Option1Command.CanExecute(null))
                {
                    Option1Command.Execute(null);
                }
            }
        }

        void Handle_Option2Clicked(object sender, System.EventArgs e)
        {
			if(OnOption2Clicked != null)
            {
                OnOption2Clicked(sender, e);
            }
            if(Option2Command != null)
            {
                if(Option2Command.CanExecute(null))
                {
                    Option2Command.Execute(null);
                }
            }
        }
    }
}
