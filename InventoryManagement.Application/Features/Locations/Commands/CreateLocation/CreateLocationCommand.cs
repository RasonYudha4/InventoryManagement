using MediatR;

namespace InventoryManagement.Application.Features.Locations.Commands.CreateLocation;

public record CreateLocationCommand(
    Guid WarehouseId,
    string Code, 
    string? Aisle,
    string? Rack,
    string? Shelf,
    string? Bin,
    decimal MaxWeightCapacity
) : IRequest<Guid>;