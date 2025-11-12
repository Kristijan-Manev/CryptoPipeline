using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using CryptoPipeline.Models;

namespace CryptoPipeline.Filters
{
    public class FetchFilter : IFilter<object, List<CryptoInfo>>
    {
        private readonly HttpClient _http;

        public FetchFilter()
        {
            _http = new HttpClient();
            _http.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
        }

        public List<CryptoInfo> Process(object input)
        {
            var allCryptos = new List<CryptoInfo>();

            for (int i = 1; i <= 4; i++)
            {
                var url = $"https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd&order=market_cap_desc&per_page=250&page={i}";
                var responseMessage = _http.GetAsync(url).Result;

                if (!responseMessage.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error fetching page {i}: {responseMessage.StatusCode}");
                    continue;
                }

                var response = responseMessage.Content.ReadFromJsonAsync<List<CryptoInfo>>().Result;
                if (response != null)
                    allCryptos.AddRange(response);

            }

            Console.WriteLine($"Fetched {allCryptos.Count} cryptos.");
            return allCryptos;
        }
    }
}
