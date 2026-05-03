using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.SalesOrders.Commands.ConfirmSalesOrder;

public class ConfirmSalesOrderCommandHandler(IApplicationDbContext context)
    : IRequestHandler<ConfirmSalesOrderCommand, bool>
{
    public async Task<bool> Handle(ConfirmSalesOrderCommand request, CancellationToken cancellationToken)
    {
        var so = await context.SalesOrders
            .FirstOrDefaultAsync(s => s.Id == request.SalesOrderId, cancellationToken);

        if (so is null)
            throw new Exception("Sales Order not found.");

        if (so.Status != SalesOrderStatus.Draft)
            throw new Exception($"Cannot confirm order. Current status is {so.Status}.");

        so.Status = SalesOrderStatus.Confirmed;

        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}