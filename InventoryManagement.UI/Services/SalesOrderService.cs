using System.Net.Http.Json;
using InventoryManagement.UI.Helpers;
using InventoryManagement.UI.Models;
using InventoryManagement.UI.Models.Requests;
using InventoryManagement.UI.Services.Interfaces;

namespace InventoryManagement.UI.Services;

public class SalesOrderService(HttpClient http) : ISalesOrderService
{
    public async Task<List<SalesOrderSummaryDto>> GetAllSalesOrdersAsync()
    {
        var result = await http.GetFromJsonAsync<List<SalesOrderSummaryDto>>("api/salesorder");
        return result!;
    }

    public async Task<Guid> CreateDraftAsync(CreateSalesOrderRequest request)
    {
        var response = await http.PostAsJsonAsync("api/salesorder", request);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ApiIdResponse>();
        return result!.Id;
    }

    public async Task ConfirmOrderAsync(Guid id)
    {
        var response = await http.PutAsync($"api/salesorder/{id}/confirm", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<SalesOrderDetailDto> GetSalesOrderByIdAsync(Guid id)
    {
        var result = await http.GetFromJsonAsync<SalesOrderDetailDto>(
            $"api/salesorder/{id}", 
            JsonOptions.Default);
        return result!;
    }
}