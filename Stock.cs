using Newtonsoft.Json;

namespace StockMarket
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

}
