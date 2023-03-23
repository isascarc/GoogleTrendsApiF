global using System;
global using System.Text.Json;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Net;
global using System.Net.Http;


namespace GoogleTrendsApi;

public static class Api
{
    public const string LastHour = "now 1-H";
    public const string LastDay = "now 1-d";
    public const string LastWeek = "now 7-d";
    public const string LastMonth = "today 1-m";
    public const string LastFiveYears = "today 5-y";

    public static async Task<string> FetchDataAsStringAsync(string[] keyword, string geo = "", string time = LastFiveYears)
    {
        var RespondSolicitud = await new TrendsUtility().getTrendsRespondSolicitud(new Query(geo, time, keyword));
        var ret = new TrendsGetData(RespondSolicitud);
        return await ret.getTrendsJsonResponseAsync();
    }

    public static string FetchDataAsString(string[] keyword, string geo = "", string time = LastFiveYears)
        => FetchDataAsStringAsync(keyword, geo, time).Result;
}