using System;
using System.Collections.Generic;
using System.Windows.Input;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.UIReadModels;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.DTO.Builders;
using System.Linq;
using CM.ChampagneApp.DTO.Builders.Helpers;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Pages.SearchPages.Helpers;
using CM.ChampagneApp.UI.Pages.SearchPages.FilterSearchResultPage;

namespace CM.ChampagneApp.UI.Pages.SearchPages.FilterSearchPage
{
    public class FilterSearchPageModel : FreshBasePageModel
    {
        public ICommand NavigateBack { get; set; }

        public ICommand TypeFiltersChosen { get; set; }
        public ICommand StyleFiltersChosen { get; set; }
        public ICommand DosageFiltersChosen { get; set; }
        public ICommand StartFilterSearch { get; set; }
        public ICommand MinimumSearchRatingChosen { get; set; }

        //Configurations for Filter Search
        public IEnumerable<FilterSearchTag> ChampagneType { get; set; }
        public IEnumerable<FilterSearchTag> ChosenChampagneType { get; set; }
        public IEnumerable<FilterSearchTag> ChampagneStyle { get; set; }
        public IEnumerable<FilterSearchTag> ChosenChampagneStyle { get; set; }
        public IEnumerable<FilterSearchTag> ChampagneDosage { get; set; }
        public IEnumerable<FilterSearchTag> ChosenChampagneDosage { get; set; }
        public double ChosenMinimumSearchRating { get; set; }


		private const string NonVintageId = "NonVintage";
		private const string VintageId = "Vintage";

		private const string RoseId = "Rose";
		private const string BlancDeBlancId = "BlancDeBlancs";
		private const string BlancDeNoirId = "BlancDeNoirs";
		private const string OnIceId = "OnIce";
		private const string TradBrut = "TradBrut";
		private const string TradSweet = "TradSweet";

		private const string BrutNatureId = "BrutNature";
		private const string ExtraBrutId = "ExtraBrut";
		private const string BrutId = "BrutId";
		private const string ExtraDryId = "ExtraDry";
		private const string DryId = "Sec";
		private const string DemiSecId = "DemiSec";
		private const string DouxId = "Doux";

       
        public FilterSearchPageModel()
        {
            NavigateBack = new Command(OnNavigateBack);

            ChosenChampagneType = new List<FilterSearchTag>();
            ChosenChampagneStyle = new List<FilterSearchTag>();
            ChosenChampagneDosage = new List<FilterSearchTag>();

            TypeFiltersChosen = new Command<IEnumerable<FilterSearchTag>>(OnTypeFiltersChosen);
            StyleFiltersChosen = new Command<IEnumerable<FilterSearchTag>>(OnStyleFiltersChosen);
            DosageFiltersChosen = new Command<IEnumerable<FilterSearchTag>>(OnDosageFiltersChosen);
            StartFilterSearch = new Command(async () => await OnStartFilterSearch());
            MinimumSearchRatingChosen = new Command<double>(OnMinimumSearchRatingChosen);

            InitializeFilterSearch();
        }

        private void OnNavigateBack()
        {
            CoreMethods.PopPageModel();
        }

