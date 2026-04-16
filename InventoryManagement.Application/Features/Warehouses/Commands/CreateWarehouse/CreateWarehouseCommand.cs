using MediatR;

namespace InventoryManagement.Application.Features.Warehouses.Commands.CreateWarehouse;

public record CreateWarehouseCommand(
    string Code,
    string Name,
    string? Address
) : IRequest<Guid>;