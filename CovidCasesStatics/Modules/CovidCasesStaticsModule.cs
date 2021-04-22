using CovidCasesStatics.Services;
using CovidCasesStatics.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidCasesStatics.Modules
{
    public class CovidCasesStaticsModule : NinjectModule
    {
        public override void Load()
        {
            //ViewModels
            Bind<MainPageViewModel>().ToSelf();

            //CoreServices
            Bind<ICovidStatisticsService>().To<CovidStatisticsServices>().InSingletonScope();
        }
    }
}
