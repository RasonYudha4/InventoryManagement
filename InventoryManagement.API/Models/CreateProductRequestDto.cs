namespace InventoryManagement.API.Models.Requests;

public record CreateProductRequest(
    string SKU,
    string Name,
    string? Description,
    decimal UnitCost,
    decimal SellingPrice,
    int ReorderPoint,
    int ReorderQuantity,
    Guid CategoryId,
    Guid SupplierId
);