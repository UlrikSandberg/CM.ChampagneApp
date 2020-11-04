using System;
using System.Windows.Input;
using FreshMvvm;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using CM.ChampagneApp.UI.PageModelInitData;
using System.Threading;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.UI.Pages.LoginSignupPages.WelcomeScreenPage.Helpers;

namespace CM.ChampagneApp.UI.Pages.LoginSignupPages.WelcomeScreenPage
{
    public class WelcomeScreenPageModel : FreshBasePageModel
    {
		WelcomeScreenInitData data = null;


		public bool IsCreatingUser { get; set; } = true;

		public ICommand AnimationDone { get; set; }

		private readonly IUICreateUserService createUserService;

		public WelcomeScreenPageModel(IUICreateUserService createUserService)
		{
			this.createUserService = createUserService;
            AnimationDone = new Command(OnAnimationDone);   
        }

        public override void Init(object initData)
        {
            base.Init(initData);

			if(initData != null)
			{
				data = (WelcomeScreenInitData)initData;

                SignUpUser();
			}
        }


        private async void SignUpUser()
		{

			var result = await createUserService.CreateUser(data.Email, data.Username, data.Password);
            if(!result.IsSuccesfull)
            {
                await CoreMethods.DisplayAlert("User creation error", result.Message, "OK");
                //Pop pageModel
				await CoreMethods.PopPageModel();
            }
            else
            {
				IsCreatingUser = false;
            }         
		}
      
        private async void OnAnimationDone()
		{
			await Task.Delay(200);
			var containerId = App.NavigationContainerManager.CreateMainContainer(true);
            await CoreMethods.PopToRoot(false);
            CoreMethods.SwitchOutRootNavigation(containerId);
			         
		}      
	}
}
