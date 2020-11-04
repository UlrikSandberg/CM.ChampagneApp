using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Headers
{
    public partial class H2WithDefinition : ContentView
    {
        
        public H2WithDefinition()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(H2WithDefinition));

        public string Definition
        {
            get { return (string)GetValue(DefinitionProperty); }
            set { SetValue(DefinitionProperty, value); }
        }

        public static BindableProperty DefinitionProperty =
            BindableProperty.Create(nameof(Definition), typeof(string), typeof(H2WithDefinition), "Hold here to see definition");
        
    }

}
