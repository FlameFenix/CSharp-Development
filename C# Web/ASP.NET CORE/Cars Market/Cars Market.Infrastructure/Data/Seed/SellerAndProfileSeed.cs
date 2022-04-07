using Cars_Market.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Data.Seed
{
    public static class SellerAndProfileSeed
    {
        public static async Task SellerInitialize(IServiceProvider serviceProvider, string userEmail )
        {
            var scoped = serviceProvider.CreateScope();

            var context = scoped.ServiceProvider.GetService<ApplicationDbContext>();

            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == userEmail);

            Seller seller = new()
            {
                Email = userEmail,
            };

            FileStream stream = new("../../Cars Market/Cars Market/wwwroot/img/defaultprofile.jpg", FileMode.Open, FileAccess.Read);

            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0 , bytes.Length);

            Profile profile = new()
            {
                Location = "TestLocation",
                Name = userEmail == "admin@carsmarket.com" ? "TestAdmin" : "TestUser",
                Phone = "+359123123123",
                SellerId = seller.Id,
                Picture = bytes,
            };

            seller.Profile = profile;

            await context.Sellers.AddAsync(seller);

            await context.SaveChangesAsync();
        }
    }
}
