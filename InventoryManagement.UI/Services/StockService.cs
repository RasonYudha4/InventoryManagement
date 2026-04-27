using System.Net.Http.Json;
using InventoryManagement.UI.Models;
using InventoryManagement.UI.Services.Interfaces;

namespace InventoryManagement.UI.Services;

public class StockService : IStockService
{
    private readonly HttpClient _http;

    public StockService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ProductDto>> GetLowStockItemsAsync()
    {
        try
        {
            // Make sure this matches the exact route of your API controller!
            // Assuming it's something like GET /api/stock/low
            var items = await _http.GetFromJsonAsync<List<ProductDto>>("api/stock/low");
            return items ?? new List<ProductDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching stock: {ex.Message}");
            return new List<ProductDto>();
        }
    }
}