namespace InventoryManagement.UI.Models.Requests;

public record PurchaseOrderLineItemRequest(
    Guid ProductId,
    int Quantity,
    decimal NegotiatedUnitCost,
    DateTime? ExpectedDeliveryDate
);

public record CreatePurchaseOrderRequest(
    Guid SupplierId,
    string? Notes,
    List<PurchaseOrderLineItemRequest> Items
);