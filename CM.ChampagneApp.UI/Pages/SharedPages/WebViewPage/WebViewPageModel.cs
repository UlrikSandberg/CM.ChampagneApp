using System;
using System.Windows.Input;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.Pages.SharedPages.Helpers;
using FreshMvvm;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.SharedPages.WebViewPage
{
    public class WebViewPageModel : FreshBasePageModel
    {

        public string Url { get; set; }

        public ICommand NavigateBack { get; set; }

        public WebViewPageModel()
        {

            NavigateBack = new Command(OnNavigateBack);
        }

		public override void Init(object initData)
		{
            base.Init(initData);

            var Data = (WebViewInitData)initData;

            System.Diagnostics.Debug.WriteLine("Should open web view with url: "+ Data.WebUrl);
            Url = Data.WebUrl;
		}

        private void OnNavigateBack()
        {
            CoreMethods.PopPageModel();
        }
	}
}
