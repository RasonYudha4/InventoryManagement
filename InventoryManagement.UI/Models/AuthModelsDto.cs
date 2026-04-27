namespace InventoryManagement.UI.Models;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AuthResponse
{
    public bool IsSuccessful { get; set; }
    public string? Token { get; set; }
    public string? ErrorMessage { get; set; }
}