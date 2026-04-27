namespace InventoryManagement.UI.Models.Requests;

public record ReceiveStockRequest(
    Guid ProductId,
    Guid LocationId,
    Guid? PurchaseOrderId,
    int Quantity,
    string? Notes
);