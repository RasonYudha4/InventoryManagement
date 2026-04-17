using FluentValidation;

namespace InventoryManagement.Application.Features.Stock.Commands.ReceiveStock;

public class ReceiveStockCommandValidator : AbstractValidator<ReceiveStockCommand>
{
    public ReceiveStockCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");
        RuleFor(x => x.LocationId).NotEmpty().WithMessage("Location ID is required.");
        RuleFor(x => x.PurchaseOrderId).NotEmpty().WithMessage("You cannot receive stock without a valid Purchase Order");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("You must receive at least 1 item. Use an Adjustment to reduce stock.");
            
    }
}