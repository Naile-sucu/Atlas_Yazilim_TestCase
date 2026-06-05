using AgentAI.Configuration;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace AgentAI.Services;

public class ChatService:IChatService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public ChatService(HttpClient httpClient, IOptions<AgentConfiguration> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey;
    }

    public async Task<string> AskAsync(string prompt)
    {
        var url =
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-pro:generateContent?key={_apiKey}";

        var requestBody = new
        {
            contents = new[]
            {
            new
            {
                parts = new[]
                {
                    new { text = prompt }
                }
            }
        }
        };

        var response = await _httpClient.PostAsync(
            url,
            new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"));

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);

        return doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString()??string.Empty;
    }
}
