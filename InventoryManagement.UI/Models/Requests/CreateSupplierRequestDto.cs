namespace InventoryManagement.UI.Models.Requests;

public record CreateSupplierRequest(
    string Name,
    string ContactName,
    string Email,
    string? Phone,
    string? Address,
    string? TaxId
);