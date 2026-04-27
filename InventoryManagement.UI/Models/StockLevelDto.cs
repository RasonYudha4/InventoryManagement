namespace InventoryManagement.UI.Models;

public record StockLevelDto(
    Guid ProductId,
    string SKU,
    string ProductName,
    int TotalQuantity,
    int AllocatedQuantity,
    int AvailableQuantity
);