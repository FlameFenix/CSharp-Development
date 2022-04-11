using Cars_Market.Core.Services.Contracts;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
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

            if (data.Cars.Contains(car))
            {
                data.Cars.Remove(car);

                await data.SaveChangesAsync();

            }
        }

        public async Task<bool> CheckCarOwner(string carId, string ownerEmail)
        {
            var carOwner = await data.Cars.Where(x => x.Id.ToString() == carId).Select(x => x.Seller.Email).FirstOrDefaultAsync();

            return carOwner == ownerEmail;
        }
        public async Task<Car> GetCarById(string carId)
        {
            return await data.Cars.FirstOrDefaultAsync(x => x.Id.ToString() == carId);

        }

        public async Task<Car> GetCarByIdWithDetails(string carId)
        {
            return await data.Cars.Include(x => x.Details).FirstOrDefaultAsync(x => x.Id.ToString() == carId);
        }

        public async Task ApproveCar(string carId)
        {
            var car = await data.Cars.FirstOrDefaultAsync(x => x.Id.ToString() == carId);

            car.Approved = true;

            await data.SaveChangesAsync();
        }

        public async Task<ICollection<Car>> GetUnaprovedCars()
        {
            return await data.Cars.Where(x => x.Approved == false).Include(x => x.Pictures).ToListAsync();
        }

        public async Task<ICollection<Car>> GetUnaprovedCarsOrdered(string sortByType, string thenByType, string orderByType)
        {
            List<Car> carsList = null;

            if (orderByType == "Ascending")
            {
                carsList = await data.Cars.Where(x => x.Approved == false).ToListAsync();
                carsList = carsList.OrderBy(x => x.GetType().GetProperty(sortByType).GetValue(x))
                                   .ThenBy(x => x.GetType().GetProperty(thenByType).GetValue(x))
                                   .ToList();
            }
            else
            {
                carsList = await data.Cars.Where(x => x.Approved == false).ToListAsync();
                carsList = carsList.OrderByDescending(x => x.GetType().GetProperty(sortByType).GetValue(x))
                                   .ThenBy(x => x.GetType().GetProperty(thenByType).GetValue(x))
                                   .ToList();
            }

            return carsList;
        }
        public async Task<ICollection<Car>> ShowAllCars()
        {
            return await data.Cars.Where(x => x.Approved == true).Include(x => x.Pictures).ToListAsync();
        }

        public async Task<ICollection<Car>> ShowOrderedCars(string sortByType, string thenByType, string orderByType)
        {
            List<Car> carsList = null;

                if (orderByType == "Ascending")
                {
                    carsList = await data.Cars.Where(x => x.Approved == true).ToListAsync();
                    carsList = carsList.OrderBy(x => x.GetType().GetProperty(sortByType).GetValue(x))
                                       .ThenBy(x => x.GetType().GetProperty(thenByType).GetValue(x))
                                       .ToList();
                }
                else
                {
                    carsList = await data.Cars.Where(x => x.Approved == true).ToListAsync();
                    carsList = carsList.OrderByDescending(x => x.GetType().GetProperty(sortByType).GetValue(x))
                                       .ThenBy(x => x.GetType().GetProperty(thenByType).GetValue(x))
                                       .ToList();
                }

            return carsList;
        }

        public async Task<ICollection<Car>> ShowMyCars(string userId)
        {
            return await data.Cars.Where(x => x.SellerId.ToString() == userId).ToListAsync();
        }

        public async Task<ICollection<Car>> ShowMyCarsOrdered(string userId, string sortByType, string thenByType, string orderByType)
        {
            List<Car> carsList = null;

            if (orderByType == "Ascending")
            {
                carsList = await data.Cars.Where(x => x.SellerId.ToString() == userId).ToListAsync();
                carsList = carsList.OrderBy(x => x.GetType().GetProperty(sortByType).GetValue(x))
                                   .ThenBy(x => x.GetType().GetProperty(thenByType).GetValue(x))
                                   .ToList();
            }
            else
            {
                carsList = await data.Cars.Where(x => x.SellerId.ToString() == userId).ToListAsync();
                carsList = carsList.OrderByDescending(x => x.GetType().GetProperty(sortByType).GetValue(x))
                                   .ThenBy(x => x.GetType().GetProperty(thenByType).GetValue(x))
                                   .ToList();
            }

            return carsList;
        }
    }
}
