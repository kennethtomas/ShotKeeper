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
                   "uwp=f9c9684c-d213-45e1-8467-2be45fd9d3f1;" +
                   "ios=2b84b8cc-50cc-41c3-9897-ba6394316aa9",
                   typeof(Analytics), typeof(Crashes));
        }
    }
}
