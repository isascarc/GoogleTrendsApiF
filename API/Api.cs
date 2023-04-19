global using System;
global using System.Text.Json;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Net;
global using System.Net.Http;
using System.Text.Json.Nodes;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;


namespace GoogleTrendsApi;

public static class Api
{
    const string baseTrendsUrl = "https://trends.google.com/trends";

    const string generalUrl = $"{baseTrendsUrl}/api/explore";
    const string interestOverTimeUrl = $"{baseTrendsUrl}/api/widgetdata/multiline";
    const string multyrangeInterestOverTimeUrl = $"{baseTrendsUrl}/api/widgetdata/multirange";
    const string interestByRegionUrl = $"{baseTrendsUrl}/api/widgetdata/comparedgeo";
    const string relatedQueriesUrl = $"{baseTrendsUrl}/api/widgetdata/relatedsearches";
    const string trendingSearchesUrl = $"{baseTrendsUrl}/hottrends/visualize/internal/data";
    const string topChartsUrl = $"{baseTrendsUrl}/api/topcharts";
    const string suggestionsUrl = $"{baseTrendsUrl}/api/autocomplete/";
    const string categoriesUrl = $"{baseTrendsUrl}/api/explore/pickers/category";
    const string todaySearchesUrl = $"{baseTrendsUrl}/api/dailytrends";
    const string realtimeTrendingSearchesUrl = $"{baseTrendsUrl}/api/realtimetrends";

    const int NormalCharsToTrim = 5;

