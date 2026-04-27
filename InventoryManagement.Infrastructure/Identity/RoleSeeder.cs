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
        // 1. Ensure all roles exist
        foreach (var role in Roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));
        }

        // 2. Helper function to create synthetic users
        async Task EnsureUserExists(string email, string first, string last, string role, string dept)
        {
            if (await userManager.FindByEmailAsync(email) is null)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = first,
                    LastName = last,
                    Department = dept,
                    EmailConfirmed = true
                };

                // Use a default password from config or a safe fallback
                var password = config["Seed:DefaultPassword"] ?? "P@ssword123!";
                var result = await userManager.CreateAsync(user, password);
                
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, role);
            }
        }

        // 3. Generate Synthetic Data
        await EnsureUserExists("admin@inventory.local", "System", "Admin", "Admin", "IT");
        await EnsureUserExists("operator@inventory.local", "Budi", "Santoso", "Operator", "Warehouse");
        await EnsureUserExists("supervisor@inventory.local", "Siti", "Aminah", "Supervisor", "Logistics");
    }
}