namespace InventoryManagement.UI.Models;

public record PurchaseOrderSummaryDto(
    Guid Id,
    string PONumber,
    DateTime OrderDate,
    DateTime? ExpectedDeliveryDate,
    string Status,
    decimal TotalAmount,
    Guid SupplierId,
    string SupplierName,
    int TotalLines,
    string? Notes
);