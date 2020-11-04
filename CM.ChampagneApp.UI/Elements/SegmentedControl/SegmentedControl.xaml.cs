using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Elements.SegmentedControl
{
	public partial class SegmentedControl : ContentView
    {
		public delegate void SegmentSelected(object sender, ValueChangedEventArgs e);
		public event SegmentSelected OnSegmentSelected;

		public ObservableCollection<SegmentedControlTab> Children { get; set; } = new ObservableCollection<SegmentedControlTab>();

		private int ChosenTabIndex = 0;

        public SegmentedControl()
        {
            InitializeComponent();
			if(Children != null)
			{
				Children.CollectionChanged += Children_CollectionChanged;
			}      
        }

        public bool IsSlidingIndicatorVisible
		{
			get { return (bool)GetValue(IsSlidingIndicatorVisibleProperty); }
			set { SetValue(IsSlidingIndicatorVisibleProperty, value); }
		}

		public static BindableProperty IsSlidingIndicatorVisibleProperty =
			BindableProperty.Create(nameof(IsSlidingIndicatorVisible), typeof(bool), typeof(SegmentedControl), false);

        public Color SlidingIndicatorTintColor
		{
			get { return (Color)GetValue(SlidingIndicatorTintColorProperty); }
			set { SetValue(SlidingIndicatorTintColorProperty, value); }
		}

		public static BindableProperty SlidingIndicatorTintColorProperty =
			BindableProperty.Create(nameof(SlidingIndicatorTintColor), typeof(Color), typeof(SegmentedControl), Color.Red);

        public Color TintColor
		{
			get { return (Color)GetValue(TintColorProperty); }
			set { SetValue(TintColorProperty, value); }
		}

		public static BindableProperty TintColorProperty =
			BindableProperty.Create(nameof(TintColor), typeof(Color), typeof(SegmentedControl), Color.Transparent);

		void Children_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
			LayoutSegmentedControl();
        }

        private void LayoutSegmentedControl()
		{
			var grid = new Grid();
			grid.ColumnSpacing = 0;
			grid.RowSpacing = 0;
			var colDef = new ColumnDefinitionCollection();
			foreach(var tab in Children)
			{
				colDef.Add(new ColumnDefinition
				{
					Width = new GridLength(1, GridUnitType.Star)
				});
			}

			grid.ColumnDefinitions = colDef;
            
			var index = 0;
			foreach(var tab in Children)
			{
				tab.Index = index;
				tab.OnTabSelected -= Tab_OnTabSelected;
				tab.OnTabSelected += Tab_OnTabSelected;
				grid.Children.Add(tab, index, 0);
				index++;            
			}
			SegmentedControlView.Content = null;
			SegmentedControlView.Content = grid;

            //Init segmentedcontrol helpers
			ChosenTabChanged(ChosenTabIndex);
			InitChosenIndexIndicator();
            
		}

        public void SetSelectedTabIndex(int i)
        {
            if(i > Children.Count)
            {
                if(i < 0)
                {
                    throw new ArgumentException($"{nameof(i)} - Value:{i} can't be less than zero");
                }
                throw new ArgumentException($"{nameof(i)} - Value:{i} succedes the total number of tabIndexes");
            }

            AnimateSlider(ChosenTabIndex, i);
            ChosenTabChanged(i);
        }

        void Tab_OnTabSelected(object sender, SegmentedControlTabEventArgs e)
		{
			if(OnSegmentSelected != null)
			{
				OnSegmentSelected(sender, new ValueChangedEventArgs(ChosenTabIndex, e.TabIndex));
			}
            AnimateSlider(ChosenTabIndex, e.TabIndex);
			ChosenTabChanged(e.TabIndex);

		}

        private void ChosenTabChanged(int newTabIndex)
		{
			//Set new chosen tabIndex;         
			ChosenTabIndex = newTabIndex;

            //Clear backgroundcolor
			foreach(var tab in Children)
			{
				tab.BackgroundColor = Color.Transparent;
			}
            
            //Set tint backgroundcolor for chosen tab index
			var chosenTab = Children[ChosenTabIndex];
			chosenTab.BackgroundColor = TintColor;         
		}

		private void InitChosenIndexIndicator()
		{
			if (IsSlidingIndicatorVisible)
			{
				//Reset Frame!
				slidingIndicator.WidthRequest = App.DisplaySettings.Width / Children.Count;
				slidingIndicator.Padding = new Thickness(-1, 2, 1, 0);
			}
		}

        private async Task AnimateSlider(int oldValue, int newValue)
		{
			if (IsSlidingIndicatorVisible) //Should animate the sliding indicator to the correct index
            {
				int destinationX = destinationX = (int)slidingIndicator.Width * (newValue);
            
				await slidingIndicator.TranslateTo(destinationX, 0, 250, Easing.SinIn);
            }
		}      
	}
}
