using System.Net.Http.Json;
using InventoryManagement.UI.Models;

namespace InventoryManagement.UI.Services;

public class AiService : IAiService
{
    private readonly HttpClient _http;

    public AiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> AskCopilotAsync(string question)
    {
        try
        {
            var request = new AiRequestDto { Question = question };
            
            // POST to your backend API
            var response = await _http.PostAsJsonAsync("api/Ai/ask", request);
            
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AiResponseDto>();
                return result?.Answer ?? "I received an empty response.";
            }
            
            return $"Error: The server returned {response.StatusCode}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"AI Service Error: {ex.Message}");
            return "Sorry, I couldn't connect to the AI brain right now. Make sure Ollama is running!";
        }
    }
}