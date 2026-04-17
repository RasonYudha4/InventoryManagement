using FluentValidation;

namespace InventoryManagement.Application.Features.PurchaseOrders.Commands.CreatePurchaseOrder;

public class CreatePurchaseOrderCommandValidator : AbstractValidator<CreatePurchaseOrderCommand>
{
    public CreatePurchaseOrderCommandValidator()
    {
        RuleFor(x => x.SupplierId).NotEmpty();
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage("A purchase order must contain at least one item.");

        RuleForEach(x => x.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId).NotEmpty();
            items.RuleFor(i => i.Quantity).GreaterThan(0);
            items.RuleFor(i => i.NegotiatedUnitCost).GreaterThanOrEqualTo(0);
        });
    }
}