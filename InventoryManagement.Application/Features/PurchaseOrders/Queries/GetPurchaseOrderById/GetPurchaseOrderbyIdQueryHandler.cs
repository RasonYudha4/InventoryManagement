using InventoryManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.PurchaseOrders.Queries.GetPurchaseOrderById;

public class GetPurchaseOrderByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetPurchaseOrderByIdQuery, PurchaseOrderDetailDto>
{
    public async Task<PurchaseOrderDetailDto> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var purchaseOrder = await context.PurchaseOrders
            .Include(po => po.Supplier)
            .Include(po => po.OrderLines)
                .ThenInclude(line => line.Product)
            .Where(po => po.Id == request.Id)
            .Select(po => new PurchaseOrderDetailDto(
                po.Id,
                po.PONumber,
                po.OrderDate,
                po.ExpectedDeliveryDate,
                po.Status,
                po.TotalAmount,
                po.Notes,
                po.SupplierId,
                po.Supplier.Name,
                po.OrderLines.Select(line => new PurchaseOrderLineDetailDto(
                    line.Id,
                    line.LineNumber,
                    line.ProductId,
                    line.Product.SKU,
                    line.Product.Name,
                    line.OrderedQuantity,
                    line.ReceivedQuantity,
                    line.UnitCost,
                    line.TotalCost
                )).OrderBy(line => line.LineNumber).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (purchaseOrder is null)
            throw new KeyNotFoundException($"Purchase Order with ID {request.Id} was not found.");

        return purchaseOrder;
    }
}