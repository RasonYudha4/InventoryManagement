using InventoryManagement.UI.Models;
using InventoryManagement.UI.Models.Requests;

namespace InventoryManagement.UI.Services.Interfaces;

public interface IStockService
{
    Task<List<LowStockItemDto>> GetLowStockItemsAsync();
    Task<List<StockLevelDto>> GetStockByLocationAsync(Guid locationId);
    Task<Guid> ReceiveStockAsync(ReceiveStockRequest request);
    Task<Guid> DispatchStockAsync(DispatchStockRequest request);
}