using Cars_Market.Infrastructure.Data.Seed.Cars;
using Cars_Market.Infrastructure.Data.Seed.PictureConverter;
using Cars_Market.Infrastructure.Data.Seed.Profiles;
using Cars_Market.Infrastructure.Data.Seed.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cars_Market.Infrastructure.Data.Seed.Accounts
{
    public class ModeratorAccount
    {
        public static async Task AccountInitializer(IServiceProvider serviceProvider)
        {
            var scoped = serviceProvider.CreateScope();

            var context = scoped.ServiceProvider.GetService<ApplicationDbContext>();

            var roleManager = scoped.ServiceProvider.GetService<RoleManager<IdentityRole>>();

            string[] roles = new string[] { "Moderator", "User" };

            var moderator = new IdentityUser
            {
                Email = "moderator@carsmarket.com",
                NormalizedEmail = "MODERATOR@CARSMARKET.COM",
                UserName = "moderator@carsmarket.com",
                NormalizedUserName = "MODERATOR@CARSMARKET.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == moderator.UserName))
            {
                var password = new PasswordHasher<IdentityUser>();
                var hashed = password.HashPassword(moderator, "moderator");
                moderator.PasswordHash = hashed;

                var userStore = new UserStore<IdentityUser>(context);
                var result = userStore.CreateAsync(moderator);

                await RolesCreate.RolesInitializer(serviceProvider, moderator.Email, roles);

                var picture = Converter.ConvertPicture("../../Cars Market/Cars Market/wwwroot/img/moderator.png");

                await ProfileCreate.ProfileInitializer(serviceProvider,
                    new string[] { "Moderator", "Plovdiv, Bulgaria", "Some info here" }, moderator, picture);

                await ModeratorCarsCreate.CarsInitializer(serviceProvider);
            }

            await context.SaveChangesAsync();
        }
    }
}