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
using Newtonsoft.Json.Converters;

namespace QuickType
{
    public partial class Stock
    {
        [JsonProperty("Open")]
        public double Open { get; set; }

        [JsonProperty("High")]
        public double High { get; set; }

        [JsonProperty("Low")]
        public double Low { get; set; }

        [JsonProperty("Close")]
        public double Close { get; set; }

        [JsonProperty("Volume")]
        public long Volume { get; set; }

        [JsonProperty("Dividends")]
        public long Dividends { get; set; }

        [JsonProperty("Stock Splits")]
        public long StockSplits { get; set; }
    }

    public partial class Stock
    {
        public static List<Stock> FromJson(string json) => JsonConvert.DeserializeObject<List<Stock>>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Stock> self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                // new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
 