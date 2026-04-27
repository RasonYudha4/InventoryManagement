
using InventoryManagement.UI.Models.Enums;

namespace InventoryManagement.UI.Models;

public record PurchaseOrderLineDetailDto(
    Guid Id,
    int LineNumber,
    Guid ProductId,
    string SKU,
    string ProductName,
    int OrderedQuantity,
    int ReceivedQuantity,
    decimal UnitCost,
    decimal TotalCost
);

public record PurchaseOrderDetailDto(
    Guid Id,
    string PONumber,
    DateTime OrderDate,
    DateTime? ExpectedDeliveryDate,
    PurchaseOrderStatus Status,
    decimal TotalAmount,
    string? Notes,
    Guid SupplierId,
    string SupplierName,
    List<PurchaseOrderLineDetailDto> OrderLines
);