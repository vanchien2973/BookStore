using Microsoft.AspNetCore.Identity;

namespace BookStore.Models;

public static class UserRoleInitiazer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<DefaultUser>>();
        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;
        foreach (var role in roleNames)
        {
            var roleExists = await
                roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                roleResult = await roleManager.CreateAsync(role: new IdentityRole(role));
            }
        }

        var email = "admin@gmail.com";
        var password = "P@ssw0rd";

        if (userManager.FindByEmailAsync(email).Result == null)
        {
            DefaultUser user = new()
            {
                Email = email,
                UserName = email,
                FirstName = "Thai",
                LastName = "Van Chien",
                Address = "2 Pham Van Bach",
                City = "Hanoi",
                ZipCode = "12345"
            };
            IdentityResult result = userManager.CreateAsync(user, password).Result;
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, role: "Admin").Wait();
            }
        }
    }
}