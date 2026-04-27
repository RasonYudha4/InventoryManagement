namespace InventoryManagement.UI.Models.Requests;

public record DispatchStockRequest(
    Guid ProductId,
    Guid LocationId,
    int Quantity,
    string? SalesOrderNumber,
    string? Notes
);