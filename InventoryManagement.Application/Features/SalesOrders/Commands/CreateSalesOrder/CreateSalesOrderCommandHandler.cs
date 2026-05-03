using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.SalesOrders.Commands.CreateSalesOrder;

public class CreateSalesOrderCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateSalesOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var validProductCount = await context.Products
            .CountAsync(p => productIds.Contains(p.Id) && !p.IsDeleted, cancellationToken);

        if (validProductCount != productIds.Count)
            throw new Exception("One or more products in the order do not exist.");

        var soNumber = $"SO-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..4].ToUpper()}";

        decimal totalAmount = 0;
        var orderLines = new List<SalesOrderLine>();
        int lineNumber = 1;

        foreach (var item in request.Items)
        {
            totalAmount += item.Quantity * item.UnitPrice;

            orderLines.Add(new SalesOrderLine
            {
                ProductId = item.ProductId,
                LineNumber = lineNumber++,
                OrderedQuantity = item.Quantity,
                DispatchedQuantity = 0,
                UnitPrice = item.UnitPrice,
                ExpectedShipDate = item.ExpectedShipDate
            });
        }

        var salesOrder = new SalesOrder
        {
            SONumber = soNumber,
            OrderDate = DateTime.UtcNow,
            CustomerName = request.CustomerName,
            CustomerContact = request.CustomerContact,
            Notes = request.Notes,
            Status = SalesOrderStatus.Draft,
            TotalAmount = totalAmount,
            OrderLines = orderLines
        };

        context.SalesOrders.Add(salesOrder);
        await context.SaveChangesAsync(cancellationToken);

        return salesOrder.Id;
    }
}