        private void InitializeFilterSearch()
        {
            var typeList = new List<FilterSearchTag>();
            var styleList = new List<FilterSearchTag>();
            var dosageList = new List<FilterSearchTag>();

            //***** CHAMPAGNE TYPE *****
            var nonVintage = new FilterSearchTag
            {
				Id = NonVintageId,
                Category = FilterCategories.Type,
                Title = "Non-Vintage",
                Definition = "Non-Vintage is described, in that the grapes used does not come from a single Vintage"
            };

            var Vintage = new FilterSearchTag
            {
				Id = VintageId,
                Category = FilterCategories.Type,
                Title = "Vintage",
                Definition = "Vintage champagne consists of 100% grapes from that exact vintage year, making a greater quality champagne."
            };

            typeList.Add(nonVintage);
            typeList.Add(Vintage);
            
            //***** CHAMPAGNE STYLE *****
            var BrutNV = new FilterSearchTag
            {
				Id = TradBrut,
                Category = FilterCategories.Style,
                Title = "Traditionel Brut",
                Definition = "Traditional Brut or commonly known as brut is the most common champagne style. Brut is perfectly balanced between dry and sweet."
            };

            var Rose = new FilterSearchTag
            {
				Id = RoseId,
                Category = FilterCategories.Style,
                Title = "Rosé",
                Definition = "A Rosé champagne means that during production a small amount of red wine is added to the champagne blend to ensure the many shades of pink colour."
            };

            
            var Sweet = new FilterSearchTag
            {
				Id = TradSweet,
                Category = FilterCategories.Style,
                Title = "Traditionel Sweet",
                Definition = "Traditional Sweet champagnes have a higher 'dose' of sugar which is added before bottling. And is neither Blanc de Blancs, Blanc de Noir nor Rosé"
            };

            var BlancDeBlancs = new FilterSearchTag
            {
				Id = BlancDeBlancId,
                Category = FilterCategories.Style,
                Title = "Blanc de Blancs",
                Definition = "Blanc de Blancs is a champagne produced exclusively from Chardonnay grapes."
            };

            var BlancDeNoirs = new FilterSearchTag
            {
				Id = BlancDeNoirId,
                Category = FilterCategories.Style,
                Title = "Blanc de Noirs",
                Definition = "The funny thing about Blanc de Noirs is that it has a beautiful pale colour, but is in fact produced exclusively from the juice of black grapes."
            };

            var OnIce = new FilterSearchTag
            {
				Id = OnIceId,
                Category = FilterCategories.Style,
                Title = "On Ice",
                Definition = "A new type of champagnes which have been created specifically to be enjoyed over ice and in cocktails with delicious ingredients."
            };

            styleList.Add(Rose);
            styleList.Add(BlancDeBlancs);
            styleList.Add(BlancDeNoirs);
            styleList.Add(OnIce);
			styleList.Add(BrutNV);         
            styleList.Add(Sweet);

            //***** CHAMPAGNE DOSAGE *****
            var BrutNature = new FilterSearchTag
            {
				Id = BrutNatureId,
                Category = FilterCategories.Dosage,
                Title = "Brut Nature",
                Definition = "Between 0-3 g/litre of sugar"
            };

            var ExtraBrut = new FilterSearchTag
            {
				Id = ExtraBrutId,
                Category = FilterCategories.Dosage,
                Title = "Extra Brut",
                Definition = "Between 0-6 g/litre of sugar"
            };

            var Brut = new FilterSearchTag
            {
				Id = BrutId,
                Category = FilterCategories.Dosage,
                Title = "Brut",
                Definition = "Between 6-12 g/litre of sugar"
            };

            var ExtraDry = new FilterSearchTag
            {
				Id = ExtraDryId,
                Category = FilterCategories.Dosage,
                Title = "Extra Dry",
                Definition = "Between 12-17 g/litre of sugar"
            };

            var Dry = new FilterSearchTag
            {
				Id = DryId,
                Category = FilterCategories.Dosage,
                Title = "Sec",
                Definition = "Between 17-32 g/litre of sugar"
            };

            var DemiSec = new FilterSearchTag
            {
				Id = DemiSecId,
                Category = FilterCategories.Dosage,
                Title = "Demi-Sec",
                Definition = "Between 32-50 g/litre of sugar"
            };

            var Doux = new FilterSearchTag
            {
                Id = DouxId,
                Category = FilterCategories.Dosage,
                Title = "Doux",
                Definition = "Between 50+ g/litre of sugar"
            };

            dosageList.Add(BrutNature);
            dosageList.Add(ExtraBrut);
            dosageList.Add(Brut);
            dosageList.Add(ExtraDry);
            dosageList.Add(Dry);
            dosageList.Add(DemiSec);
            dosageList.Add(Doux);

            ChampagneType = typeList;
            ChampagneStyle = styleList;
            ChampagneDosage = dosageList;

        }

        private void OnMinimumSearchRatingChosen(double minimumRating)
        {
            System.Diagnostics.Debug.WriteLine("Minimum search rating chosen: " + minimumRating);
            ChosenMinimumSearchRating = minimumRating;
        }

