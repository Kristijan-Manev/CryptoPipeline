using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPipeline.Models
{
    public class OHLCV
    {
        public long Time { get; set; }          
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
    }


    public class MarketData
    {
        public string Symbol { get; set; } = "";
        public List<OHLCV> HistoricalData { get; set; } = new List<OHLCV>();
    }
}
