using System;
using System.Windows.Input;
using Xamarin.Forms;
using CM.ChampagneApp.UI.PageModelInitData;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.UI.UIFacade.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.Instrumentation;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage;
using CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers;
using CM.ChampagneApp.UI.Pages.Commons;
using CM.ChampagneApp.UI.Pages.Helpers;
using System.Collections.Generic;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;

namespace CM.ChampagneApp.UI.Pages.BrandPages.BrandCellarPage           
{
	public class BrandCellarPageModel : BasePageModel
    {
	    //DataServices and initData
	    private readonly IUIChampagneDataService _champagneDataService;
	    private readonly IUIBrandDataService _uIBrandDataService;
	    private BrandCellarInitData _initData;
	    
	    //DownloadManager and vintageArchiveManager
	    public IDownloadManager<UIBrandCellar> DownloadManager { get; set; }
	    public VintageArchiveManager<UIChampagneLight> VintageArchive { get; set; }
		    
        public ICommand ChampagneClicked { get; set; }
        public ICommand VintageArchiveClicked { get; set; }

        private async Task<IEnumerable<UIChampagneRoot>> ChampagnePagingEndpoint(int page, int pageSize)
        {
            return await _champagneDataService.GetChampagneRoots(page, pageSize);
        }

        public BrandCellarPageModel(IUIChampagneDataService ChampagneDataService, IUIBrandDataService uIBrandDataService, IEventCollector ec) : base(ec)
		{
			//Configure dataservices
			_champagneDataService = ChampagneDataService;
			_uIBrandDataService = uIBrandDataService;
			
			//Configure DownloadManager and vintageArchiveManager
			DownloadManager = new DownloadManager<UIBrandCellar,UIBrandCellar,UIBrandCellar>(
				async () => await _uIBrandDataService.GetBrandCellar(_initData.BrandId));
            VintageArchive = new VintageArchiveManager<UIChampagneLight>(_champagneDataService.GetBottlesFromRoot);
			
			//Configure commands
			ChampagneClicked = new Command<UIChampagneRoot>(async (x) => await Handle_ChampagneClicked(x));
            VintageArchiveClicked = new Command<Guid>(async (x) => await Handle_VintageArchiveClicked(x));

			HasBackButton = true;
		}      
        
        public override void Init(object initData)
        {
            base.Init(initData);         
         
			if(initData != null)
			{
				_initData = initData as BrandCellarInitData;

				DownloadManager.FetchData();
			}
			else
			{
				throw new ArgumentException($"InitData for {nameof(BrandCellarPageModel)} can't be null --> Use initModel: {nameof(BrandCellarInitData)}");
			}
        }

        private async Task Handle_ChampagneClicked(UIChampagneRoot champagne)
        {
            if (champagne.ChampagneIds.Count == 1)
            {
                await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(champagne.ChampagneIds[0]));
            }
            else if (champagne.ChampagneIds.Count > 1)
            {
                await VintageArchive.DownloadVintageArchive(champagne.Id, champagne.FolderContentType);
            }
        }

        private async Task Handle_VintageArchiveClicked(Guid id)
        {
            await CoreMethods.PushPageModel<ChampagneProfilePageModel>(new ChampagneProfileInitData(id));
        }
    }
}
