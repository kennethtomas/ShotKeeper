using Prism.Unity;
using ShotKeeper.Views;
using Xamarin.Forms;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using ShotKeeper.Models;
using ShotKeeper.Interfaces;
using Xamarin.Forms.Xaml;

namespace ShotKeeper
{
    public partial class App : PrismApplication
    {
        static ShootingSessionDatabase database;

        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/ShootingSessionListPage");
        }

        public static ShootingSessionDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ShootingSessionDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("ShootingSessionSQLite.db3"));
                }
                return database;
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<ShootingSessionPage>();
            Container.RegisterTypeForNavigation<HomePage>();
            Container.RegisterTypeForNavigation<SessionsPage>();

            Container.RegisterTypeForNavigation<ShotKeeperCarouselPage>();
            Container.RegisterTypeForNavigation<ShotKeeperPage1>();
            Container.RegisterTypeForNavigation<ShootingSessionListPage>();

            Container.RegisterTypeForNavigation<ShootingSessionPage>();
        }

        protected override void OnStart()
        {
            base.OnStart();
            MobileCenter.Start("android=ecf7da19-2657-4192-b958-596960e69a79;" +
                   "uwp=f9c9684c-d213-45e1-8467-2be45fd9d3f1;" +
                   "ios=2b84b8cc-50cc-41c3-9897-ba6394316aa9",
                   typeof(Analytics), typeof(Crashes));
        }
    }
}
