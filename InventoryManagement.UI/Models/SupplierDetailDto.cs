namespace InventoryManagement.UI.Models;

public record SupplierDetailDto(
    Guid Id,
    string Name,
    string ContactName,
    string Email,
    string? Phone,
    string? Address,
    string? TaxId,
    bool IsActive
);