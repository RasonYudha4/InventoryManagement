using System.Security.Claims;
using InventoryManagement.Application.Interfaces;

namespace InventoryManagement.API.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string? UserId => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    public string? Email => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
}