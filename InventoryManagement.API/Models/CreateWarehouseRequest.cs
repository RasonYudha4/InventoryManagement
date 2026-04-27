namespace InventoryManagement.API.Models.Requests;

public record CreateWarehouseRequest(
    string Code,
    string Name,
    string? Address
);