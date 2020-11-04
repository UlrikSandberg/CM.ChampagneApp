using System;
using CM.ChampagneApp.UI.Pages.SearchPages.FilterSearchPage;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.CustomLayouts
{
    public partial class FilterSearchContent : ContentView
    {

        public FilterSearchContent()
        {
            InitializeComponent();
        }

        void Handle_OnTypeFiltersChosen(object sender, CM.ChampagneApp.UI.CustomEventArgs.ChosenFiltersListEventArgs args)
        {
            var ViewModel = (FilterSearchPageModel)this.BindingContext;
            if(ViewModel.TypeFiltersChosen.CanExecute(args.ChosenFilters))
            {
                ViewModel.TypeFiltersChosen.Execute(args.ChosenFilters);
            }
        }

        void Handle_OnStyleFiltersChosen(object sender, CM.ChampagneApp.UI.CustomEventArgs.ChosenFiltersListEventArgs args)
        {
            var ViewModel = (FilterSearchPageModel)this.BindingContext;
            if (ViewModel.StyleFiltersChosen.CanExecute(args.ChosenFilters))
            {
                ViewModel.StyleFiltersChosen.Execute(args.ChosenFilters);
            }
        }

        void Handle_OnDosageFiltersChosen(object sender, CM.ChampagneApp.UI.CustomEventArgs.ChosenFiltersListEventArgs args)
        {
            var ViewModel = (FilterSearchPageModel)this.BindingContext;
            if (ViewModel.DosageFiltersChosen.CanExecute(args.ChosenFilters))
            {
                ViewModel.DosageFiltersChosen.Execute(args.ChosenFilters);
            }
        }

        void Handle_OnClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (FilterSearchPageModel)this.BindingContext;
            if(ViewModel.StartFilterSearch.CanExecute(null))
            {
                ViewModel.StartFilterSearch.Execute(null);
            }
        }

        void Handle_OnLongGestureType(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            TypeDefiniton.Definition = args.TextEntered;
        }

        void Handle_OnLongGestureStyle(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            StyleDefinition.Definition = args.TextEntered;
        }

        void Handle_OnLongGestureDosage(object sender, CM.ChampagneApp.UI.CustomEventArgs.TextEnteredEventArgs args)
        {
            DosageDefinition.Definition = args.TextEntered;
        }

        void Handle_OnLongGestureEndedType(object sender)
        {
            TypeDefiniton.Definition = "Hold here to read definition";
        }

        void Handle_OnLongGestureEndedStyle(object sender)
        {
            StyleDefinition.Definition = "Hold here to read definition";
        }

        void Handle_OnLongGestureEndedDosage(object sender)
        {
            DosageDefinition.Definition = "Hold here to read definition";
        }

        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            var roundedDouble = Math.Round(e.NewValue, 1);
            var roundedString = "";
            if(roundedDouble % 1 == 0)
            {
                roundedString = roundedDouble.ToString();
                roundedString += ".0";  
            }
            else
            {
                roundedString = roundedDouble.ToString();
            }
            MinimumRating.Text = roundedString;

            var ViewModel = (FilterSearchPageModel)this.BindingContext;
            if(ViewModel.MinimumSearchRatingChosen.CanExecute(roundedDouble))
            {
                ViewModel.MinimumSearchRatingChosen.Execute(roundedDouble);
            }
        }
    }
}
