using MediatR;

namespace InventoryManagement.Application.Features.Stock.Commands.ReceiveStock;

public record ReceiveStockCommand(
    Guid ProductId,
    Guid LocationId,
    int Quantity, 
    string? ReferenceNumber,
    string? Notes,
    string PerformedByUserId
) : IRequest<Guid>;