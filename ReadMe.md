
# Google trends API 2.2.4
[![NuGet version (GoogleTrendsApi)](https://img.shields.io/nuget/v/GoogleTrendsApi.svg)](https://www.nuget.org/packages/GoogleTrendsApi/)

This is a library for receiving data from trends into the .net environment, easily, and **without dependencies**!


# Example 

## Get trends
You simply call the function like this:
```csharp
Api.FetchData(new[] { "react", "angular" });
```

And also possible with parameters:
```csharp
Api.FetchData(new[] { "react", "angular" }, "US", DateOptions.LastThreeMonths, GroupOptions.youtube)
```


## Trending searches
Get all trending searches:
```csharp
Api.GetAllTrendingSearches()
```

Get trending searches by country (israel, in this example):
```csharp
Api.GetTrendingSearches("israel");
```

## Categories list

For get list of all categories, use:
```csharp
Api.GetCategories()
```