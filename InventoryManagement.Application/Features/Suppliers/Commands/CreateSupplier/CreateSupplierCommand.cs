using MediatR;

public record CreateSupplierCommand(
    string Name,
    string ContactName,
    string Email,
    string? Phone,
    string? Address,
    string? TaxId
) : IRequest<Guid>;