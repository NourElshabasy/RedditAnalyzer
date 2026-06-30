namespace Services;
using System.Net.Http;
using System.Text.Json;
using Models;

internal class HackerNewsService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    internal HackerNewsService(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    internal async Task<Story> GetStoryAsync(int id)
    {
        var json = await _client.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{id}.json");

        return JsonSerializer.Deserialize<Story>(json, _options)
            ?? throw new InvalidOperationException("Story could not be deserialized.");
    }

    internal async Task<List<Story>> GetStoriesByIdsAsync(IEnumerable<int> ids)
    {
        var tasks = ids.Select(GetStoryAsync);
        var stories = await Task.WhenAll(tasks);
        return stories.ToList();
    }
    internal async Task<List<int>> GetTopStoriesIdsAsync()
    {
        var json = await _client.GetStringAsync("https://hacker-news.firebaseio.com/v0/topstories.json");

        return JsonSerializer.Deserialize<List<int>>(json, _options)
            ?? throw new InvalidOperationException("Top stories IDs could not be deserialized.");
    }

    internal async Task<List<Story>> GetTopStoriesAsync(int count)
    {
        var ids = await GetTopStoriesIdsAsync();
        return await GetStoriesByIdsAsync(ids.Take(count));
    }

    
}