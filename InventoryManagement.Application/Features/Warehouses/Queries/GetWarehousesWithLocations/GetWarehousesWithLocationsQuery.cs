using MediatR;

namespace InventoryManagement.Application.Features.Stock.Queries;

public record LocationDto(Guid Id, string Code, decimal MaxWeightCapacity);

public record WarehouseDto(
    Guid Id,
    string Code, 
    string Name, 
    List<LocationDto> Locations
);

public record GetWarehousesWithLocationsQuery : IRequest<List<WarehouseDto>>;