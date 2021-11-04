using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MarketMonitorApp
{
    public class StockInfo
    {
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public int Volume { get; set; }
        public int Dividends { get; set; }

        [JsonProperty("Stock Splits")]
        public int StockSplits { get; set; }

    }

    class MarketHttpClient
    {
        
        public static async Task RetrievePrice(string ticker)
        {
            using var client = new HttpClient();
            string baseUrl = "https://xvfo29na9j.execute-api.us-west-2.amazonaws.com/PROD?ticker=";

            HttpResponseMessage response = await client.GetAsync($"{baseUrl}{ticker}");
            string responseBody = await response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(responseBody);
            // StockInfo stock = JsonConvert.DeserializeObject<StockInfo>(response);
        }
    }
}
 