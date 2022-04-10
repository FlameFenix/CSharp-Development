using Cars_Market.Infrastructure.Data.Seed.Cars;
using Cars_Market.Infrastructure.Data.Seed.PictureConverter;
using Cars_Market.Infrastructure.Data.Seed.Profiles;
using Cars_Market.Infrastructure.Data.Seed.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Data.Seed.Accounts
{
    public class UserAccount
    {
        public static async Task AccountInitializer(IServiceProvider serviceProvider)
        {
            var scoped = serviceProvider.CreateScope();

            var context = scoped.ServiceProvider.GetService<ApplicationDbContext>();

            var roleManager = scoped.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            string[] roles = new string[] { "User" };

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

                await RolesCreate.RolesInitializer(serviceProvider, user.Email, roles);

                var picture = Converter.ConvertPicture("../../Cars Market/Cars Market/wwwroot/img/user-logo.jpg");

                await ProfileCreate.ProfileInitializer(serviceProvider,
                    new string[] { "User", "Varna, Bulgaria", "Some info here" }, user, picture);

                await UserCarsCreate.CarsInitializer(serviceProvider);
            }

            await context.SaveChangesAsync();
        }
    }
}
