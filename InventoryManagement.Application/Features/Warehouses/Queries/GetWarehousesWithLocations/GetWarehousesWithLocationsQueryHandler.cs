using InventoryManagement.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetWarehousesWithLocationsQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetWarehousesWithLocationsQuery, List<WarehouseDto>>
{
    public async Task<List<WarehouseDto>> Handle(GetWarehousesWithLocationsQuery request, CancellationToken cancellationToken)
    {
        return await context.Warehouses
            .AsNoTracking()
            .Include(w => w.Locations)
            .Select(w => new WarehouseDto(
                w.Id,
                w.Code,
                w.Name,
                w.Locations.Select(l => new LocationDto(l.Id, l.Code, l.MaxWeightCapacity)).ToList()
            )).ToListAsync(cancellationToken);
    }
}