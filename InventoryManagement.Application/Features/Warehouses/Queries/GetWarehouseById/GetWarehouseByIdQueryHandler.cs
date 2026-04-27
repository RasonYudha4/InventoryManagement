using InventoryManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Application.Features.Warehouses.Queries.GetWarehouseById;

public class GetWarehouseByIdQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetWarehouseByIdQuery, WarehouseDetailDto>
{
    public async Task<WarehouseDetailDto> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
    {
        var warehouse = await context.Warehouses
            .Include(w => w.Locations)
            .Where(w => w.Id == request.Id && w.IsActive)
            .Select(w => new WarehouseDetailDto(
                w.Id,
                w.Code,
                w.Name,
                w.Address,
                w.IsActive,
                w.Locations.Select(l => new LocationDetailDto(
                    l.Id,
                    l.Code,
                    l.Aisle,
                    l.Rack,
                    l.Shelf,
                    l.Bin,
                    l.MaxWeightCapacity
                )).ToList()
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (warehouse is null)
            throw new KeyNotFoundException($"Warehouse with ID {request.Id} was not found.");

        return warehouse;
    }
}