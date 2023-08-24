using GoogleTrendsApi;
using NUnit.Framework;

namespace tester;

public class Tests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void ApiTest()
    {
        var t = Api.GetTopCharts(2022).Result;
        Assert.That(t, Has.Count.GreaterThan(1));

        var t2 = Api.GetAllTrendingSearches().Result;
        Assert.That(t2, Is.Not.Null);

        var t3 = Api.GetTrendingSearches("israel").Result;
        Assert.That(t3, Has.Count.GreaterThan(10));

        var t4 = Api.GetCategories().Result;
        Assert.That(t4, Is.Not.Null);

        var t6 = Api.GetTodaySearches().Result;
        Assert.That(t6, Is.Not.Null);

        var t5 = Api.GetInterestOverTimeTyped(new string[] { "angular" }, GeoId.Israel, DateOptions.LastThreeMonths, GroupOptions.All).Result;
        Assert.That(t5.AsArray()?[0]?.AsObject().ContainsKey("date"), Is.True);
    }
}