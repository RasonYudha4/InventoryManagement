using InventoryManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.PurchaseOrders.Queries.GetAllPurchaseOrders;

public class GetAllPurchaseOrdersQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetAllPurchaseOrdersQuery, List<PurchaseOrderSummaryDto>>
{
    public async Task<List<PurchaseOrderSummaryDto>> Handle(
        GetAllPurchaseOrdersQuery request,
        CancellationToken cancellationToken)
    {
        return await context.PurchaseOrders
            .OrderByDescending(po => po.OrderDate)
                .ThenBy(po => po.PONumber)
            .Select(po => new PurchaseOrderSummaryDto(
                po.Id,
                po.PONumber,
                po.OrderDate,
                po.Status.ToString(),
                po.TotalAmount,
                po.SupplierId,
                po.Supplier.Name,
                po.OrderLines.Count,
                po.Notes
            ))
            .ToListAsync(cancellationToken);
    }
}