using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.Warehouses.Commands.CreateWarehouse;

public class CreateWarehouseCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateWarehouseCommand, Guid>
{
    public async Task<Guid> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
    {
        var codeExists = await context.Warehouses
            .AnyAsync(w => w.Code == request.Code, cancellationToken);

        if (codeExists)
        {
            throw new Exception($"A warehouse with the code {request.Code} already exists.");
        }

        var warehouse = new Warehouse
        {
            Code = request.Code,
            Name = request.Name,
            Address = request.Address,
            IsActive = true
        };

        context.Warehouses.Add(warehouse);
        await context.SaveChangesAsync(cancellationToken);

        return warehouse.Id;
    }
}