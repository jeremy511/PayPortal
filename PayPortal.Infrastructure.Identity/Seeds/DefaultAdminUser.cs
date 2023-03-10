
using Microsoft.AspNetCore.Identity;
using PayPortal.Core.Application.Enums;
using PayPortal.Infrastructure.Identity.Entities;


namespace PayPortal.Infrastructure.Identity.Seeds
{
    public class DefaultAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser users = new();

            users.FirstName = "Jhon";
            users.LastName = "Doe";
            users.UserName = "adminuser";
            users.Email = "admin@gmail.com";
            users.EmailConfirmed = true;
            users.PhoneNumberConfirmed = true;
            users.IdCard = "001";

            if (userManager.Users.All(u => u.Id != users.Id))
            {
                await userManager.CreateAsync(users, "123Pa$$");
                await userManager.AddToRoleAsync(users, Roles.Client.ToString());
                await userManager.AddToRoleAsync(users, Roles.Admin.ToString());

            }
        }
    }
}
