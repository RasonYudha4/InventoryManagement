using MediatR;

namespace InventoryManagement.Application.Features.SalesOrders.Queries.GetSalesOrderById;

public record SalesOrderLineDetailDto(
    Guid Id,
    int LineNumber,
    Guid ProductId,
    string SKU,
    string ProductName,
    int OrderedQuantity,
    int DispatchedQuantity,
    decimal UnitPrice,
    decimal TotalPrice,
    DateTime? ExpectedShipDate
);

public record SalesOrderDetailDto(
    Guid Id,
    string SONumber,
    DateTime OrderDate,
    string CustomerName,
    string? CustomerContact,
    string Status,
    decimal TotalAmount,
    string? Notes,
    List<SalesOrderLineDetailDto> OrderLines
);

public record GetSalesOrderByIdQuery(Guid Id) : IRequest<SalesOrderDetailDto>;