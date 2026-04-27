using MediatR;

namespace InventoryManagement.Application.Features.Warehouses.Commands;

public record CreateWarehouseCommand(
    string Code,
    string Name,
    string? Address
) : IRequest<Guid>;