using System.Net.Http.Json;
using InventoryManagement.UI.Models;
using InventoryManagement.UI.Models.Requests;
using InventoryManagement.UI.Services.Interfaces;

namespace InventoryManagement.UI.Services;

public class WarehouseService(HttpClient http) : IWarehouseService
{
    public async Task<Guid> CreateWarehouseAsync(CreateWarehouseRequest request)
    {
        var response = await http.PostAsJsonAsync("api/warehouse", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ApiIdResponse>();
        return result!.Id;
    }

    public async Task<List<WarehouseDto>> GetAllWarehousesAsync()
    {
        var warehouses = await http.GetFromJsonAsync<List<WarehouseDto>>("api/warehouse");
        return warehouses ?? [];
    }

    public async Task<WarehouseDetailDto> GetWarehouseByIdAsync(Guid id)
    {
        var result = await http.GetFromJsonAsync<WarehouseDetailDto>($"api/warehouse/{id}");
        return result!;
    }
}