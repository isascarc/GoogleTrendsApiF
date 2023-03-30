global using System;
global using System.Text.Json;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Net;
global using System.Net.Http;
//using System.Threading;
//using System.Timers;
//using System.IO;7
using System.Text.Json.Nodes;

namespace GoogleTrendsApi;

public static class Api
{
    #region DateOptions

    // These are all the options offered by Google trends.
    public const string LastHour = "now 1-H";
    public const string LastFourHours = "now 4-H";

    public const string LastDay = "now 1-d";
    public const string LastWeek = "now 7-d";

    public const string LastMonth = "today 1-m";
    public const string LastThreeMonths = "today 3-m";

    // This is the way to mark a year from today
    public const string LastYear = "";
    public const string LastFiveYears = "today 5-y";
    public const string FromStart = "all";

    #endregion

    //public static async Task<string> FetchDataAsStringAsync(string[] keyword, string geo = "", string time = LastThreeMonths)
    //{
    //    var RespondSolicitud = await new TrendsUtility().GetTrendsRespondSolicitud(new Query(geo, time, keyword));
    //    var ret = new TrendsGetData(RespondSolicitud);
    //    return await ret.getTrendsJsonResponseAsync();
    //}

    //private static string FetchDataAsString(string[] keyword, string geo = "", string time = LastFiveYears)
    //    => FetchDataAsStringAsync(keyword, geo, time).Result;

    //
    public static async Task<JsonNode> FetchDataAsync(string[] keyword, string geo = "", string time = LastThreeMonths)
    {
        var RespondSolicitud = await new TrendsUtility().GetTrendsRespondSolicitud(new Query(geo, time, keyword));
        var ret = new TrendsGetData(RespondSolicitud);
        return JsonNode.Parse(await ret.getTrendsJsonResponseAsync());
    }

    // 
    public static JsonNode FetchData(string[] keyword, string geo = "", string time = LastFiveYears)
         => FetchDataAsync(keyword, geo, time).Result;

    public static JsonObject GetAllTrendingSearches()
    {
        var gg = new TrendsUtility().GetTrendingSearches();
        var result = JsonNode.Parse("{\"Uni" + gg)?.AsObject();
        return result;
    }

    public static JsonArray GetTrendingSearches(string searchVal = "United_states")
    {
        var gg = new TrendsUtility().GetTrendingSearches();
        var result = JsonNode.Parse("{\"Uni" + gg)?.AsObject();
        var res = result.ContainsKey(searchVal) ? result[searchVal].AsArray() : new JsonArray();
        return res;
    }
}