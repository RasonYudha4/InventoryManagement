namespace InventoryManagement.UI.Models;

public record LocationDto(Guid Id, string Code, decimal MaxWeightCapacity);

public record WarehouseDto(
    Guid Id,
    string Code,
    string Name,
    List<LocationDto> Locations
);

public record LocationDetailDto(
    Guid Id,
    string Code,
    string? Aisle,
    string? Rack,
    string? Shelf,
    string? Bin,
    decimal MaxWeightCapacity
);

public record WarehouseDetailDto(
    Guid Id,
    string Code,
    string Name,
    string? Address,
    bool IsActive,
    List<LocationDetailDto> Locations
);