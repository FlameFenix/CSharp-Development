using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Data.Seed
{
    public static class AccountAndRolesSeed
    {
        public static async Task AccountInitializer(IServiceProvider serviceProvider)
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


            var admin = new IdentityUser
            {
                Email = "admin@carsmarket.com",
                NormalizedEmail = "ADMIN@CARSMARKET.COM",
                UserName = "admin@carsmarket.com",
                NormalizedUserName = "ADMIN@CARSMARKET.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == admin.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(admin, "admin");
                admin.PasswordHash = hashed;

                var userStore = new UserStore<IdentityUser>(context);
                var result = userStore.CreateAsync(admin);

            }

            var user = new IdentityUser
            {
                Email = "user@carsmarket.com",
                NormalizedEmail = "USER@CARSMARKET.COM",
                UserName = "user@carsmarket.com",
                NormalizedUserName = "USER@CARSMARKET.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(user, "user");
                user.PasswordHash = hashed;

                var userStore = new UserStore<IdentityUser>(context);
                var result = userStore.CreateAsync(user);

            }

            if(!context.Sellers.Any(x => x.Email == admin.Email))
            {
                await SellerAndProfileSeed.SellerInitialize(serviceProvider, admin.Email);
            }

            if (!context.Sellers.Any(x => x.Email == user.Email))
            {
                await SellerAndProfileSeed.SellerInitialize(serviceProvider, user.Email);
            }
            

            await AssignRoles(scoped.ServiceProvider, admin.Email, roles);
            await AssignRoles(scoped.ServiceProvider, user.Email, new string[] { "User" });

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
