namespace InventoryManagement.API.Models.Requests;

public record PurchaseOrderLineItemRequest(
    Guid ProductId,
    int Quantity,
    decimal NegotiatedUnitCost
);

public record CreatePurchaseOrderRequest(
    Guid SupplierId,
    DateTime? ExpectedDeliveryDate,
    string? Notes,
    List<PurchaseOrderLineItemRequest> Items
);