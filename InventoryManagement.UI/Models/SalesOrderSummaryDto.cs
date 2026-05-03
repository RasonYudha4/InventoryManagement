namespace InventoryManagement.UI.Models;

public record SalesOrderSummaryDto(
    Guid Id,
    string SONumber,
    DateTime OrderDate,
    string CustomerName,
    string Status,
    decimal TotalAmount,
    int TotalLines,
    string? Notes
);