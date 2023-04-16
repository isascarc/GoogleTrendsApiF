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

#region searchOptions

// These are all the options offered by Google trends.
public enum DateOptions
{
    [Description("now 1-H")]
    LastHour,
    [Description("now 4-H")]
    LastFourHours,
    [Description("now 1-d")]
    LastDay,
    [Description("now 7-d")]
    LastWeek,
    [Description("today 1-m")]
    LastMonth,
    [Description("today 3-m")]
    LastThreeMonths,
    [Description("today 12-m")]
    LastYear,
    [Description("today 5-y")]
    LastFiveYears,
    [Description("all")]
    FromStart,
}

public enum GroupOptions
{
    [Description("")]
    All,
    [Description("images")]
    images,
    [Description("news")]
    news,
    [Description("youtube")]
    youtube,
    [Description("froogle")]
    froogle
}

#endregion

public static class Api
{
    const string baseTrendsUrl = "https://trends.google.com/trends";

    const string generalUrl = $"{baseTrendsUrl}/api/explore";
    const string INTEREST_OVER_TIME_URL = $"{baseTrendsUrl}/api/widgetdata/multiline";
    const string MULTIRANGE_INTEREST_OVER_TIME_URL = $"{baseTrendsUrl}/api/widgetdata/multirange";
    const string INTEREST_BY_REGION_URL = $"{baseTrendsUrl}/api/widgetdata/comparedgeo";
    const string relatedQueriesUrl = $"{baseTrendsUrl}/api/widgetdata/relatedsearches";
    const string trendingSearchesUrl = $"{baseTrendsUrl}/hottrends/visualize/internal/data";
    const string TOP_CHARTS_URL = $"{baseTrendsUrl}/api/topcharts";
    const string SUGGESTIONS_URL = $"{baseTrendsUrl}/api/autocomplete/";
    const string categoriesUrl = $"{baseTrendsUrl}/api/explore/pickers/category";
    const string todaySearchesUrl = $"{baseTrendsUrl}/api/dailytrends";
    const string REALTIME_TRENDING_SEARCHES_URL = $"{baseTrendsUrl}/api/realtimetrends";

    const int NormalCharsToTrim = 5;

    public static async Task<JsonNode> FetchData(string[] keyword, string geo = "", DateOptions time = DateOptions.LastHour,
        GroupOptions group = GroupOptions.All, int category = 0, string hl = "en-US", string tz = "300")
    {
        var query = new Query(geo, time.GetDescription(), keyword, category, group.GetDescription());

        var r1 = await GetCookiesAndData(new Uri($"https://trends.google.com/trends/api/explore?req={JsonSerializer.Serialize(query)}&hl={hl}&tz={tz}"),
            NormalCharsToTrim);
        var r2 = r1.Length > 0 ? JsonSerializer.Deserialize<TrendsRespond>(r1) : new TrendsRespond();

        var ret = new TrendsGetData(r2);



        string baseUrl = "https://trends.google.us/trends/api/widgetdata/";
        Uri dataUri = new Uri($"{baseUrl}multiline/json?req={JsonSerializer.Serialize(ret )}&token={ret.token}&tz=300&cat=3");
        
        return JsonNode.Parse(await GetData(dataUri));
        //return JsonNode.Parse(await ret.getTrendsJsonResponseAsync());
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
    /// Request data from Google Daily Trends section.
    /// </summary>
    /// <returns></returns>       
    public static async Task<JsonArray> GetTodaySearches(string geo = "US", string hl = "en-US")
    {
        var gg = await GetCookiesAndData(new Uri($"{todaySearchesUrl}?ns=15&geo={geo}&tz=-180&hl={hl}"), NormalCharsToTrim); //title.query
        return JsonNode.Parse(gg)?["default"]["trendingSearchesDays"][0]["trendingSearches"].AsArray();
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
    public static async Task<JsonArray> GetRelated(Query solicitud, int widget)
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
        var res = await GetCookiesAndData(new Uri($"{SUGGESTIONS_URL}{keyword}?hl={hl}"), NormalCharsToTrim);
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
    /// Gets an Description attribute on an enum field value
    /// </summary>
    private static string GetDescription(this Enum enumVal)
    {
        var type = enumVal.GetType();
        var memInfo = type.GetMember(enumVal.ToString());
        var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return (attributes.Length > 0) ? ((DescriptionAttribute)attributes[0]).Description : "";
    }
}