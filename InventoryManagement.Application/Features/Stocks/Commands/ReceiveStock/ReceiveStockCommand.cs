using MediatR;

namespace InventoryManagement.Application.Features.Stock.Commands.ReceiveStock;

public record ReceiveStockCommand(
    Guid ProductId,
    Guid LocationId,
    Guid? PurchaseOrderId, 
    int Quantity,
    string? Notes
) : IRequest<Guid>;