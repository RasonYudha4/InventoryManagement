using MediatR;

namespace InventoryManagement.Application.Features.PurchaseOrders.Commands.CreatePurchaseOrder;

public record PurchaseOrderLineItemDto(
    Guid ProductId,
    int Quantity,
    decimal NegotiatedUnitCost,
    DateTime? ExpectedDeliveryDate
);

public record CreatePurchaseOrderCommand(
    Guid SupplierId,
    string? Notes,
    List<PurchaseOrderLineItemDto> Items
) : IRequest<Guid>;