using InventoryManagement.API.Models.Requests;
using InventoryManagement.Application.Features.Locations.Commands.CreateLocation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationRequest request)
    {
        var command = new CreateLocationCommand(
            request.WarehouseId,
            request.Code,
            request.Aisle,
            request.Rack,
            request.Shelf,
            request.Bin,
            request.MaxWeightCapacity
        );

        var locationId = await mediator.Send(command);
        return Ok(new { Message = "Location created successfully", Id = locationId });
    }
}