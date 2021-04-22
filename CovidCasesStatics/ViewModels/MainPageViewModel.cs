using Acr.UserDialogs;
using CovidCasesStatics.Models;
using CovidCasesStatics.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace CovidCasesStatics.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly ICovidStatisticsService _service;
        public ObservableCollection<RegionModel> Regions { get; set; }

        private ObservableCollection<ReportListModel> _report;
        public ObservableCollection<ReportListModel> Report
        {
            get => _report;
            set
            {
                _report = value;
                OnPropertyChanged();
            }
        }
        public MainPageViewModel(ICovidStatisticsService service)
        {
            _service = service;
        }


        public async Task LoadReport(string iso = "")
        {
            try
            {
                List<ReportListModel> reportQuery = null;
                IsBusy = true;
                UserDialogs.Instance.ShowLoading("Consultando");
                var regions = await _service.GetAllRegionsAsync();
                var reports = await _service.GetReportDataAsync();
                Regions = new ObservableCollection<RegionModel>(regions);
                if (string.IsNullOrEmpty(iso))
                {
                    reportQuery = (from x in reports
                                   group x by x.Region.Iso into regionCases
                                   orderby regionCases.Sum(c => c.Confirmed) descending
                                   select new
                                   ReportListModel
                                   {
                                       Confirmed = regionCases.Sum(c => c.Confirmed),
                                       Deaths = regionCases.Sum(c => c.Deaths),
                                       RegionOrProvince = regions.SingleOrDefault(c => c.Iso == regionCases.Key).Name
                                   }
                                         ).Take(10).ToList();
                }
                else
                {
                    reportQuery = (from x in reports
                                   group x by new { x.Region.Iso, x.Region.Province } into provinceCases
                                   orderby provinceCases.Sum(c => c.Confirmed) descending
                                   where provinceCases.Key.Iso == iso
                                   select new
                                   ReportListModel
                                   {
                                       Confirmed = provinceCases.Sum(c => c.Confirmed),
                                       Deaths = provinceCases.Sum(c => c.Deaths),
                                       RegionOrProvince = provinceCases.Key.Province
                                   }
                          ).Take(10).ToList();
                }
                Report = new ObservableCollection<ReportListModel>(reportQuery);
                UserDialogs.Instance.HideLoading();
                IsBusy = false;
            }
            catch (System.Exception)
            { 
            } 
        }
    }
}
