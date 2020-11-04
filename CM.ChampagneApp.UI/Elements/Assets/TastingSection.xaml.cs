using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class TastingSection : ContentView
    {
        public string Notes
        {
            get { return (string)GetValue(NotesProperty); }
            set { SetValue(NotesProperty, value); }
        }

        public static BindableProperty NotesProperty =
            BindableProperty.Create(nameof(Notes), typeof(string), typeof(TastingSection));

        public string SectionHeader
        {
            get { return (string)GetValue(SectionHeaderProperty); }
            set { SetValue(SectionHeaderProperty, value); }
        }

        public static BindableProperty SectionHeaderProperty =
            BindableProperty.Create(nameof(SectionHeader), typeof(string), typeof(TastingSection));

        public string SectionIcon
        {
            get { return (string)GetValue(SectionIconProperty); }
            set { SetValue(SectionIconProperty, value); }
        }

        public static BindableProperty SectionIconProperty =
            BindableProperty.Create(nameof(SectionIcon), typeof(string), typeof(TastingSection));
    
        public TastingSection()
        {
            InitializeComponent();
        }
    }
}
