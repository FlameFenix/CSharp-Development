using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Data.Seed.Roles
{
    public class RolesCreate
    {
        public static async Task RolesInitializer(IServiceProvider serviceProvider, string email, string[] userRoles)
        {

            var scoped = serviceProvider.CreateScope();

            var context = scoped.ServiceProvider.GetService<ApplicationDbContext>();

            var roleManager = scoped.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            string[] roles = new string[] { "User", "Administrator", "Moderator" };

            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }


            await AssignRoles(scoped.ServiceProvider, email, userRoles);

            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<IdentityUser> _userManager = services.GetService<UserManager<IdentityUser>>();
            IdentityUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
