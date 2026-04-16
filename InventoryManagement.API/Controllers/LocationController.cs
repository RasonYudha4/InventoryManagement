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
    public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand command)
    {
        var locationId = await mediator.Send(command);
        return Ok(new { Message = "Location created succcessfully", Id = locationId });
    }
}