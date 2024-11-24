using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Project.Infrastructure.Identity;


public static class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Seed roles
        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Create the roles if they do not exist
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Seed a default admin user
        var adminUser = new ApplicationUser
        {
            UserName = "admin@123",
            Email = "admin@gmail.com",
            FirstName = "Super",
            LastName = "Admin",
            PhoneNumber = "01711223344",
           
        };

        string adminPassword = "admin@123";

        var user = await userManager.FindByEmailAsync(adminUser.Email);

        if (user == null)
        {
            var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
            if (createAdminUser.Succeeded)
            {
                // Assign the "Admin" role to the admin user
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
