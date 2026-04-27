using System.Net.Http.Json;
using InventoryManagement.UI.Models;
using InventoryManagement.UI.Models.Requests;
using InventoryManagement.UI.Services.Interfaces;

namespace InventoryManagement.UI.Services;

public class LocationService(HttpClient http) : ILocationService
{
    public async Task<Guid> CreateLocationAsync(CreateLocationRequest request)
    {
        var response = await http.PostAsJsonAsync("api/location", request);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<ApiIdResponse>();
        return result!.Id;
    }
}