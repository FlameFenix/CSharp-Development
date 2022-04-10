using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Infrastructure.Data.Seed.PictureConverter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Infrastructure.Data.Seed.Cars
{
    public static class AdminCarsCreate
    {
        public static async Task CarsInitializer(IServiceProvider serviceProvider)
        {
            var scoped = serviceProvider.CreateScope();

            var context = scoped.ServiceProvider.GetService<ApplicationDbContext>();

            var seller = await context.Sellers.FirstOrDefaultAsync(x => x.Email == "admin@carsmarket.com");

            var car = new Car()
            {
                MainPicture = Converter.ConvertPicture("../../Cars Market/Cars Market/wwwroot/img/cars/mazda rx-8.jpg"),
                SellerId = seller.Id,
                Approved = true,
                Make = "Mazda",
                Model = "RX-8",
                Money = double.Parse("8500.50", CultureInfo.InvariantCulture),
                Year = 2005,
                Pictures = new List<CarPicture>()
                {
                    new CarPicture()
                    {
                        Id = Guid.NewGuid(),

                        Picture = Converter.ConvertPicture("../../Cars Market/Cars Market/wwwroot/img/cars/mazda rx-8 second.jpg")
                    }
                 },
                Details = new CarDetails()
                {
                    Color = "Silver",
                    Description = "The Mazda RX-8 is a desirable, but unusual and unconventional sports car. Though it was launched back in 2003, it still looks futuristic and sleek, and is beautifully detailed. Under the bonnet, it features a high-revving 1.3-litre rotary engine.",
                    FuelType = "Petrol",
                    IsSold = false,
                    GearboxType = "Manual",
                }
            };

            var anotherCar = new Car()
            {
                MainPicture = Converter.ConvertPicture("../../Cars Market/Cars Market/wwwroot/img/cars/a6.jpg"),
                SellerId = seller.Id,
                Approved = false,
                Make = "Audi",
                Model = "A6",
                Money = double.Parse("10500.50", CultureInfo.InvariantCulture),
                Year = 2010,
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
