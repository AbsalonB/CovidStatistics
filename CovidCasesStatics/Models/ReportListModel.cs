using System;
using System.Collections.Generic;
using System.Text;

namespace CovidCasesStatics.Models
{
    public class ReportListModel
    {
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public string RegionOrProvince { get; set; }
    }
}
