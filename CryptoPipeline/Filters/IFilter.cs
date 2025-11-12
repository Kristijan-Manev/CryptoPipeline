using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPipeline.Filters
{
    public interface IFilter<TInput,TOutput>
    {
        TOutput Process(TInput input);
    }
}
