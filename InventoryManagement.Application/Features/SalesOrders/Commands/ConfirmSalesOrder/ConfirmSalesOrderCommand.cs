using MediatR;

namespace InventoryManagement.Application.Features.SalesOrders.Commands.ConfirmSalesOrder;

public record ConfirmSalesOrderCommand(Guid SalesOrderId) : IRequest<bool>;