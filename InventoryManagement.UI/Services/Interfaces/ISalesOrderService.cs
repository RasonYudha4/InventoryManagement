using InventoryManagement.UI.Models;
using InventoryManagement.UI.Models.Requests;

namespace InventoryManagement.UI.Services.Interfaces;

public interface ISalesOrderService
{
    Task<Guid> CreateDraftAsync(CreateSalesOrderRequest request);
    Task ConfirmOrderAsync(Guid id);
    Task<SalesOrderDetailDto> GetSalesOrderByIdAsync(Guid id);
    Task<List<SalesOrderSummaryDto>> GetAllSalesOrdersAsync();
}