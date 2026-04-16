using FluentValidation;

namespace InventoryManagement.Application.Features.Warehouses.Commands.CreateWarehouse;

public class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
{
    public CreateWarehouseCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(20)
            .WithMessage("Warehouse code must be under 20 characters.");

        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
    }
}