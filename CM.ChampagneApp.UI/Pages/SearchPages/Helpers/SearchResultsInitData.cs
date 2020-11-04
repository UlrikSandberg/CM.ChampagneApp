using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.UIReadModels;
using CM.ChampagneApp.DTO.Builders;

namespace CM.ChampagneApp.UI.Pages.SearchPages.Helpers
{
	public class SearchResultsInitData
	{
		public FilterSearchQuery FilterSearchQuery { get; private set; }

		public SearchResultsInitData(FilterSearchQuery filterSearchQuery)
		{
			FilterSearchQuery = filterSearchQuery;
		}
	}
}
