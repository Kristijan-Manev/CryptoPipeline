using CryptoPipeline.Filters;

class Program
{
    static void Main()
    {
        var fetch = new FetchFilter();
        var check = new CheckFilter();
        var fill = new FillFilter();

        var step1 = fetch.Process(null);
        var step2 = check.Process(step1);
        var step3 = fill.Process(step2);
    }
}
