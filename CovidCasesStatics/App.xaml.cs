using CovidCasesStatics.Modules;
using CovidCasesStatics.ViewModels;
using CovidCasesStatics.Views;
using Ninject;
using Ninject.Modules;
using Xamarin.Forms;

namespace CovidCasesStatics
{
    public partial class App : Application
    {
        public IKernel Kernel { get; set; }
        public App(params INinjectModule[] platformModules)
        {
            InitializeComponent();
            // Register core services
            Kernel = new StandardKernel(new CovidCasesStaticsModule());
            Kernel.Load(platformModules);
            var mainPage = new NavigationPage(new MainPage())
            {
                BindingContext = Kernel.Get<MainPageViewModel>()
            };
            MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
