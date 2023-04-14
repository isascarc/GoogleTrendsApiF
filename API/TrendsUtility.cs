using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;

namespace GoogleTrendsApi;

class TrendsUtility
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


    public async Task<TrendsRespond> GetTrendsRespondSolicitud(Query solicitud)
    {
        var res = await GetCookiesAndData(new Uri($"https://trends.google.com/trends/api/explore?req=" + //"cat=13&date=today 1-m&geo=IL"));
            $"{JsonSerializer.Serialize(solicitud)}&hl=he-IL&tz=300"), NormalCharsToTrim);
        return res.Length > 0 ? JsonSerializer.Deserialize<TrendsRespond>(res) : new TrendsRespond();
    }

    public async Task<string> GetTrends2(Query solicitud)
    {
        var res = await GetCookiesAndData(new Uri($"https://trends.google.com/trends/api/explore?req=" +
           $"{JsonSerializer.Serialize(solicitud)}&hl=he-IL&tz=300"), NormalCharsToTrim);
        return res;
    }

    public async Task<JsonArray> GetTrendsRelated(Query solicitud)
    {
        var _res = await GetCookiesAndData(new Uri($"https://trends.google.com/trends/api/explore?req=" +
           $"{JsonSerializer.Serialize(solicitud)}&hl=he-IL&tz=300"), NormalCharsToTrim);
        var res = JsonNode.Parse(_res);
        var r = res["widgets"][2];
        var relatedQueries =await  GetCookiesAndData(new Uri(relatedQueriesUrl + $"?token={r["token"]}&req={r["request"]}"), NormalCharsToTrim);
        return JsonNode.Parse(relatedQueries)["default"]["rankedList"].AsArray();
    }



    public async Task<string> GetTrendingSearches()
    {
        return await GetCookiesAndData(new Uri(trendingSearchesUrl));
    }

    public async Task<string> GetTodaySearches(string geo = "US", string hl = "en-US")
    {
        return await GetCookiesAndData(new Uri($"{todaySearchesUrl}?ns=15&geo={geo}&tz=-180&hl={hl}"), NormalCharsToTrim); //title.query
    }

    public async Task<string> Categories()
    {
        return await GetCookiesAndData(new Uri(categoriesUrl + $"?hl=en-US"), NormalCharsToTrim); //title.query
    }









    //--------------------------------------
    //--------------------------------------
    //--------------------------------------
    public async Task<string> GetCookiesAndData(Uri url, int trimChars = 0)
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



    public async Task<string> GetData(Uri url, CookieContainer cookies = null, bool trim = true)
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
}