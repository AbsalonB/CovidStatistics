using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidCasesStatics.Helpers
{
    public class HttpHelper<T>
    {
        private readonly HttpClient _httpClient;
        public HttpHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", "RAPID_API_KEY" );
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" );
            _httpClient.DefaultRequestHeaders.Add("useQueryString","true" );
        }
        public async Task<T> GetRestServiceDataAsync(string serviceAddress)
        {
            _httpClient.BaseAddress = new Uri(serviceAddress);
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            response.EnsureSuccessStatusCode();
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }
    }
}
