using InventoryManagement.UI.Models;
using InventoryManagement.UI.Models.Requests;

namespace InventoryManagement.UI.Services.Interfaces;

public interface IWarehouseService
{
    Task<Guid> CreateWarehouseAsync(CreateWarehouseRequest request);
    Task<List<WarehouseDto>> GetAllWarehousesAsync();
    Task<WarehouseDetailDto> GetWarehouseByIdAsync(Guid id);
}