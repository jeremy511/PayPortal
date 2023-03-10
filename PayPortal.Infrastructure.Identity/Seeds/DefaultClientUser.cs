using Microsoft.AspNetCore.Identity;
using PayPortal.Core.Application.Enums;
using PayPortal.Infrastructure.Identity.Entities;


namespace PayPortal.Infrastructure.Identity.Seeds
{
    public static class DefaultClientUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser users = new();

            users.FirstName = "John";
            users.LastName = "Doe";
            users.UserName = "clientuser";
            users.Email = "client@gmail.com";
            users.IdCard = "001";
            users.StartingAmount = 0;
            users.EmailConfirmed = true;
            users.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u=> u.Id != users.Id))
            {
                var user = await userManager.FindByEmailAsync(users.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(users, "123Pa$$word!");
                    await userManager.AddToRoleAsync(users, Roles.Client.ToString());
                }
            }
         
        }
    }
}
