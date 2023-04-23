using GoogleTrendsApi;

namespace tester;

public class Tests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void Test1()
    {
        var t = Api.GetTopCharts(2022).Result;
        Assert.That(t, Has.Count.GreaterThan(1));

        var t2 = Api.GetAllTrendingSearches().Result;
        Assert.That(t2, Is.Not.Null);

        var t3 = Api.GetTrendingSearches("israel").Result;
        Assert.That(t3, Has.Count.GreaterThan(1));

        var t4 = Api.GetCategories().Result;
        Assert.That(t4, Is.Not.Null);
    }
}