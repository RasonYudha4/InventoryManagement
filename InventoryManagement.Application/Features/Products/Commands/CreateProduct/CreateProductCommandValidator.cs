using FluentValidation;

namespace InventoryManagement.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.SKU).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.UnitCost).GreaterThanOrEqualTo(0);
        RuleFor(x => x.SellingPrice).GreaterThan(x => x.UnitCost)
            .WithMessage("Selling price must be greater than unit cost.");
        RuleFor(x => x.ReorderPoint).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ReorderQuantity).GreaterThan(0);
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.SupplierId).NotEmpty();
    }
}