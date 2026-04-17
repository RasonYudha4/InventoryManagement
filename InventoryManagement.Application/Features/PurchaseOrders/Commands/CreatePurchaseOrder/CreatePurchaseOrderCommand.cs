using MediatR;

namespace InventoryManagement.Application.Features.PurchaseOrders.Commands.CreatePurchaseOrder;

public record PurchaseOrderLineItemDto(
    Guid ProductId,
    int Quantity,
    decimal NegotiatedUnitCost
);

public record CreatePurchaseOrderCommand(
    Guid SupplierId,
    DateTime? ExpectedDeliveryDate,
    string? Notes,
    List<PurchaseOrderLineItemDto> Items
) : IRequest<Guid>;