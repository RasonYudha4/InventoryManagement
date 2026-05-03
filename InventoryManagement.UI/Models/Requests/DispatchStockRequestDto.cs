namespace InventoryManagement.UI.Models.Requests;

public record DispatchStockRequest(
    Guid ProductId,
    Guid LocationId,
    Guid SalesOrderId,
    int Quantity,
    string? Notes
);