using MediatR;

namespace InventoryManagement.Application.Features.Stock.Commands.DispatchStock;

public record DispatchStockCommand(
    Guid ProductId,
    Guid LocationId,
    Guid SalesOrderId,
    int Quantity,
    string? Notes
) : IRequest<Guid>;