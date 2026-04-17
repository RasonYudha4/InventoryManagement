using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.Stock.Commands.ReceiveStock;

public class ReceiveStockCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService) 
: IRequestHandler<ReceiveStockCommand, Guid>
{
    public async Task<Guid> Handle(ReceiveStockCommand request, CancellationToken cancellationToken)
    {
        var po = await context.PurchaseOrders
            .Include(p => p.OrderLines)
            .FirstOrDefaultAsync(p => p.Id == request.PurchaseOrderId, cancellationToken);

        if (po == null)
            throw new Exception("Purchase Order not found.");
        
        if (po.Status != PurchaseOrderStatus.Approved && po.Status != PurchaseOrderStatus.PartiallyReceived)
            throw new Exception($"Cannot receive against this PO. Current status is {po.Status}.");

        var poLine = po.OrderLines.FirstOrDefault(l => l.ProductId == request.ProductId);
        if (poLine == null) 
            throw new Exception("This product is not listed on this Purchase Order.");

        var remainingToReceive = poLine.OrderedQuantity - poLine.ReceivedQuantity;
        if (request.Quantity > remainingToReceive)
            throw new Exception($"Over-receipt error! You ordered {poLine.OrderedQuantity}, already received {poLine.ReceivedQuantity}. You cannot receive {request.Quantity} more.");

        var location = await context.Locations.FindAsync(new object[] { request.LocationId }, cancellationToken);
        if (location == null) throw new Exception("Location not found.");

        var stockLevel = await context.StockLevels
            .FirstOrDefaultAsync(sl => 
                sl.ProductId == request.ProductId &&
                sl.LocationId == request.LocationId,
                cancellationToken);

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

        int qtyBefore = stockLevel.Quantity;

        stockLevel.Quantity += request.Quantity;
        stockLevel.UpdatedAt = DateTime.UtcNow;

        var transaction = new StockTransaction
        {
            ProductId = request.ProductId,
            LocationId = request.LocationId,
            Type = TransactionType.Receipt,
            Quantity = request.Quantity,
            QuantityAfter = stockLevel.Quantity,
            QuantityBefore = qtyBefore,
            ReferenceNumber = po.PONumber,
            Notes = request.Notes,
            PerformedBy = currentUserService.Email ?? "System"
        };

        context.StockTransactions.Add(transaction);

        poLine.ReceivedQuantity += request.Quantity;

        bool isFullyReceived = po.OrderLines.All(l => l.ReceivedQuantity >= l.OrderedQuantity);

        po.Status = isFullyReceived ? PurchaseOrderStatus.Received : PurchaseOrderStatus.PartiallyReceived;

        await context.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}