using MediatR;

namespace InventoryManagement.Application.Features.SalesOrders.Commands.CreateSalesOrder;

public record SalesOrderLineItemDto(
    Guid ProductId,
    int Quantity,
    decimal UnitPrice,
    DateTime? ExpectedShipDate
);

public record CreateSalesOrderCommand(
    string CustomerName,
    string? CustomerContact,
    string? Notes,
    List<SalesOrderLineItemDto> Items
) : IRequest<Guid>;