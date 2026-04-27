using InventoryManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.Suppliers.Queries.GetSupplierById;

public class GetSupplierByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetSupplierByIdQuery, SupplierDetailDto>
{
    public async Task<SupplierDetailDto> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
    {
        var supplier = await context.Suppliers
            .Where(s => s.Id == request.Id && s.IsActive)
            .Select(s => new SupplierDetailDto(
                s.Id,
                s.Name,
                s.ContactName,
                s.Email,
                s.Phone,
                s.Address,
                s.TaxId,
                s.IsActive))
            .FirstOrDefaultAsync(cancellationToken);

        if (supplier is null)
            throw new KeyNotFoundException($"Supplier with ID {request.Id} was not found.");

        return supplier;
    }
}