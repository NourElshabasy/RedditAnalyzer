using System;
using System.Net.Http;
class Program
{
    static async Task Main()
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add(
                "User-Agent",
                "RedditAnalyzer/1.0"
            );

            var response = await client.GetAsync("https://www.reddit.com/r/programming.json");
            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine(json);
        }
    }
}