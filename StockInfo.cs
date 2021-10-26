using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketMonitorApp
{
    public class StockInfo
    {
        public string ticker { get; set; }
        public int currentPrice { get; set; }
        public int yearHigh { get; set; }
        public int peRatio { get; set; }
    }
}
