using System;
using System.Collections.Generic;
using System.Windows.Input;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Pages.SearchPages.Helpers;
using CM.ChampagneApp.UI.UIReadModels;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class FilterSearchTagCard : ContentView
    {

        public delegate void FilterChosen(object sender, ChosenFilterEventArgs args);
        public event FilterChosen OnFilterChosen;

        public delegate void LongGesture(object sender, TextEnteredEventArgs args);
        public event LongGesture OnLongGesture;

        public delegate void LongGestureEnded(object sender);
        public event LongGestureEnded OnLongGestureEnded;

        public ICommand longPressRecognized { get; set; }
        public ICommand longPressEnded { get; set; }

        private bool Chosen { get; set; }
        public string definition { get; set; }

        public FilterSearchTagCard()
        {
            InitializeComponent();
            Chosen = false;

            longPressRecognized = new Command(OnLongPressGesture);
            longPressEnded = new Command(OnLongPressGestureEnded);
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(FilterSearchTagCard));

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            if(!Chosen)
            {
                border.BackgroundColor = Color.FromHex("#A68F68");
                Chosen = true;
            }
            else
            {
                border.BackgroundColor = Color.Transparent;
                Chosen = false;
            }

            var args = new ChosenFilterEventArgs((FilterSearchTag)this.BindingContext, Chosen);

            if(OnFilterChosen != null)
            {
                System.Diagnostics.Debug.WriteLine("Sending to list");
                OnFilterChosen(sender, args);
            }
        }

        private void OnLongPressGesture()
        {
            var filterSearchTag = (FilterSearchTag)this.BindingContext;
            var textArgs = new TextEnteredEventArgs(filterSearchTag.Definition);
            if(OnLongGesture != null)
            {
                OnLongGesture(this, textArgs);
            }

        }

        private void OnLongPressGestureEnded()
        {
            if(OnLongGestureEnded != null)
            {
                OnLongGestureEnded(this);
            }
        }
    }
}
