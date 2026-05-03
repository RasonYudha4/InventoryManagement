using MediatR;

namespace InventoryManagement.Application.Features.SalesOrders.Queries.GetAllSalesOrders;

public record SalesOrderSummaryDto(
    Guid Id,
    string SONumber,
    DateTime OrderDate,
    string CustomerName,
    string Status,
    decimal TotalAmount,
    int TotalLines,
    string? Notes
);

public record GetAllSalesOrdersQuery : IRequest<List<SalesOrderSummaryDto>>;
