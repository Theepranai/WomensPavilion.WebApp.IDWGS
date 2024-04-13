using Microsoft.AspNetCore.Identity;
using PixelPlusMedia.Application.Authorization;
using PixelPlusMedia.Identity.Models;
using System.Security.Claims;

namespace PixelPlusMedia.Identity.Seed
{
    public static class CreateFirstUser
    {
        /// <summary>
        /// Seed
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <returns></returns>
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            var applicationUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                ClientName = "Admin",
                UserName = "admin",
            };

            var AdminUserRole = await roleManager.FindByNameAsync("Administrator");

            await CreateRolesIfNull(roleManager, AdminUserRole);
            await SetUpRolesPermissions(roleManager, AdminUserRole);

            var user = await userManager.FindByNameAsync(applicationUser.UserName);
            if (user == null)
            {
                await userManager.CreateAsync(applicationUser, "P1xel@12345");
                await userManager.AddToRoleAsync(applicationUser, "Administrator");
            }
        }

        /// <summary>
        /// Create Role If Null
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="AdminRole"></param>
        /// <returns></returns>
        public static async Task CreateRolesIfNull(RoleManager<ApplicationRole> roleManager, ApplicationRole AdminRole)
        {
            if (AdminRole == null)
            {
                await roleManager.CreateAsync(new ApplicationRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrator"
                });
            }
        }
        
        /// <summary>
        /// Setup Role Permission
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="AdminRole"></param>
        /// <returns></returns>
        public static async Task SetUpRolesPermissions(
            RoleManager<ApplicationRole> roleManager,
            ApplicationRole AdminRole)
        {
            var AdminRoleClaims = await roleManager.GetClaimsAsync(AdminRole);
            if (AdminRoleClaims.Count == 0)
            {
                await SetUpAdminPermissions(AdminRole, roleManager);
            }
        }

        public static async Task SetUpAdminPermissions(ApplicationRole role, RoleManager<ApplicationRole> roleManager)
        {
            /**
             * Grant All User permissions
             */
            var userFields = typeof(Permission.Users).GetFields();
            foreach (var field in userFields)
            {
                await roleManager.AddClaimAsync(role, new Claim(ClaimType.Permission, field.GetValue(field.Name).ToString()));
            }
        }
    }
}
