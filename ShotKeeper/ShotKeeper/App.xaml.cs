using Prism.Unity;
using ShotKeeper.Views;

namespace ShotKeeper
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            //NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
            NavigationService.NavigateAsync("ShotKeeperPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<ShotKeeperPage>();
            Container.RegisterTypeForNavigation<HomePage>();
        }
    }
}
