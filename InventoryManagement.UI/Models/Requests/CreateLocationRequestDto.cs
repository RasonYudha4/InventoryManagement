namespace InventoryManagement.UI.Models.Requests;

public record CreateLocationRequest(
    Guid WarehouseId,
    string Code,
    string? Aisle,
    string? Rack,
    string? Shelf,
    string? Bin,
    decimal MaxWeightCapacity
);