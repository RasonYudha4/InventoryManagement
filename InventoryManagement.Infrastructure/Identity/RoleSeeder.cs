using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace InventoryManagement.Infrastructure.Identity;

public static class RoleSeeder
{
    public static readonly string[] Roles = ["Admin", "Operator", "Supervisor"];

    public static async Task SeedAsync(
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager,
        IConfiguration config)
    {
        // Ensure all roles exist
        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Seed a default admin from appsettings (never hard-code credentials)
        var adminEmail = config["Seed:AdminEmail"] ?? "admin@inventory.local";
        if (await userManager.FindByEmailAsync(adminEmail) is null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "System",
                LastName = "Admin",
                EmailConfirmed = true
            };
            var result = await userManager.CreateAsync(admin, config["Seed:AdminPassword"]!);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}