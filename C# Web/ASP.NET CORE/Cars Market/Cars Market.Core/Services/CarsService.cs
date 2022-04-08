﻿using Cars_Market.Core.Services.Contracts;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ICollection<Car>> ShowAllCars()
        {
            return await data.Cars.Where(x => x.Approved == true).Include(x => x.Pictures).ToListAsync();
        }

        public async Task<ICollection<Car>> ShowOrderedCars(string sortByType, string orderByType)
        {
            ICollection<Car> carsList;
            if(orderByType == "Ascending")
            {
                if (sortByType == "Make")
                {
                    carsList = await data.Cars.OrderBy(x => x.Make).ToListAsync();
                }
                else
                {
                    carsList = await data.Cars.OrderBy(x => x.Money).ToListAsync();
                }
            }
            else
            {
                if (sortByType == "Make")
                {
                    carsList = await data.Cars.OrderByDescending(x => x.Make).ToListAsync();
                }
                else
                {
                    carsList = await data.Cars.OrderByDescending(x => x.Money).ToListAsync();
                }
            }

            return carsList;
        }

        public async Task<ICollection<Car>> ShowMyCars(string userId)
        {
            return await data.Cars.Where(x => x.SellerId.ToString() == userId).ToListAsync();
        }
    }
}
