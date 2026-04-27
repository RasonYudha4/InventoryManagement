using MediatR;

namespace InventoryManagement.Application.Features.Warehouses.Queries.GetWarehouseById;

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

public record GetWarehouseByIdQuery(Guid Id) : IRequest<WarehouseDetailDto>;