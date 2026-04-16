using InventoryManagement.Application.Features.Warehouses.Commands.CreateWarehouse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseCommand command)
    {
        var warehouseId = await mediator.Send(command);
        return Ok(new { Message = "Warehouse created successfully", Id = warehouseId });
    }

    [Authorize] 
    [HttpGet]
    public async Task<IActionResult> GetAllWarehouses()
    {
        var query = new GetWarehousesWithLocationsQuery();
        var result = await mediator.Send(query);

        return Ok(result);
    }
}