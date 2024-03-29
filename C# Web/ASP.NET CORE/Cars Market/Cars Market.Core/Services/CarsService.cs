﻿using Cars_Market.Core.Services.Contracts;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Core.Services
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext data;
        private readonly ByteConverter converter;
        public CarsService(ApplicationDbContext _data, ByteConverter _converter)
        {
            data = _data;
            converter = _converter;
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

        public async Task UpdateCar(string carId, Car carInfo, CarDetails detailsInfo)
        {
            var car = await GetCarByIdWithDetails(carId);

            car.Make = carInfo.Make;
            car.Model = carInfo.Model;
            car.Year = carInfo.Year;
            car.Money = carInfo.Money;
            car.Details.FuelType = detailsInfo.FuelType;
            car.Details.Description = detailsInfo.Description;
            car.Details.GearboxType = detailsInfo.GearboxType;

            await data.SaveChangesAsync();

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

        public List<CarPicture> GetCarPictures(ICollection<IFormFile> images, Guid carId)
        {
            var pictures = new List<CarPicture>();

            foreach (var picture in images)
            {
                var carPicture = new CarPicture()
                {
                    CarId = carId,
                    Picture = converter.ConvertToByteArray(picture)
                };

                pictures.Add(carPicture);
            }

            return pictures;
        }

        public byte[] GetMainPicture(IFormFile image)
        {
            return converter.ConvertToByteArray(image);
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
