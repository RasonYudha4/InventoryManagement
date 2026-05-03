using InventoryManagement.UI.Models.Enums;

namespace InventoryManagement.UI.Models;

public record SalesOrderLineDetailDto(
    Guid Id,
    int LineNumber,
    Guid ProductId,
    string SKU,
    string ProductName,
    int OrderedQuantity,
    int DispatchedQuantity,
    decimal UnitPrice,
    decimal TotalPrice,
    DateTime? ExpectedShipDate
);

public record SalesOrderDetailDto(
    Guid Id,
    string SONumber,
    DateTime OrderDate,
    string CustomerName,
    string? CustomerContact,
    SalesOrderStatus Status,
    decimal TotalAmount,
    string? Notes,
    List<SalesOrderLineDetailDto> OrderLines
);