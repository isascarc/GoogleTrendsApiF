# Google trends API 2.2
[![NuGet version (GoogleTrendsApi)](https://img.shields.io/nuget/v/GoogleTrendsApi.svg?style=flat-square)](https://www.nuget.org/packages/GoogleTrendsApi/)

This is a library for receiving data from trends into the .net environment, easily, and **without dependencies**!


# Example
You simply call the function like this:
```csharp
await Api.FetchDataAsStringAsync(new[] { "angular", "react" }, time: Api.LastMonth);
```

or, if we want to do it synchronously:
```csharp
await Api.FetchDataAsString(new[] { "angular", "react" }, time: Api.LastMonth);
```