using InventoryManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.SalesOrders.Queries.GetSalesOrderById;

public class GetSalesOrderByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetSalesOrderByIdQuery, SalesOrderDetailDto>
{
    public async Task<SalesOrderDetailDto> Handle(
        GetSalesOrderByIdQuery request,
        CancellationToken cancellationToken)
    {
        var salesOrder = await context.SalesOrders
            .Where(so => so.Id == request.Id && !so.IsDeleted)
            .Select(so => new SalesOrderDetailDto(
                so.Id,
                so.SONumber,
                so.OrderDate,
                so.CustomerName,
                so.CustomerContact,
                so.Status.ToString(),
                so.TotalAmount,
                so.Notes,
                so.OrderLines
                    .OrderBy(l => l.LineNumber)
                    .Select(l => new SalesOrderLineDetailDto(
                        l.Id,
                        l.LineNumber,
                        l.ProductId,
                        l.Product.SKU,
                        l.Product.Name,
                        l.OrderedQuantity,
                        l.DispatchedQuantity,
                        l.UnitPrice,
                        l.TotalPrice,
                        l.ExpectedShipDate
                    ))
                    .ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (salesOrder is null)
            throw new KeyNotFoundException($"Sales Order with ID {request.Id} was not found.");

        return salesOrder;
    }
}