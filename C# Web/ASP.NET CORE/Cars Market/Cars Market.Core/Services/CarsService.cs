using Cars_Market.Core.Services.Contracts;
using Cars_Market.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Core.Services
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext data;
        public CarsService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task AddCar(Car car)
        {
            await data.Cars.AddAsync(car);

            await data.SaveChangesAsync();
        }

        public async Task RemoveCar(string carId)
        {
            var car = await data.Cars.FirstOrDefaultAsync(x => x.Id.ToString() == carId);

            data.Cars.Remove(car);

            await data.SaveChangesAsync();

        }

        public async Task<ICollection<Car>> ShowAllCars()
        {
            return await data.Cars.ToListAsync();
        }

        public async Task<ICollection<Car>> ShowMyCars(string userId)
        {
            return await data.Cars.Where(x => x.SellerId.ToString() == userId).ToListAsync();
        }
    }
}
