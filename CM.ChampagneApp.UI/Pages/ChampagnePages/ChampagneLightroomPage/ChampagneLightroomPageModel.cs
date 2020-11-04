using System;

using Xamarin.Forms;
using FreshMvvm;
using CM.ChampagneApp.UI.PageModelInitData;
using System.Windows.Input;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;

namespace CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneLightroomPage
{
	public class ChampagneLightroomPageModel : FreshBasePageModel
    {
		public ICommand NavigateBack { get; set; }

		ChampagneLightroomInitData data = null;
		public string ImageUrl { get; set; }
		public string BottleName { get; set; }
		public string BrandName { get; set; }

        public ChampagneLightroomPageModel()
        {
			NavigateBack = new Command(async () => await OnNavigateBack());
        }

		public override void Init(object initData)
		{
			base.Init(initData);

			if(initData != null)
			{
				data = (ChampagneLightroomInitData)initData;
				ImageUrl = data.ImageUrl;
				BottleName = data.BottleName;
				BrandName = data.BrandName;
			}
		}


        private async Task OnNavigateBack()
		{
			await CoreMethods.PopPageModel();
		}
	}
}

