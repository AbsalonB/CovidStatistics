using System;
using System.Collections.Generic;
using System.Text;

namespace CovidCasesStatics.Helpers
{
    public static class ApiConfiguration
    {
        public static string Regions(string baseUri) => $"{baseUri}/regions";
        public static string Provinces(string baseUri) => $"{baseUri}/provinces";
        public static string Reports(string baseUri) => $"{baseUri}/reports";
    }
}
