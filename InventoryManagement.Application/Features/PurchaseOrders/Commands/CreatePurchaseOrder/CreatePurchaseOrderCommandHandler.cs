using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.PurchaseOrders.Commands.CreatePurchaseOrder;

public class CreatePurchaseOrderCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreatePurchaseOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        var supplierExists = await context.Suppliers.AnyAsync(s => s.Id == request.SupplierId, cancellationToken);
        if (!supplierExists) throw new Exception("Supplier not found.");

        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var validProductCount = await context.Products.CountAsync(p => productIds.Contains(p.Id), cancellationToken);

        if (validProductCount != productIds.Count)
            throw new Exception("One or more products in the order do not exist.");

        var poNumber = $"PO-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}";

        decimal totalAmount = 0;
        var orderLines = new List<PurchaseOrderLine>();
        int lineNumber = 1;

        foreach (var item in request.Items)
        {
            totalAmount += item.Quantity * item.NegotiatedUnitCost;

            orderLines.Add(new PurchaseOrderLine
            {
                ProductId = item.ProductId,
                LineNumber = lineNumber++,
                OrderedQuantity = item.Quantity,
                ReceivedQuantity = 0,
                UnitCost = item.NegotiatedUnitCost
            });
        }

        var purchaseOrder = new PurchaseOrder
        {
            SupplierId = request.SupplierId,
            PONumber = poNumber,
            OrderDate = DateTime.UtcNow,
            ExpectedDeliveryDate = request.ExpectedDeliveryDate,
            Notes = request.Notes,
            Status = PurchaseOrderStatus.Draft,
            TotalAmount = totalAmount,
            OrderLines = orderLines
        };

        context.PurchaseOrders.Add(purchaseOrder);
        await context.SaveChangesAsync(cancellationToken);

        return purchaseOrder.Id;
    }
}