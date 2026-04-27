using InventoryManagement.API.Models.Requests;
using InventoryManagement.Application.Features.Stock.Queries;
using InventoryManagement.Application.Features.Warehouses.Commands;
using InventoryManagement.Application.Features.Warehouses.Queries.GetWarehouseById;
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
    public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseRequest request)
    {
        var command = new CreateWarehouseCommand(
            request.Code,
            request.Name,
            request.Address
        );

        var warehouseId = await mediator.Send(command);
        return Ok(new { Message = "Warehouse created successfully", Id = warehouseId });
    }

    [Authorize(Roles = "Admin,Manager,WarehouseStaff,Auditor")]
    [HttpGet]
    public async Task<IActionResult> GetAllWarehouses()
    {
        var query = new GetWarehousesWithLocationsQuery();
        var result = await mediator.Send(query);

        return Ok(result);
    }

    [Authorize(Roles = "Admin,Manager,WarehouseStaff,Auditor")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetWarehouseById(Guid id)
    {
        var query = new GetWarehouseByIdQuery(id);
        var result = await mediator.Send(query);
        return Ok(result);
}
}