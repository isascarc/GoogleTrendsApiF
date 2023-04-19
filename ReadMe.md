
# Google trends API 2.3.1
[![NuGet version (GoogleTrendsApi)](https://img.shields.io/nuget/v/GoogleTrendsApi.svg)](https://www.nuget.org/packages/GoogleTrendsApi/)
[![NuGet version (GoogleTrendsApi)](https://img.shields.io/nuget/dt/GoogleTrendsApi.svg)](https://www.nuget.org/packages/GoogleTrendsApi/)

This is a library for receiving data from trends into the .net environment, easily, and **without dependencies**!


# Example 

## Get trends
You simply call the function like this:
```csharp
Api.GetInterestOverTime(new[] { "react", "angular" });
```

And also possible with parameters:
```csharp
Api.FetchData(new[] { "react", "angular" }, "US", DateOptions.LastThreeMonths, GroupOptions.youtube, 14);
```

## Get trends by region
Get all trends, divided by regions:
```csharp
Api.GetInterestByRegion(new string[] { "Vue" }, DateOptions.LastThreeMonths, "CITY");
```
You can set the resolution as one of the following options:
"DMA", "CITY", "REGION", and "COUNTRY".



## Trending searches
Get all trending searches:
```csharp
Api.GetAllTrendingSearches();
```

Get trending searches by country (israel, in this example):
```csharp
Api.GetTrendingSearches("israel");
```

## Today searches
Get all Today searches, by country:
```csharp
Api.GetTodaySearches("US");
```


## Related queries
Get all Related queries.
You can enter a query, filter by category, time, country, and group for search ('images', 'news', etc.). For example:
```csharp
Api.GetRelatedQueries(new string[] { "angular" }, "US", DateOptions.LastThreeMonths, GroupOptions.youtube ,3);
```

Also, you can also not enter any query, and just search according to the other categories:
```csharp
 Api.GetRelatedQueries(new string[] { "" }, "US", DateOptions.LastThreeMonths);
```


## Related topics
Get all related topics of query.
You can enter a query, filter by parameters. For example:
```csharp
Api.GetRelatedTopics(new string[] { "angular" }, "US", DateOptions.LastThreeMonths, GroupOptions.youtube ,14);
```


## Categories list

For get list of all categories, use:
```csharp
Api.GetCategories();
```

## Suggestions list

For get list of all suggestions, use:
```csharp
Api.GetSuggestions("angula");
```

