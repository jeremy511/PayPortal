using Microsoft.AspNetCore.Identity;
using PayPortal.Core.Application.Enums;
using PayPortal.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPortal.Infrastructure.Identity.Seeds
{
    public static class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser users = new();

            users.FirstName = "Jhon";
            users.LastName = "Doe";
            users.UserName = "superadminuser";
            users.Email = "superadmin@gmail.com";
            users.EmailConfirmed = true;
            users.PhoneNumberConfirmed = true;
            users.IdCard = "001";

            if (userManager.Users.All(u => u.Id != users.Id))
            {
                await userManager.CreateAsync(users, "123Pa$$");
                await userManager.AddToRoleAsync(users, Roles.Client.ToString());
                await userManager.AddToRoleAsync(users, Roles.Admin.ToString());
                await userManager.AddToRoleAsync(users, Roles.SuperAdmin.ToString());
            }

        }
    }
}
