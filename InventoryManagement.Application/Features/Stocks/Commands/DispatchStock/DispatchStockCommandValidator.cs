using FluentValidation;

namespace InventoryManagement.Application.Features.Stock.Commands.DispatchStock;

public class DispatchStockCommandValidator : AbstractValidator<DispatchStockCommand>
{
    public DispatchStockCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.LocationId).NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("You must dispatch at least 1 item.");

        RuleFor(x => x.SalesOrderNumber)
            .NotEmpty().WithMessage("A Sales Order Number is required for outbound dispatch.");
    }
}