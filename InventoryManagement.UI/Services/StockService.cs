using System.Net.Http.Json;
using InventoryManagement.UI.Models;
using InventoryManagement.UI.Models.Requests;
using InventoryManagement.UI.Services.Interfaces;

namespace InventoryManagement.UI.Services;

public class StockService(HttpClient http) : IStockService
{
    public async Task<List<LowStockItemDto>> GetLowStockItemsAsync()
    {
        var items = await http.GetFromJsonAsync<List<LowStockItemDto>>("api/stock/low");
        return items ?? [];
    }

    public async Task<List<StockLevelDto>> GetStockByLocationAsync(Guid locationId)
    {
        var items = await http.GetFromJsonAsync<List<StockLevelDto>>($"api/stock/location/{locationId}");
        return items ?? [];
    }

    public async Task<Guid> ReceiveStockAsync(ReceiveStockRequest request)
    {
        var response = await http.PostAsJsonAsync("api/stock/receive", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ApiTransactionResponse>();
        return result!.TransactionId;
    }

    public async Task<Guid> DispatchStockAsync(DispatchStockRequest request)
    {
        var response = await http.PostAsJsonAsync("api/stock/dispatch", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ApiTransactionResponse>();
        return result!.TransactionId;
    }
}