using System.Net.Http.Json;
using InventoryManagement.UI.Models;
using InventoryManagement.UI.Models.Requests;
using InventoryManagement.UI.Services.Interfaces;

namespace InventoryManagement.UI.Services;

public class PurchaseOrderService(HttpClient http) : IPurchaseOrderService
{
    public async Task<Guid> CreateDraftAsync(CreatePurchaseOrderRequest request)
    {
        var response = await http.PostAsJsonAsync("api/purchaseorder", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ApiIdResponse>();
        return result!.Id;
    }

    public async Task ApproveOrderAsync(Guid id)
    {
        var response = await http.PutAsync($"api/purchaseorder/{id}/approve", null);
        response.EnsureSuccessStatusCode();
    }

    public async Task<PurchaseOrderDetailDto> GetPurchaseOrderByIdAsync(Guid id)
    {
        var result = await http.GetFromJsonAsync<PurchaseOrderDetailDto>($"api/purchaseorder/{id}");
        return result!;
    }

    public async Task<List<PurchaseOrderSummaryDto>> GetAllPurchaseOrdersAsync()
    {
        var result = await http.GetFromJsonAsync<List<PurchaseOrderSummaryDto>>("api/purchaseorder");
        return result!;
    }
}