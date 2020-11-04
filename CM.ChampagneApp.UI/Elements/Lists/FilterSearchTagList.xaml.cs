using System;
using System.Collections;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Elements.Cards;
using CM.ChampagneApp.UI.Pages.SearchPages.Helpers;
using CM.ChampagneApp.UI.UIReadModels;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists
{
    public partial class FilterSearchTagList : ContentView
    {

        public delegate void FiltersChosen(object sender, ChosenFiltersListEventArgs args);
        public event FiltersChosen OnFiltersChosen;

        public delegate void LongGesture(object sender, TextEnteredEventArgs args);
        public event LongGesture OnLongGesture;

        public delegate void LongGestureEnded(object sender);
        public event LongGestureEnded OnLongGestureEnded;

        public List<FilterSearchTag> ChosenFilters { get; set; }

        public FilterSearchTagList()
        {
            InitializeComponent();
            ChosenFilters = new List<FilterSearchTag>();
        }

        public IEnumerable ItemSource
        {

            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }

        }

        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(TechnicalList), propertyChanged: ItemSourceChanged);


        private static void ItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var filterSearchTagList = (FilterSearchTagList)bindable;

            if (filterSearchTagList != null)
            {
                filterSearchTagList.UpdateContent();
            }
        }


        private void UpdateContent()
        {
			if (ItemSource != null)
			{
				foreach (FilterSearchTag tag in ItemSource)
				{
					System.Diagnostics.Debug.WriteLine("This should have been a tag: " + tag);

					var FilterOption = new FilterSearchTagCard();
					FilterOption.BindingContext = tag;
					FilterOption.Title = tag.Title;
					FilterOption.definition = tag.Definition;

					FilterOption.OnFilterChosen += new FilterSearchTagCard.FilterChosen(Handle_OnFilterChosen);
					FilterOption.OnLongGesture += new FilterSearchTagCard.LongGesture(Handle_OnLongPressGesture);
					FilterOption.OnLongGestureEnded += new FilterSearchTagCard.LongGestureEnded(Handle_OnLongPressGestureEnded);

					ContentPresenter.Children.Add(FilterOption);
				}
			}
        }

        private void Handle_OnLongPressGesture(object sender, TextEnteredEventArgs args)
        {
            if(OnLongGesture != null)
            {
                OnLongGesture(sender, args);
            }
        }

        private void Handle_OnLongPressGestureEnded(object sender)
        {
            if(OnLongGestureEnded != null)
            {
                OnLongGestureEnded(sender);
            }
        }

        private void Handle_OnFilterChosen(object sender, ChosenFilterEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Handling in list!");
            if(args.Toggled)
            {
                if(!ChosenFilters.Contains(args.Filter))
                {
                    ChosenFilters.Add(args.Filter);
                }   
            }
            else
            {
                ChosenFilters.Remove(args.Filter);
            }

            var chosenListArgs = new ChosenFiltersListEventArgs(ChosenFilters);
            OnFiltersChosen(sender, chosenListArgs);
                
        }
    }
}