    /// <summary>
    /// Request data from Google's Interest Over Time section.
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="geo"></param>
    /// <param name="time"></param>
    /// <param name="group"></param>
    /// <param name="category"></param>
    /// <param name="hl"></param>
    /// <param name="tz"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static async Task<JsonNode> GetInterestOverTime(string[] keyword, string geo = "", DateOptions time = DateOptions.LastHour,
        GroupOptions group = GroupOptions.All, int category = 0, string hl = "en-US", string tz = "300")
    {  
        // Get token
        var query = new Query(geo, time.GetDescription(), keyword, category, group.GetDescription());
        var r1 = await GetCookiesAndData(new Uri($"{generalUrl}?req={JsonSerializer.Serialize(query)}&hl={hl}&tz={tz}"), NormalCharsToTrim);
        if (r1.Length < 1)
            throw new Exception("Something went wrong");
            
        var r2 = JsonSerializer.Deserialize<TrendsRespond>(r1);
        var r3 = new TrendsGetData(r2);
        
        var r5 = JsonNode.Parse(r1)["widgets"][0];

        // Get date
        Uri dataUri = new($"{interestOverTimeUrl}/json?req={JsonSerializer.Serialize(r3)}&token={r5["token"]}&tz={tz}");
        return JsonNode.Parse(await GetData(dataUri));
    }

    public async static Task<JsonObject> GetAllTrendingSearches()
    {
        var r = await GetCookiesAndData(new Uri(trendingSearchesUrl));
        return JsonNode.Parse(r)?.AsObject();
    }

    public async static Task<JsonArray> GetTrendingSearches(string country = "united_states")
    {
        var result = await GetAllTrendingSearches();
        return result.ContainsKey(country) ? result[country].AsArray() : new JsonArray();
    }

    /// <summary>
    /// Request data from Google Realtime Search Trends section.
    /// </summary>
    /// <param name="country"></param>
    /// <returns></returns>
    public async static Task<JsonArray> GetRealtimeSearches(string geo = "US", Category cat = Category.ScienceAndTech, int count = 300)
    {
        // Don't know what some of the params mean here, followed the nodejs library https://github.com/pat310/google-trends-api/ 's implemenration.
        int riValue = 300;
        if (count < riValue)
            riValue = count;

        int rsValue = 200;
        if (count < rsValue)
            rsValue = count - 1;

        // Get date
        var r1 = await GetCookiesAndData(new Uri($"{realtimeTrendingSearchesUrl}?ns=15&geo={geo}&tz=-300&hl=en-US&cat={cat.GetDescription()}&fi=0&fs=0&ri={riValue}&rs={rsValue}&sort=0"), NormalCharsToTrim);
        return JsonNode.Parse(r1)["storySummaries"]["trendingStories"].AsArray();
    }

    /// <summary>
    /// Request data from Google's Top Charts.
    /// </summary>
    /// <param name="date"></param>
    /// <param name="hl"></param>
    /// <param name="tz"></param>
    /// <param name="geo"></param>
    /// <returns>An array that contains a list of objects, such as: Searches, News, People, and many more.</returns>
    public static async Task<JsonArray> GetTopCharts(int yearNumber, string hl = "en-US", int tz = 300, string geo = "GLOBAL")
    {
        var res = await GetCookiesAndData(new Uri($"{topChartsUrl}?isMobile=false&date={yearNumber}&geo={geo}&tz={tz}&hl={hl}"), NormalCharsToTrim);
        return JsonNode.Parse(res)["topCharts"].AsArray();
    }


    /// <summary>
    /// Request data from Google's Interest by Region section.
    /// </summary>
    /// <param name="resolution"></param>
    /// <param name="inc_low_vol"></param>
    /// <param name="inc_geo_code"></param>
    /// <returns></returns>
    public static async Task<JsonNode> GetInterestByRegion(string[] keywords, DateOptions date, string resolution = "COUNTRY", string geo = "US",
        bool inc_low_vol = false, int cat = 0, GroupOptions groupOptions = GroupOptions.All, bool inc_geo_code = false, int tz = 300)
    {
        // Get widget
        var query = new Query(geo, date.GetDescription(), keywords, cat, groupOptions.GetDescription());
        var res1 = await GetTokens(query, 1);

        // ----
        if (geo == "" || (geo == "US" && new string[] { "DMA", "CITY", "REGION" }.Contains(resolution)))
            res1["request"]["resolution"] = resolution;
        res1["request"]["includeLowSearchVolumeGeos"] = inc_low_vol;

        Uri regionUrl = new($"{interestByRegionUrl}?req={JsonSerializer.Serialize(res1["request"])}&token={res1["token"]}&tz={tz}");
        var res = JsonNode.Parse(await GetCookiesAndData(regionUrl, NormalCharsToTrim));

        return res;

        // todo: edit results (lines 352-370)
    }



    /// <summary>
    /// Request data from Google's Interest Over Time section.
    /// </summary>
    /// <param name="hl"></param>
    /// <param name="tz"></param>
    /// <param name="geo"></param>
    /// <returns></returns>
    public static async Task GetInterestOverTime(string hl = "en-US", int tz = 300, string geo = "GLOBAL")
    {
        //var _res = await GetCookiesAndData(new Uri($"https://trends.google.com/trends/api/explore?req=" +
        //   $"{JsonSerializer.Serialize(solicitud)}&hl=he-IL&tz=300"), 4);

        //var widgets = JsonNode.Parse(_res)["widgets"].AsArray();
        //var res1 = widgets.FirstOrDefault(x => x["id"].ToString() == options[widget]);

        //var res = await GetCookiesAndData(new Uri
        //    ($"{INTEREST_OVER_TIME_URL}?req=false&token={yearNumber}&tz={tz}"), NormalCharsToTrim);
        //return JsonNode.Parse(res)["topCharts"].AsArray();
    }

    /// <summary>
    /// For geoMap - take one only.
    /// </summary>
    /// <param name="solicitud"></param>
    /// <param name="widget"></param>
    /// <returns></returns>
    public static async Task<JsonNode> GetTokens(Query solicitud, int widget)
    {
        var options = new string[] { "TIMESERIES", "GEO_MAP" };

        var _res = await GetCookiesAndData(new Uri($"https://trends.google.com/trends/api/explore?req=" +
           $"{JsonSerializer.Serialize(solicitud)}&hl=he-IL&tz=300"), NormalCharsToTrim);


        var widgets = JsonNode.Parse(_res)["widgets"].AsArray();
        return widgets.FirstOrDefault(x => x["id"].ToString() == options[widget]);
    }



    /// <summary>
    /// Request data from Google Daily Trends section.
    /// </summary>
    /// <returns></returns>       
    public static async Task<JsonArray> GetTodaySearches(string geo = "US", string hl = "en-US")
    {
        var res = await GetCookiesAndData(new Uri($"{todaySearchesUrl}?ns=15&geo={geo}&tz=-180&hl={hl}"), NormalCharsToTrim); //title.query
        return JsonNode.Parse(res)?["default"]["trendingSearchesDays"][0]["trendingSearches"].AsArray();
    }


    public static async Task<JsonArray> GetRelatedQueries(string[] keyword, string geo = "", DateOptions time = DateOptions.LastThreeMonths,
        GroupOptions group = GroupOptions.All, int category = 0)
    {
        return await GetRelated(new Query(geo, time.GetDescription(), keyword, category, group.GetDescription()), 1);
    }

    public static async Task<JsonArray> GetRelatedTopics(string[] keyword, string geo = "", DateOptions time = DateOptions.LastThreeMonths,
    GroupOptions group = GroupOptions.All, int category = 0)
    {
        return await GetRelated(new Query(geo, time.GetDescription(), keyword, category, group.GetDescription()), 0);
    }

    /// <summary>
    /// Get related queries / topics (by widget parameter).
    /// </summary>
    /// <param name="solicitud"></param>
    /// <param name="widget"> 1 for topics, 2 for queries.</param>
    /// <returns></returns>
    private static async Task<JsonArray> GetRelated(Query solicitud, int widget)
    {
        var options = new string[] { "RELATED_TOPICS", "RELATED_QUERIES" };

        var _res = await GetCookiesAndData(new Uri($"https://trends.google.com/trends/api/explore?req=" +
           $"{JsonSerializer.Serialize(solicitud)}&hl=he-IL&tz=300"), NormalCharsToTrim);

        var widgets = JsonNode.Parse(_res)["widgets"].AsArray();
        var res1 = widgets.FirstOrDefault(x => x["id"].ToString() == options[widget]);

        var relateds = await GetCookiesAndData(new Uri(relatedQueriesUrl + $"?token={res1["token"]}&req={res1["request"]}"), NormalCharsToTrim);
        return JsonNode.Parse(relateds)["default"]["rankedList"].AsArray();
    }


    /// <summary>
    /// Request available categories data from Google's API.
    /// </summary>
    /// <returns></returns>
    public static async Task<string> GetCategories(string hl = "en-US")
    {
        return await GetCookiesAndData(new Uri($"{categoriesUrl}?hl={hl}"), NormalCharsToTrim);
    }

    /// <summary>
    /// Request data from Google's Keyword Suggestion.
    /// </summary>
    /// <param name="keyword"></param>
    /// <param name="hl"></param>
    /// <returns></returns>
    public static async Task<JsonArray> GetSuggestions(string keyword, string hl = "en-US")
    {
        var res = await GetCookiesAndData(new Uri($"{suggestionsUrl}{keyword}?hl={hl}"), NormalCharsToTrim);
        return JsonNode.Parse(res)["default"]["topics"].AsArray();
    }



    public static async Task<string> GetCookiesAndData(Uri url, int trimChars = 0)
    {
        var cookieContainer = new CookieContainer();

        using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
        using (var client = new HttpClient(handler))
        {
            HttpRequestMessage request = new(HttpMethod.Post, url.OriginalString);
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Host", url.Host);
            await client.SendAsync(request);
        }

        using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer ?? new CookieContainer() })
        using (var client = new HttpClient(handler) { })
        {
            HttpRequestMessage request = new(HttpMethod.Get, url.OriginalString);
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Host", url.Host);

            var a = await client.SendAsync(request);
            var b = await a.Content.ReadAsStringAsync();
            string clearResult = b[trimChars..];
            return cookieContainer is null ? GetDataFinal(clearResult) : clearResult;
        }
    }

    public static async Task<string> GetData(Uri url, CookieContainer cookies = null, bool trim = true)
    {
        using (var handler = new HttpClientHandler() { CookieContainer = cookies ?? new CookieContainer() })
        using (var client = new HttpClient(handler) { })
        {
            var cc = url.OriginalString;
            HttpRequestMessage request = new(HttpMethod.Get, cc);
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Host", url.Host);
            var a = await client.SendAsync(request);
            var b = await a.Content.ReadAsStringAsync();
            string clearResult = b[NormalCharsToTrim..];
            return cookies is null ? GetDataFinal(clearResult) : clearResult;
        }
    }

    public static string GetDataFinal(string data)
    {
        using (Stream st = GenerateStreamFromString(data))
        {
            var jsonReceived = JsonNode.Parse(st)["default"]["timelineData"];
            var jsonResult = jsonReceived.AsArray().Select(x => new { date = x["time"], value = x["value"] });
            return JsonSerializer.Serialize(jsonResult);
        }
    }

    public static MemoryStream GenerateStreamFromString(string value)
        => new(Encoding.UTF8.GetBytes(value ?? ""));


    /// <summary>
    /// Gets an Description attribute on an enum field value.
    /// </summary>
    private static string GetDescription(this Enum enumVal)
    {
        var type = enumVal.GetType();
        var memInfo = type.GetMember(enumVal.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return (attributes.Length > 0) ? ((DescriptionAttribute)attributes[0]).Description : "";
    }
}