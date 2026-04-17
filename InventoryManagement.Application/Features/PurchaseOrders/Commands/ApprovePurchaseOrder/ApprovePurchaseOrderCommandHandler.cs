using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class ApprovePurchaseOrderCommandHandler(IApplicationDbContext context)
    : IRequestHandler<ApprovePurchaseOrderCommand, bool>
{
    public async Task<bool> Handle(ApprovePurchaseOrderCommand request, CancellationToken cancellationToken)
    {
        var po = await context.PurchaseOrders
            .FirstOrDefaultAsync(p => p.Id == request.PurchaseOrderId, cancellationToken);

        if (po == null) throw new Exception("Purchase Order not found.");

        if (po.Status != PurchaseOrderStatus.Draft && po.Status != PurchaseOrderStatus.PendingApproval)
        {
            throw new Exception($"Cannot approve order. Current status is {po.Status}.");
        }

        po.Status = PurchaseOrderStatus.Approved;

        await context.SaveChangesAsync(cancellationToken);
        return true;
    }
}