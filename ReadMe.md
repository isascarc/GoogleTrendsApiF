# Google trends API 2.1

This is a library for receiving data from trends into the .net environment, easily, and **without dependencies**!


# Example
After installation, you simply call the function like this:
```
await GoogleTrendsApi.Api.FetchDataAsStringAsync(new[] { "angular", "react" }, time: GoogleTrendsApi.Api.LastMonth);
```