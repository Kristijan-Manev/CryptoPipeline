using CryptoPipeline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoPipeline.Filters
{
    public class FillFilter : IFilter<List<CryptoInfo>,List<MarketData>>
    {
        private readonly HttpClient _http;

        private readonly string _apiKey = "YOUR_CRYPTOCOMPARE_API_KEY";

        public FillFilter()
        {
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        }

        public List<MarketData> Process(List<CryptoInfo> cryptos)
        {
            var marketDataList = new List<MarketData>();

            foreach (var crypto in cryptos.Take(5)) // start with top 5 for testing
            {
                try
                {
                    var url = $"https://min-api.cryptocompare.com/data/v2/histoday?fsym={crypto.Symbol.ToUpper()}&tsym=USD&limit=2000&api_key={_apiKey}";
                    var json = _http.GetStringAsync(url).Result;

                    // Parse JSON
                    using var doc = JsonDocument.Parse(json);
                    var dataArray = doc.RootElement.GetProperty("Data").GetProperty("Data").EnumerateArray();

                    var ohlcvList = dataArray.Select(d => new OHLCV
                    {
                        Time = d.GetProperty("time").GetInt64(),
                        Open = d.GetProperty("open").GetDouble(),
                        High = d.GetProperty("high").GetDouble(),
                        Low = d.GetProperty("low").GetDouble(),
                        Close = d.GetProperty("close").GetDouble(),
                        Volume = d.GetProperty("volumeto").GetDouble()
                    }).ToList();

                    marketDataList.Add(new MarketData
                    {
                        Symbol = crypto.Symbol,
                        HistoricalData = ohlcvList
                    });

                    Console.WriteLine($"Fetched {ohlcvList.Count} days of data for {crypto.Symbol}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching {crypto.Symbol}: {ex.Message}");
                }
            }

            return marketDataList;
        }
    }
}
