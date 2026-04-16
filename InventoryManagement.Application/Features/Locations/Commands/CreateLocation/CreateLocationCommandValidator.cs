using FluentValidation;

namespace InventoryManagement.Application.Features.Locations.Commands.CreateLocation;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(x => x.WarehouseId)
            .NotEmpty().WithMessage("A location must belong to a warehouse.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.MaxWeightCapacity)
            .GreaterThan(0).WithMessage("Max weight capacity must be greater than zero.");
    }
}