        private void OnTypeFiltersChosen(IEnumerable<FilterSearchTag> filters)
        {
            System.Diagnostics.Debug.WriteLine("Setting type filters");

            ChosenChampagneType = filters;
        }

        private void OnStyleFiltersChosen(IEnumerable<FilterSearchTag> filters)
        {
            System.Diagnostics.Debug.WriteLine("Setting style filters");
            ChosenChampagneStyle = filters;
        }

        private void OnDosageFiltersChosen(IEnumerable<FilterSearchTag> filters)
        {
            System.Diagnostics.Debug.WriteLine("Setting dosage filters");
            ChosenChampagneDosage = filters;
        }

        private async Task OnStartFilterSearch()
        {

			var filterSearchQueryBuilder = new FilterSearchQuery.FilterSearchQueryBuilder();

			foreach(var filter in ChosenChampagneType)
			{
				if(filter.Id.Equals(NonVintageId))
				{
					filterSearchQueryBuilder.SetIsNonVintage(true);
				}
				if(filter.Id.Equals(VintageId))
				{
					filterSearchQueryBuilder.SetIsVintage(true);
				}
			}


			foreach(var filter in ChosenChampagneStyle)
			{
				if(filter.Id.Equals(RoseId))
				{
					filterSearchQueryBuilder.SetChampagneStyle(ChampagneStyleEnum.Rose);
				}
				if(filter.Id.Equals(BlancDeBlancId))
				{
					filterSearchQueryBuilder.SetChampagneStyle(ChampagneStyleEnum.BlancDeBlanc);
				}
				if(filter.Id.Equals(BlancDeNoirId))
				{
					filterSearchQueryBuilder.SetChampagneStyle(ChampagneStyleEnum.BlancDeNoir);
				}
				if(filter.Id.Equals(OnIceId))
				{
					filterSearchQueryBuilder.SetChampagneStyle(ChampagneStyleEnum.OnIce);
				}
				if(filter.Id.Equals(TradBrut))
				{
					filterSearchQueryBuilder.SetChampagneStyle(ChampagneStyleEnum.TradBrut);
				}
				if(filter.Id.Equals(TradSweet))
				{
					filterSearchQueryBuilder.SetChampagneStyle(ChampagneStyleEnum.TradSweet);
				}
			}

			foreach(var filter in ChosenChampagneDosage)
			{
				if (filter.Id.Equals(BrutNatureId))
                {
					filterSearchQueryBuilder.SetChampagneDosage(ChampagneDosageEnum.BrutNature);
                }

				if (filter.Id.Equals(ExtraBrutId))
                {
					filterSearchQueryBuilder.SetChampagneDosage(ChampagneDosageEnum.ExtraBrut);
                }

				if (filter.Id.Equals(BrutId))
                {
					filterSearchQueryBuilder.SetChampagneDosage(ChampagneDosageEnum.Brut);
                }

				if (filter.Id.Equals(ExtraDryId))
                {
					filterSearchQueryBuilder.SetChampagneDosage(ChampagneDosageEnum.ExtraDry);
                }

				if (filter.Id.Equals(DryId))
                {
					filterSearchQueryBuilder.SetChampagneDosage(ChampagneDosageEnum.Sec);
                }

				if (filter.Id.Equals(DemiSecId))
                {
					filterSearchQueryBuilder.SetChampagneDosage(ChampagneDosageEnum.DemiSec);
                }

				if (filter.Id.Equals(DouxId))
                {
					filterSearchQueryBuilder.SetChampagneDosage(ChampagneDosageEnum.Doux);
                }            
			}

            filterSearchQueryBuilder.SetLowerRating(ChosenMinimumSearchRating).SetUpperRating(5.0);

			var filterSearchQuery = filterSearchQueryBuilder.Build();

			var searchResultInitData = new SearchResultsInitData(filterSearchQuery);         
            await CoreMethods.PushPageModel<FilterSearchResultPageModel>(searchResultInitData);
        }
    }
}
