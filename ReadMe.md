
# Google trends API 2.2.3
[![NuGet version (GoogleTrendsApi)](https://img.shields.io/nuget/v/GoogleTrendsApi.svg?style=flat-square)](https://www.nuget.org/packages/GoogleTrendsApi/)

This is a library for receiving data from trends into the .net environment, easily, and **without dependencies**!


# Example 

## Get trends
You simply call the function like this:
```csharp
await Api.FetchDataAsync(new[] { "angular", "react" });
```

or, if we want to do it synchronously:
```csharp
await Api.FetchData(new[] { "angular", "react" });
```

## Trending searches

Get all trending searches:
```csharp
Api.GetAllTrendingSearches()
```

Get trending searches by country(israel, in this example):
```csharp
Api.GetTrendingSearches("israel");
```