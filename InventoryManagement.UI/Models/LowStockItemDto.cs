namespace InventoryManagement.UI.Models;

public record LowStockItemDto(
    Guid ProductId, 
    string ProductName, 
    int CurrentQuantity, 
    int ReorderPoint
);