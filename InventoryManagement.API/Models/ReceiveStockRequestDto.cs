namespace InventoryManagement.API.Models;

public record ReceiveStockRequest(
    Guid ProductId,
    Guid LocationId,
    Guid PurchaseOrderId,
    int Quantity,
    string? Notes
);

public record DispatchStockRequest(
    Guid ProductId,
    Guid LocationId,
    int Quantity,
    string? SalesOrderNumber,
    string? Notes
);