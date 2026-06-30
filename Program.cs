using System;
using System.Net.Http;
using System.Text.Json;
using Models;
using Services;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Welcome to the Hacker News Top Stories Viewer!");
        HackerNewsService hackerNewsService = new HackerNewsService(new HttpClient());
        var topStories = await hackerNewsService.GetTopStoriesAsync(10);
        foreach (var story in topStories.OrderByDescending(s => s.Score))
        {
            Console.WriteLine(story.Title);
            Console.WriteLine($"By: {story.By}, Score: {story.Score}, Id: {story.Id}");
        }
    }
}