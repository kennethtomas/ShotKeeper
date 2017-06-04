using Prism.Unity;
using ShotKeeper.Views;
using Xamarin.Forms;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;

namespace ShotKeeper
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            
            NavigationService.NavigateAsync("NavigationPage/SessionsPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<ShotKeeperPage>();
            Container.RegisterTypeForNavigation<HomePage>();
            Container.RegisterTypeForNavigation<SessionsPage>();

        }

        protected override void OnStart()
        {
            base.OnStart();
            MobileCenter.Start("android=6f484fd0-7301-4362-8cdc-a9072d7858b1;" +
                   "uwp={Your UWP App secret here};" +
                   "ios={Your iOS App secret here}",
                   typeof(Analytics), typeof(Crashes));
        }
    }
}
