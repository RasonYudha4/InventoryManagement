namespace InventoryManagement.API.Models.Requests;

public record SalesOrderLineItemRequest(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    DateTime? ExpectedShipDate
);

public record CreateSalesOrderRequest(
    string CustomerName,
    string? CustomerContact,
    string? Notes,
    List<SalesOrderLineItemRequest> Items
);