using InventoryManagement.API.Authentication;
using InventoryManagement.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<ApplicationUser> userManager,
    TokenService tokenService) : ControllerBase
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized("Invalid credentials.");

        var token = await tokenService.GenerateAccessTokenAsync(user);
        return Ok(new { Token = token });
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole(string email, string roleName)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null) return NotFound("User not found.");

        await userManager.AddToRoleAsync(user, roleName);
        return Ok($"User {email} assigned to role {roleName}.");
    }
}