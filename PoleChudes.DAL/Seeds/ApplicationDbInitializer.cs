using Microsoft.AspNetCore.Identity;
using PoleChudes.DAL.Entities;
using PoleChudes.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PoleChudes.DAL.Seeds
{
    public static class ApplicationDbInitializer
    {
        public static async Task Seed(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roles = Enum.GetValues(typeof(Roles)).OfType<Roles>().ToList().Select(value => value.ToString());
            foreach (var role in roles)
            {
                var roleCheck = await roleManager.RoleExistsAsync(role);
                if (!roleCheck)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedUsers(UserManager<User> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                var user = new User
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com"
                };

                IdentityResult result = await userManager.CreateAsync(user, "Admin-123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
                }
            }

            if (await userManager.FindByEmailAsync("user@gmail.com") == null)
            {
                var user = new User
                {
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com"
                };

                IdentityResult result = await userManager.CreateAsync(user, "User-123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.User.ToString());
                }
            }
        }
    }
}
