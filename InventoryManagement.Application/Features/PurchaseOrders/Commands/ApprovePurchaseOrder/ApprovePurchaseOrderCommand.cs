using MediatR;

public record ApprovePurchaseOrderCommand(Guid PurchaseOrderId) : IRequest<bool>;