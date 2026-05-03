using InventoryManagement.Application.Features.Stock.Commands.DispatchStock;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.Stock.Commands.DispatchStock;

public class DispatchStockCommandHandler(
    IApplicationDbContext context,
    ICurrentUserService currentUserService)
    : IRequestHandler<DispatchStockCommand, Guid>
{
    public async Task<Guid> Handle(DispatchStockCommand request, CancellationToken cancellationToken)
    {
        // Validate SO exists and is in a dispatchable state
        var so = await context.SalesOrders
            .Include(s => s.OrderLines)
            .FirstOrDefaultAsync(s => s.Id == request.SalesOrderId, cancellationToken);

        if (so is null)
            throw new Exception("Sales Order not found.");

        if (so.Status != SalesOrderStatus.Confirmed && so.Status != SalesOrderStatus.PartiallyDispatched)
            throw new Exception($"Cannot dispatch against this SO. Current status is {so.Status}.");

        // Validate the product is on this SO
        var soLine = so.OrderLines.FirstOrDefault(l => l.ProductId == request.ProductId);
        if (soLine is null)
            throw new Exception("This product is not listed on this Sales Order.");

        // Validate remaining quantity
        var remainingToDispatch = soLine.OrderedQuantity - soLine.DispatchedQuantity;
        if (request.Quantity > remainingToDispatch)
            throw new Exception($"Over-dispatch error! You ordered {soLine.OrderedQuantity}, already dispatched {soLine.DispatchedQuantity}. You cannot dispatch {request.Quantity} more.");

        // Validate stock level
        var stockLevel = await context.StockLevels
            .FirstOrDefaultAsync(s =>
                s.ProductId == request.ProductId &&
                s.LocationId == request.LocationId,
                cancellationToken);

        if (stockLevel is null)
            throw new Exception("Stock not found at this location.");

        if (stockLevel.Quantity < request.Quantity)
            throw new Exception($"Cannot dispatch {request.Quantity}. Only {stockLevel.Quantity} physically available.");

        // Update stock level
        int qtyBefore = stockLevel.Quantity;
        stockLevel.Quantity -= request.Quantity;
        stockLevel.UpdatedAt = DateTime.UtcNow;

        if (stockLevel.AllocatedQuantity >= request.Quantity)
            stockLevel.AllocatedQuantity -= request.Quantity;

        // Record transaction
        var transaction = new StockTransaction
        {
            ProductId = request.ProductId,
            LocationId = request.LocationId,
            Quantity = request.Quantity,
            QuantityBefore = qtyBefore,
            QuantityAfter = stockLevel.Quantity,
            Type = TransactionType.Issue,
            ReferenceNumber = so.SONumber,
            Notes = request.Notes,
            PerformedBy = currentUserService.Email ?? "System"
        };

        context.StockTransactions.Add(transaction);

        // Update SO line dispatched quantity
        soLine.DispatchedQuantity += request.Quantity;

        // Update SO status
        bool isFullyDispatched = so.OrderLines.All(l => l.DispatchedQuantity >= l.OrderedQuantity);
        so.Status = isFullyDispatched
            ? SalesOrderStatus.Dispatched
            : SalesOrderStatus.PartiallyDispatched;

        await context.SaveChangesAsync(cancellationToken);

        return transaction.Id;
    }
}