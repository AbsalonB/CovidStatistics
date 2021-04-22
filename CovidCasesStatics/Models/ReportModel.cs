namespace CovidCasesStatics.Models
{
    public class ReportModel
    {
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public ProvinceModel Region { get; set; }
    }
}
