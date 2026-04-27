namespace InventoryManagement.UI.Models;

public record ProductDetailDto(
    Guid Id,
    string SKU,
    string Name,
    string? Description,
    decimal UnitCost,
    decimal SellingPrice,
    int ReorderPoint,
    int ReorderQuantity,
    string CategoryName,
    string SupplierName
);