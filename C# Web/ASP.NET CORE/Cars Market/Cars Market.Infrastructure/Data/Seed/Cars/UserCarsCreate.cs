using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Infrastructure.Data.Seed.PictureConverter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Data.Seed.Cars
{
    public class UserCarsCreate
    {
        public static async Task CarsInitializer(IServiceProvider serviceProvider)
        {
            var scoped = serviceProvider.CreateScope();

            var context = scoped.ServiceProvider.GetService<ApplicationDbContext>();

            var seller = await context.Sellers.FirstOrDefaultAsync(x => x.Email == "user@carsmarket.com");

            var car = new Car()
            {
                MainPicture = Converter.ConvertPicture("../../Cars Market/Cars Market/wwwroot/img/cars/a3.jpg"),
                SellerId = seller.Id,
                Approved = true,
                Make = "Audi",
                Model = "A3",
                Money = double.Parse("4999", CultureInfo.InvariantCulture),
                Year = 2005,
                Details = new CarDetails()
                {
                    Color = "Black",
                    Description = "The Audi A6 is an executive car made by the German automaker Audi. Now in its fifth generation, the successor to the Audi 100 is manufactured in Neckarsulm, Germany, and is available in saloon and estate configurations, the latter marketed by Audi as the Avant.",
                    FuelType = "Diesel",
                    IsSold = false,
                    GearboxType = "Manual",
                }
            };

            var anotherCar = new Car()
            {
                MainPicture = Converter.ConvertPicture("../../Cars Market/Cars Market/wwwroot/img/cars/audi a6.jpg"),
                SellerId = seller.Id,
                Approved = true,
                Make = "Audi",
                Model = "A6",
                Money = double.Parse("10500.50", CultureInfo.InvariantCulture),
                Year = 2014,
                Details = new CarDetails()
                {
                    Color = "Black",
                    Description = "The Audi A6 is an executive car made by the German automaker Audi. Now in its fifth generation, the successor to the Audi 100 is manufactured in Neckarsulm, Germany, and is available in saloon and estate configurations, the latter marketed by Audi as the Avant.",
                    FuelType = "Diesel",
                    IsSold = false,
                    GearboxType = "Automatic",
                }
            };

            await context.Cars.AddAsync(car);
            await context.Cars.AddAsync(anotherCar);
            await context.SaveChangesAsync();
        }
    }
}
