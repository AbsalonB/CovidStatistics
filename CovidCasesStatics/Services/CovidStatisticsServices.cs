using CovidCasesStatics.Helpers;
using CovidCasesStatics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidCasesStatics.Services
{
    public class CovidStatisticsServices : ICovidStatisticsService
    {
        private readonly string API_URI;
        public CovidStatisticsServices()
        {
            API_URI = "https://covid-19-statistics.p.rapidapi.com";

        }
        public async Task<List<RegionModel>> GetAllRegionsAsync()
        {
            var uri = ApiConfiguration.Regions(API_URI);
            var helper = new HttpHelper<BaseApiResponse<List<RegionModel>>>();
            var res = await helper.GetRestServiceDataAsync(uri);
            return res.Data;
        }

        public async Task<List<ReportModel>> GetReportDataAsync(string regionISO = "")
        {
            var uri = ApiConfiguration.Reports(API_URI) + (!string.IsNullOrEmpty(regionISO) ? $"?iso={regionISO}" : ""); ;
            var helper = new HttpHelper<BaseApiResponse<List<ReportModel>>>();
            var res = await helper.GetRestServiceDataAsync(uri);
            return res.Data;
        }
    }
}
