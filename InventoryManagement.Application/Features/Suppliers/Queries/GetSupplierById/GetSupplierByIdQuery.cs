using MediatR;

namespace InventoryManagement.Application.Features.Suppliers.Queries.GetSupplierById;

public record SupplierDetailDto(
    Guid Id,
    string Name,
    string ContactName,
    string Email,
    string? Phone,
    string? Address,
    string? TaxId,
    bool IsActive
);

public record GetSupplierByIdQuery(Guid Id) : IRequest<SupplierDetailDto>;