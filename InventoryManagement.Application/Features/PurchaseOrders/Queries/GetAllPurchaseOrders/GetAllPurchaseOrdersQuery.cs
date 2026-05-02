using MediatR;

namespace InventoryManagement.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;

public record PurchaseOrderSummaryDto(
    Guid Id,
    string PONumber,
    DateTime OrderDate,
    string Status,
    decimal TotalAmount,
    Guid SupplierId,
    string SupplierName,
    int TotalLines,
    string? Notes
);

public record GetAllPurchaseOrdersQuery : IRequest<List<PurchaseOrderSummaryDto>>;