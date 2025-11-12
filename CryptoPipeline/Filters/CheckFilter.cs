using CryptoPipeline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPipeline.Filters
{
    public class CheckFilter : IFilter<List<CryptoInfo>,List<CryptoInfo>>
    {
        public List<CryptoInfo> Process(List<CryptoInfo> input)
        {
            
            
            return input;
        }
    }
}
