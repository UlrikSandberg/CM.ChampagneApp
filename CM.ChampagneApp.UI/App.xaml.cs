using System;
using FreshMvvm;
using Xamarin.Forms;
using CM.ChampagneApp.Business.Services;
using CM.ChampagneApp.Business.Configuration;
using Xamarin.Forms.Xaml;
using CM.ChampagneApp.UI;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using Microsoft.AppCenter.Distribute;
using CM.ChampagneApp.UI.UIFacade.Services;
using CM.ChampagneApp.Acq;
using CM.ChampagneApp.UI.Dependency;
using CM.ChampagneApp.UI.Elements.CustomContextActionViews;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.Helpers;
using CM.ChampagneApp.Instrumentation;
using CM.ChampagneApp.Instrumentation.Http;
using System.Net.Http;
using CM.ChampagneApp.Business.Caching;
using System.Collections.Generic;
using CM.ChampagneApp.UI.Pages.ChampagnePages.ChampagneProfilePage;
using CM.ChampagneApp.UI.Pages.LoginSignupPages.OnboardingPage;
using CM.ChampagneApp.UI.Pages.SharedPages.BaseInitPage;
using CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers;
using CM.ChampagneApp.UI.Helpers.Routing;
using CM.ChampagneApp.UI.Helpers.AccountService;
using CM.ChampagneApp.UI.Pages.Commons.ActiveCallTracker;
using Xamarin.Forms.PlatformConfiguration;
using Device = Xamarin.Forms.Device;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CM.ChampagneApp
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
                return null;

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source);

            return imageSource;
        }
    }

    public partial class App : Application
    {
        public static IAppConfiguration AppConfig { get; private set; }
        public static IDisplaySettings DisplaySettings { get; private set; }
        public static IAccountService AccountService { get; private set; }
        public static ICustomContextActionsManager ContextActionsManager { get; private set; }
        public static INavigationContainerService NavigationContainerManager { get; private set; }
        public static IMicroInterActionsQue MicroInteractionsQueManager { get; private set; }
        public static ITabbarBadgeService TabbarBadgeService { get; private set; }
        public static IActiveCallTracker ActiveCallTracker { get; private set; }
        public static bool IsOnIphoneX { get; set; }


        public static string NotificationReceivedKey = "NotificationReceived";

        public App()
        {
            InitializeComponent();

            RegisterDeviceDependentServices();
            ConfigureIOC();
            
            AppConfig = FreshIOC.Container.Resolve<IAppConfiguration>();
            

            //Set login container
            var loginPage = FreshPageModelResolver.ResolvePageModel<OnboardingPageModel>();
            var loginContainer = new InstrumentedNavigationContainer(loginPage, PageContainerIdentifiers.AuthenticationContainer, FreshIOC.Container.Resolve<IEventCollector>());

            //Set basic container -> Validate if the user is already loge.
            var page = FreshPageModelResolver.ResolvePageModel<BaseInitPageModel>();
            var basicNavContainer = new InstrumentedNavigationContainer(page, FreshIOC.Container.Resolve<IEventCollector>())
            {
                //Set statusbar text color to text color white.
                BarTextColor = Color.White
            };

            loginContainer.BarTextColor = Color.White;

            //Sets the main container of the app as basicNavContainer
            MainPage = basicNavContainer;
        }

        private void RegisterDeviceDependentServices()
        {
            DisplaySettings = DependencyService.Get<IDisplaySettings>();
            AccountService = DependencyService.Get<IAccountService>();
            ContextActionsManager = DependencyService.Get<ICustomContextActionsManager>();
            NavigationContainerManager = DependencyService.Get<INavigationContainerService>();
            MicroInteractionsQueManager = DependencyService.Get<IMicroInterActionsQue>();
            TabbarBadgeService = DependencyService.Get<ITabbarBadgeService>();
            ActiveCallTracker = DependencyService.Get<IActiveCallTracker>();

            if (Device.RuntimePlatform.Equals(Device.iOS))
            {
                ContextActionsManager?.SetCustomView(new DeleteContextActionView(), true);
                ContextActionsManager?.SetCustomView(new SaveContextActionView());   
            }
        }

        private void ConfigureIOC()
        {
            //Acq injection register
            FreshIOC.Container.Register<IBusinessAccountService, BussinessAccountService>();

            //Business injection register
            FreshIOC.Container.Register<IEventCollector, EventCollector>();

            FreshIOC.Container.Register<IHttpProxy, HttpProxy>().AsSingleton();

            FreshIOC.Container.Register<ICache, Cache>();

            FreshIOC.Container.Register<IPageRouter, PageRouter>();
            FreshIOC.Container.Register<IBrandDataService, BrandDataService>();
            FreshIOC.Container.Register<IChampagneDataService, ChampagneDataService>();
            FreshIOC.Container.Register<IAuthenticationService, AuthenticationService>();
            FreshIOC.Container.Register<IAppConfiguration, AppConfiguration>().AsSingleton();
            FreshIOC.Container.Register<IUserDataService, UserDataService>();
            FreshIOC.Container.Register<ICreateUserService, CreateUserService>();
            FreshIOC.Container.Register<IFollowLikeService, FollowLikeService>();
            FreshIOC.Container.Register<ITastingDataService, TastingDataService>();
            FreshIOC.Container.Register<INotificationRegistrationService, NotificationRegistrationService>();
            FreshIOC.Container.Register<ITop10ListDataService, Top10ListDataService>();

            //UI injection register
            FreshIOC.Container.Register<IUIUserDataService, UIUserDataService>();
            FreshIOC.Container.Register<ChampagneProfilePageModel, ChampagneProfilePageModel>();
            FreshIOC.Container.Register<IUIBrandDataService, UIBrandDataService>();
            FreshIOC.Container.Register<IUIChampagneDataService, UIChampagneDataService>();
            FreshIOC.Container.Register<IUIArticleDataService, UIArticleDataServices>();
            FreshIOC.Container.Register<IUIAuthenticationService, UIAuthenticationService>();
            FreshIOC.Container.Register<IUICreateUserService, UICreateUserService>();
            FreshIOC.Container.Register<IUIFollowLikeServie, UIFollowLikeService>();
            FreshIOC.Container.Register<IUITastingDataService, UITastingDataService>();
            FreshIOC.Container.Register<IUINotificationService, UINotificationService>();
            FreshIOC.Container.Register<IUITop10ListDataService, UITop10ListDataService>();


            //UI helper injection register
            FreshIOC.Container.Register<IMicroInteractionService, MicroInteractionService>();
        }

        
        protected override void OnStart()
        {
            var cfg = AppConfiguration.LocalAppSettings.AppCenter;

            if (cfg.EnableAppCenterIntegration)
            {
                var services = new List<Type>();
                if (cfg.EnableAnalytics) services.Add(typeof(Analytics));
                if (cfg.EnableCrashes) services.Add(typeof(Crashes));
                if (cfg.EnableDistribute) services.Add(typeof(Distribute));
                if (cfg.EnablePush) services.Add(typeof(Push));

                AppCenter.LogLevel = cfg.LogLevel;
                AppCenter.Start(cfg.AppSecret, services.ToArray());
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
