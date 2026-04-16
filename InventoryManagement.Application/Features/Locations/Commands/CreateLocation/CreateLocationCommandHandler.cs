using System.Security.Cryptography.X509Certificates;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.Locations.Commands.CreateLocation;

public class CreateLocationCommandHandler(IApplicationDbContext context) 
    : IRequestHandler<CreateLocationCommand, Guid>
{
    public async Task<Guid> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var warehouseExist = await context.Warehouses
            .AnyAsync(w => w.Id == request.WarehouseId, cancellationToken);

        if (!warehouseExist)
            throw new Exception($"Warehouse with ID {request.WarehouseId} does not exist.");

        var codeExists = await context.Locations
            .AnyAsync(l => l.Code == request.Code, cancellationToken);

        if (codeExists)
            throw new Exception($"A location with the code '{request.Code}' already exists");

        var location = new Location
        {
            WarehouseId = request.WarehouseId,
            Code = request.Code,
            Aisle = request.Aisle,
            Rack = request.Rack,
            Shelf = request.Shelf,
            Bin = request.Bin,
            MaxWeightCapacity = request.MaxWeightCapacity,
            IsActive = true
        };

        context.Locations.Add(location);
        await context.SaveChangesAsync(cancellationToken);

        return location.Id;
    }
}