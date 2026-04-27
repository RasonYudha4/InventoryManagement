using InventoryManagement.UI.Models;

namespace InventoryManagement.UI.Services.Interfaces;

public interface IStockService
{
    // We will call the API endpoint connected to that CQRS Query you built earlier!
    Task<List<ProductDto>> GetLowStockItemsAsync();
}