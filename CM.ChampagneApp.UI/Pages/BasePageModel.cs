using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.UI.Helpers;
using FreshMvvm;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages
{
	public abstract class BasePageModel : FreshBasePageModel
    {
        public delegate void ReloadEvent(object sender, System.EventArgs e);
        public event ReloadEvent OnReloadEvent;

        public bool HasBackButton { get; set; } = false;

        //Pop current pageModel...
        public ICommand NavigateBack { get; set; }
        
        public ICommand RightIconClicked { get; set; }

        //User request a reconnect...
		public ICommand Reconnect { get; set; }

        //When the page is out of service
		public bool IsOutOfService { get; set; } = false;

        //The first time the vies is rendered, not viedDidLoad but ViewDidAppearInit
        public bool ViewDidAppearInit { get; set; } = true;

        //The view is shown additional time after intial load. 
        public bool ShouldReload { get; set; } = false;
        protected IEventCollector EventCollector { get; }

        protected BasePageModel(IEventCollector eventCollector)
        {
           
            eventCollector.TrackPageView(GetType().Name);
			Reconnect = new Command(async () => await OnReconnect());
			NavigateBack = new Command(async () => await OnNavigateBack());
			RightIconClicked = new Command(async () => await Handle_RightIconClicked());
            EventCollector = eventCollector;
        }

		protected override void ViewIsAppearing(object sender, EventArgs e)
		{
			if(!ShouldReload)
			{
				ShouldReload = true;
			}
			else
			{
				Reload().RunForget();
                OnReloadEvent?.Invoke(this, new System.EventArgs());
            }

            if(ViewDidAppearInit)
            {
                ViewDidAppearInit = false;
                ViewDidAppearInitial().RunForget();
            }

            base.ViewIsAppearing(sender, e);
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        protected virtual async Task Reload() { }
        protected virtual async Task ViewDidAppearInitial() { }
        protected virtual async Task OnReconnect() { }
        protected virtual async Task Handle_RightIconClicked() {}
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        protected async virtual Task OnNavigateBack()
		{
			await CoreMethods.PopPageModel();
		}

        public async Task DisplayAlert(string msg)
        {
            await CoreMethods.DisplayAlert("Error: ", msg + "\n Try Again", "OK");
        }
    }
}

