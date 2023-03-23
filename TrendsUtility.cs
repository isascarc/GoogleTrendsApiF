using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;

namespace GoogleTrendsApi;

class TrendsUtility
{
    const int CharsToTrim = 5;
    public async Task<TrendsRespond?> getTrendsRespondSolicitud(Query solicitud)
    {
        Uri url = new Uri($"https://trends.google.com/trends/api/explore?req={JsonSerializer.Serialize(solicitud)}&hl=he-IL&tz=300");
        var Cookie = await GetCookie(url);
        var data = await GetData(url, Cookie);
        return data.Length > 0 ? JsonSerializer.Deserialize<TrendsRespond>(data) : new TrendsRespond();
    }

    public async Task<CookieContainer> GetCookie(Uri url)
    {
        var cookieContainer = new CookieContainer();

        using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
        using (var client = new HttpClient(handler))
        {
            HttpRequestMessage request = new(HttpMethod.Post, url.OriginalString);
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Host", url.Host);
            await client.SendAsync(request);
            return cookieContainer;
        }
    }

    public async Task<string> GetData(Uri url, CookieContainer cookies = null)
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
            string clearResult = b.Substring(CharsToTrim);
            return cookies is null ? GetDataFinal(clearResult) : clearResult;
        }
    }

    public static string GetDataFinal(string data)
    {
        using (Stream st = GenerateStreamFromString(data))
        {
            var jsonReceived = JsonObject.Parse(st)["default"]["timelineData"];
            var jsonResult = jsonReceived.AsArray().Select(x => new { date = x["time"], value = x["value"] });
            return JsonSerializer.Serialize(jsonResult);
        }
    }

    public static MemoryStream GenerateStreamFromString(string value)
        => new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
}