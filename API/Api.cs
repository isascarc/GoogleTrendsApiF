global using System;
global using System.Text.Json;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Net;
global using System.Net.Http;
using System.Text.Json.Nodes;
using System.ComponentModel;

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
    public static async Task<JsonNode> FetchData(string[] keyword, string geo = "", DateOptions time = DateOptions.LastHour,
        GroupOptions group = GroupOptions.All, int category = 0)
    {
        var RespondSolicitud = await new TrendsUtility().GetTrendsRespondSolicitud(new Query(geo, time.GetDescription(), keyword, category, group.GetDescription()));
        var ret = new TrendsGetData(RespondSolicitud);
        return JsonNode.Parse(await ret.getTrendsJsonResponseAsync());
    }

    public async static Task<JsonObject> GetAllTrendingSearches()
    {
        var gg = await new TrendsUtility().GetTrendingSearches();
        return JsonNode.Parse(gg)?.AsObject();
    }

    public async static Task<JsonArray> GetTrendingSearches(string searchVal = "united_states")
    {
        var result = await GetAllTrendingSearches();
        return result.ContainsKey(searchVal) ? result[searchVal].AsArray() : new JsonArray();
    }

    /// <summary>
    /// Request data from Google Daily Trends section.
    /// </summary>
    /// <returns></returns>                         
    public static async Task<JsonArray> GetTodaySearches(string geo = "US")
    {
        var gg = await (new TrendsUtility().GetTodaySearches(geo));
        return JsonNode.Parse(gg)?["default"]["trendingSearchesDays"][0]["trendingSearches"].AsArray();
    }

    public static async Task<JsonArray> GetRelatedQueries(string[] keyword, string geo = "", DateOptions time = DateOptions.LastThreeMonths,
        GroupOptions group = GroupOptions.All, int category = 0)
    {
        return await new TrendsUtility()
            .GetRelated(new Query(geo, time.GetDescription(), keyword, category, group.GetDescription()));
    }

    public static async Task<JsonArray> GetRelatedTopics(string[] keyword, string geo = "", DateOptions time = DateOptions.LastThreeMonths,
    GroupOptions group = GroupOptions.All, int category = 0)
    {
        return await new TrendsUtility()
            .GetRelated(new Query(geo, time.GetDescription(), keyword, category, group.GetDescription()), 0);
    }

    /// <summary>
    /// Request available categories data from Google's API and return a dictionary
    /// </summary>
    /// <returns></returns>
    public static async Task<string> GetCategories()
    {
        return await new TrendsUtility().Categories();
    }

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