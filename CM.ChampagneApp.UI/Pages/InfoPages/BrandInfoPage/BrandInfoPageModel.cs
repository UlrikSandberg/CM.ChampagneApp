using System;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.UIFacade.Services;
using System.Windows.Input;
using CM.ChampagneApp.UI.UIFacade.Models.UIBrandModels;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;

namespace CM.ChampagneApp.UI.Pages.InfoPages.BrandInfoPage
{
	public class BrandInfoPageModel : BasePageModel
	{
        //Dataservices and initdata
        private readonly IUIBrandDataService _brandDataService;
        private BrandInfoPageModelInitData _initData;

        //DownloadManager
        public IDownloadManager<UIBrandInfoPageModel> DownloadManager { get; set; }

		public bool IsVintageUI { get; set; }
  
		private async Task<UIBrandInfoPageModel> DataEndpoint()
        {
            return await _brandDataService.GetBrandInfoPageAsync(_initData.BrandId, _initData.InfoPageId);
        }

        public BrandInfoPageModel(IUIBrandDataService brandDataService, IEventCollector ec) : base(ec)
        {
            //Dataservices
			_brandDataService = brandDataService;

            //Configure downloadingManager
            DownloadManager = new DownloadManager<UIBrandInfoPageModel, UIBrandInfoPageModel, UIBrandInfoPageModel>(DataEndpoint, () =>
            {
                if(DownloadManager.Data.uiTemplateIdentifier.Equals("ChampagneTemplate"))
                {
                    IsVintageUI = false;
                }
                else
                {
                    IsVintageUI = true;
                }
            });
            HasBackButton = true;
        }

        public override void Init(object initData)
		{
			base.Init(initData);

            if(initData != null)
            {
                _initData = initData as BrandInfoPageModelInitData;

                DownloadManager.FetchData();
            }
            else
            {
                throw new ArgumentException($"InitData for {nameof(BrandInfoPageModel)} can't be null --> Use initModel: {nameof(BrandInfoPageModelInitData)}");
            }
		}
	}
}

