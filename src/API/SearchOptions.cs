using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleTrendsApi;


/// <summary>
/// These are all the options offered by Google trends.
/// </summary>
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

public enum Category
{
    [Description("all")]
    All,
    [Description("e")]
    Entertainment,
    [Description("b")]
    Business,
    [Description("t")]
    ScienceAndTech,
    [Description("m")]
    Health,
    [Description("s")]
    Sports,
    [Description("h")]
    TopStories,
}
