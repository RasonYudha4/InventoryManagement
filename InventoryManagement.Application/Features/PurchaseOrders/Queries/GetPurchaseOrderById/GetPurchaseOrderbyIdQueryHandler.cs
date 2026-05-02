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
            .Where(po => po.Id == request.Id)
            .Select(po => new PurchaseOrderDetailDto(
                po.Id,
                po.PONumber,
                po.OrderDate,
                po.Status,
                po.TotalAmount,
                po.Notes,
                po.SupplierId,
                po.Supplier.Name,
                po.OrderLines
                    .OrderBy(line => line.LineNumber)
                    .Select(line => new PurchaseOrderLineDetailDto(
                        line.Id,
                        line.LineNumber,
                        line.ProductId,
                        line.Product.SKU,
                        line.Product.Name,
                        line.OrderedQuantity,
                        line.ReceivedQuantity,
                        line.UnitCost,
                        line.TotalCost,
                        line.ExpectedDeliveryDate
                    )).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (purchaseOrder is null)
            throw new KeyNotFoundException($"Purchase Order with ID {request.Id} was not found.");

        return purchaseOrder;
    }
}