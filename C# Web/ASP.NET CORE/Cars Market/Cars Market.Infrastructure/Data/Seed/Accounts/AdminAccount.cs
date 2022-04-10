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
    public class AdminAccount
    {
        public static async Task AccountInitializer(IServiceProvider serviceProvider)
        {
            var scoped = serviceProvider.CreateScope();

            var context = scoped.ServiceProvider.GetService<ApplicationDbContext>();

            var roleManager = scoped.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            string[] roles = new string[] { "User", "Administrator", "Moderator" };

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

                await RolesCreate.RolesInitializer(serviceProvider, admin.Email, roles);

                var picture = Converter.ConvertPicture("../../Cars Market/Cars Market/wwwroot/img/avatar5.png");

                await ProfileCreate.ProfileInitializer(serviceProvider,
                    new string[] { "Administrator", "Radnevo, Stara Zagora, Bulgaria", "Some info here" }, admin, picture);

                await AdminCarsCreate.CarsInitializer(serviceProvider);
            }
            
            await context.SaveChangesAsync();
        }
    }
}
