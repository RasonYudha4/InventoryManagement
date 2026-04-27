using InventoryManagement.UI.Models.Requests;

namespace InventoryManagement.UI.Services.Interfaces;

public interface ILocationService
{
    Task<Guid> CreateLocationAsync(CreateLocationRequest request);
}