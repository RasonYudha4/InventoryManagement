using MediatR;

namespace InventoryManagement.Application.Features.Stock.Commands.DispatchStock;

public record DispatchStockCommand(
    Guid ProductId,
    Guid LocationId,
    int Quantity,
    string? SalesOrderNumber,
    string? Notes
) : IRequest<Guid>;