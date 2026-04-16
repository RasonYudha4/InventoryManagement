using MediatR;

public record GetWarehousesWithLocationsQuery : IRequest<List<WarehouseDto>>;