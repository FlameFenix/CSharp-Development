using Cars_Market.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Data.Seed.Profiles
{
    public static class ProfileCreate
    {
        public static async Task ProfileInitializer(IServiceProvider serviceProvider, string[] info, IdentityUser user, byte[] picture)
        {
            var scoped = serviceProvider.CreateScope();

            var context = scoped.ServiceProvider.GetService<ApplicationDbContext>();

            var seller = new Seller()
            {
                Email = user.Email,
                Profile = new Profile()
                {
                    Name = info[0],
                    Phone = user.PhoneNumber,
                    Location = info[1],
                    Picture = picture
                    //, About = info[2]
                }
            };

            await context.Sellers.AddAsync(seller);

            await context.SaveChangesAsync();
        }
    }
}
