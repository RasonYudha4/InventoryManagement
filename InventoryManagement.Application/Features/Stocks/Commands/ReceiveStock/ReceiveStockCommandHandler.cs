using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.Stock.Commands.ReceiveStock;

public class ReceiveStockCommandHandler(IApplicationDbContext context) 
: IRequestHandler<ReceiveStockCommand, Guid>
{
    public async Task<Guid> Handle(ReceiveStockCommand request, CancellationToken cancellationToken)
    {
        var productExists = await context.Products.AnyAsync(p => p.Id == request.ProductId, cancellationToken);
        if (!productExists) throw new Exception($"Product with ID {request.ProductId} not found.");

        var locationExist = await context.Locations.AnyAsync(l => l.Id == request.LocationId, cancellationToken);
        if (!locationExist) throw new Exception($"Location with ID {request.LocationId} not found.");

        var stockLevel = await context.StockLevels
            .FirstOrDefaultAsync(sl => 
                sl.ProductId == request.ProductId &&
                sl.LocationId == request.LocationId,
                cancellationToken);

        int quantityBefore = 0;

        if (stockLevel == null)
        {
            stockLevel = new StockLevel
            {
                ProductId = request.ProductId,
                LocationId = request.LocationId,
                Quantity = 0,
                AllocatedQuantity = 0
            };
            context.StockLevels.Add(stockLevel);
        }
        else
        {
            quantityBefore = stockLevel.Quantity;
        }

        stockLevel.Quantity += request.Quantity;

        var transaction = new StockTransaction
        {
            ProductId = request.ProductId,
            LocationId = request.LocationId,
            Type = TransactionType.Receipt,
            Quantity = request.Quantity,
            QuantityBefore = quantityBefore,
            QuantityAfter = stockLevel.Quantity,
            ReferenceNumber = request.ReferenceNumber,
            Notes = request.Notes,
            PerformedBy = request.PerformedByUserId
        };

        context.StockTransactions.Add(transaction);

        await context.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}