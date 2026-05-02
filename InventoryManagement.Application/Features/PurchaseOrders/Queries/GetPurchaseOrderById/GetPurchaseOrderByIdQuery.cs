using InventoryManagement.Domain.Enums;
using MediatR;

namespace InventoryManagement.Application.Features.PurchaseOrders.Queries.GetPurchaseOrderById;

public record PurchaseOrderLineDetailDto(
    Guid Id,
    int LineNumber,
    Guid ProductId,
    string SKU,
    string ProductName,
    int OrderedQuantity,
    int ReceivedQuantity,
    decimal UnitCost,
    decimal TotalCost,
    DateTime? ExpectedDeliveryDate
);

public record PurchaseOrderDetailDto(
    Guid Id,
    string PONumber,
    DateTime OrderDate,
    PurchaseOrderStatus Status,
    decimal TotalAmount,
    string? Notes,
    Guid SupplierId,
    string SupplierName,
    List<PurchaseOrderLineDetailDto> OrderLines
);

public record GetPurchaseOrderByIdQuery(Guid Id) : IRequest<PurchaseOrderDetailDto>;