using InventoryManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.SalesOrders.Queries.GetAllSalesOrders;

public class GetAllSalesOrdersQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetAllSalesOrdersQuery, List<SalesOrderSummaryDto>>
{
    public async Task<List<SalesOrderSummaryDto>> Handle(
        GetAllSalesOrdersQuery request,
        CancellationToken cancellationToken)
    {
        return await context.SalesOrders
            .Where(so => !so.IsDeleted)
            .OrderByDescending(so => so.OrderDate)
                .ThenBy(so => so.SONumber)
            .Select(so => new SalesOrderSummaryDto(
                so.Id,
                so.SONumber,
                so.OrderDate,
                so.CustomerName,
                so.Status.ToString(),
                so.TotalAmount,
                so.OrderLines.Count,
                so.Notes
            ))
            .ToListAsync(cancellationToken);
    }
}