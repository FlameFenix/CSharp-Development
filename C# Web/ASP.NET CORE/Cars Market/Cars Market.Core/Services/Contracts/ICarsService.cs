using Cars_Market.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars_Market.Core.Services.Contracts
{
    internal interface ICarsService
    {
        public Task AddCar(Car car);

        public Task RemoveCar(string carId);

        public Task<Car> GetCarById(string carId);

        public Task ApproveCar(string carId);

        public Task<ICollection<Car>> GetUnaprovedCars();
        public Task<ICollection<Car>> ShowAllCars();

        public Task<ICollection<Car>> ShowMyCars(string userId);

        public Task<ICollection<Car>> ShowOrderedCars(string sortByType, string orderByType);
    }
}
