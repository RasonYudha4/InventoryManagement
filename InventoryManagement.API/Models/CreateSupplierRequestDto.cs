namespace InventoryManagement.API.Models.Requests;

public record CreateSupplierRequest(
    string Name,
    string ContactName,
    string Email,
    string? Phone,
    string? Address,
    string? TaxId
);