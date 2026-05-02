using InventoryManagement.UI.Models;
using InventoryManagement.UI.Models.Requests;

namespace InventoryManagement.UI.Services.Interfaces;

public interface IPurchaseOrderService
{
    Task<Guid> CreateDraftAsync(CreatePurchaseOrderRequest request);
    Task ApproveOrderAsync(Guid id);
    Task<PurchaseOrderDetailDto> GetPurchaseOrderByIdAsync(Guid id);
    Task<List<PurchaseOrderSummaryDto>> GetAllPurchaseOrdersAsync();
}