using CovidCasesStatics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidCasesStatics.Services
{
    public interface ICovidStatisticsService
    {
        Task<List<RegionModel>> GetAllRegionsAsync();
        Task<List<ReportModel>> GetReportDataAsync(string regionISO="");
    }
}
