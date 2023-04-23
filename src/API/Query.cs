using static GoogleTrendsApi.Api;

namespace GoogleTrendsApi;

public class Query
{
    public List<ComparisonItem> comparisonItem { get; set; }
    public int category { get; set; } = 0;
    public string property { get; set; } = "";

    public class ComparisonItem
    {
        public string keyword { get; set; }
        public string geo { get; set; }
        public string time { get; set; }
    }

    public Query(string geo, string time, string[] keyword,int cat, string proper )
    {
        category = cat;
        property = proper;
        comparisonItem = new List<ComparisonItem>();
        foreach (var item in keyword)
            comparisonItem.Add(new ComparisonItem() { keyword = item, geo = geo, time = time });
    }

    public void AddItem(string keyword, string geo, string time)
        => comparisonItem.Add(new ComparisonItem() { keyword = keyword, geo = geo, time = time });
}
