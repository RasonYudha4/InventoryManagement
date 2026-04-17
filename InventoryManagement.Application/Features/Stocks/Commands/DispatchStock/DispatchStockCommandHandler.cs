using InventoryManagement.Application.Features.Stock.Commands.DispatchStock;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class DispatchStockCommandHandler(IApplicationDbContext context)
    : IRequestHandler<DispatchStockCommand, Guid>
{
    public async Task<Guid> Handle(DispatchStockCommand request, CancellationToken cancellationToken)
    {
        var stockLevel = await context.StockLevels
            .FirstOrDefaultAsync(s => s.ProductId == request.ProductId && s.LocationId == request.LocationId, cancellationToken);

        if (stockLevel == null) throw new Exception("Stock not found at this location.");

        if (stockLevel.Quantity < request.Quantity)
            throw new Exception($"Cannot dispatch {request.Quantity}. Only {stockLevel.Quantity} physically available.");

        int qtyBefore = stockLevel.Quantity;

        stockLevel.Quantity -= request.Quantity;

        if (stockLevel.AllocatedQuantity >= request.Quantity)
        {
            stockLevel.AllocatedQuantity -= request.Quantity;
        }

        var transaction = new StockTransaction
        {
            ProductId = request.ProductId,
            LocationId = request.LocationId,
            Quantity = request.Quantity,
            QuantityBefore = qtyBefore,
            QuantityAfter = stockLevel.Quantity,
            Type = TransactionType.Issue,
            ReferenceNumber = request.SalesOrderNumber,
            Notes = "Dispatched to customer."
        };

        context.StockTransactions.Add(transaction);
        await context.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}