using System;
using System.Collections.Generic;
using System.Windows.Input;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Services;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.DTO.Models;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Pages.BrandPages.BrandCellarPage;
using CM.ChampagneApp.UI.Pages.SharedPages.WebViewPage;
using CM.ChampagneApp.UI.Pages.FollowingFollowersPages.FollowersPage;
using CM.ChampagneApp.UI.Pages.FollowingFollowersPages.Helpers;
using CM.ChampagneApp.UI.Pages.SharedPages.Helpers;
using CM.ChampagneApp.UI.Pages.Helpers;
using CM.ChampagneApp.UI.Helpers.Routing;
using CM.ChampagneApp.UI.Pages.BrandPages.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;

namespace CM.ChampagneApp.UI.Pages.BrandPages.BrandProfilePage
{
	public class BrandProfilePageModel : BasePageModel
	{      
		//DataServices and initData
		private readonly IUIBrandDataService _uIBrandDataService;
		private readonly IPageRouter _router;
		private BrandProfileInitData _initData;
		
		//DownloadManager and toggleManager
		public IDownloadManager<UIBrandProfile> DownloadManager { get; set; }
		public ToggleManager<string> ToggleManager { get; set; }
		
		public ICommand ClickedPage { get; set; }
		public ICommand CuveesClicked { get; set; }
		public ICommand FollowersClicked { get; set; }
		public ICommand SocialIconClicked { get; set; }

		private async Task<UIBrandProfile> DataEndpoint()
		{
			return await _uIBrandDataService.GetBrandProfile(_initData.BrandId);
		}
		
		public BrandProfilePageModel(IMicroInteractionService microInteractionService, IUIBrandDataService uIBrandDataService, IPageRouter router, IUIFollowLikeServie followLikeServie, IEventCollector ec) : base(ec)
        {
	        //Configure dataServices
			_uIBrandDataService = uIBrandDataService;
			_router = router;
			
			//Configure download and toggle managers...
			DownloadManager = new DownloadManager<UIBrandProfile, UIBrandProfile, UIBrandProfile>(DataEndpoint, () => ToggleManager.BoolToggle.ToggleState = DownloadManager.Data.IsRequesterFollowing, updateEndpoint: DataEndpoint, updateMapper:new BrandProfileUpdateMapper(), updateCompletionHandler: () => ToggleManager.BoolToggle.ToggleState = DownloadManager.Data.IsRequesterFollowing);
			OnReloadEvent += DownloadManager.UpdateHandler;
			ToggleManager = new ToggleManager<string>(microInteractionService,this, "FollowHouseTrue.png", "FollowHouseFalse.png",
				() =>
				{
					DownloadManager.Data.IsRequesterFollowing = true;
					DownloadManager.Data.NumberOfFollowers++;
				},
				() =>
				{
					DownloadManager.Data.IsRequesterFollowing = false;
					DownloadManager.Data.NumberOfFollowers--;
				});
			
			//Configure commands
			ClickedPage = new Command<string>(async (x) => await Handle_PageClicked(x));
			CuveesClicked = new Command(async () => await Handle_CuveesClicked());
			FollowersClicked = new Command(async () => await Handle_FollowersClicked());
			SocialIconClicked = new Command<string>(async (x) => await Handle_SocialIconClicked(x));

			HasBackButton = true;
        }

		public override void Init(object initData)
		{
			base.Init(initData);

            if (initData != null)
            {
	            _initData = initData as BrandProfileInitData;

	            //Download
	            DownloadManager.FetchData();
            }
            else
            {
	            throw new ArgumentException($"InitData for {nameof(BrandProfilePageModel)} can't be null --> Use initModel: {nameof(BrandProfileInitData)}");
            }
        }

		protected override async Task Handle_RightIconClicked()
		{
			if (DownloadManager.Data != null)
			{
				await ToggleManager.Handle_FollowToggle(DownloadManager.Data.Id, false);
			}
		}
        
        private async Task Handle_CuveesClicked()
        {
            if(_initData != null)
            {
	            await CoreMethods.PushPageModel<BrandCellarPageModel>(new BrandCellarInitData(_initData.BrandId));
            }
        }

        private async Task Handle_FollowersClicked()
        {
            if(_initData != null)
            {
                await CoreMethods.PushPageModel<FollowersPageModel>(new FollowersPageModelInitData(_initData.BrandId, FollowersType.Brand, username: DownloadManager.Data.Name));
            }
        }
        
        private async Task Handle_SocialIconClicked(string URL)
        {
	        await CoreMethods.PushPageModel<WebViewPageModel>(new WebViewInitData(URL));
        }
        
        private async Task Handle_PageClicked(string URLPath)
        {
	        await _router.RouteToPath(URLPath, CoreMethods);         
        }
    }
}
