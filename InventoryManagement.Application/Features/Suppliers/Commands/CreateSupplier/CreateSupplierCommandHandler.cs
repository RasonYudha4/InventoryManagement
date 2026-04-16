using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class CreateSupplierCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateSupplierCommand, Guid>
{
    public async Task<Guid> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        var emailExist = await context.Suppliers
            .AnyAsync(s => s.Email == request.Email, cancellationToken);

        if (emailExist)
            throw new Exception($"A supplier with the email {request.Email} already exists.");

        var supplier = new Supplier
        {
            Name = request.Name,
            ContactName = request.ContactName,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            TaxId = request.TaxId,
            IsActive = true
        };

        context.Suppliers.Add(supplier);
        await context.SaveChangesAsync(cancellationToken);

        return supplier.Id;
    }
}