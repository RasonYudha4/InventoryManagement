using FluentValidation;

namespace InventoryManagement.Application.Features.SalesOrders.Commands.ConfirmSalesOrder;

public class ConfirmSalesOrderCommandValidator : AbstractValidator<ConfirmSalesOrderCommand>
{
    public ConfirmSalesOrderCommandValidator()
    {
        RuleFor(x => x.SalesOrderId).NotEmpty();
    }
}