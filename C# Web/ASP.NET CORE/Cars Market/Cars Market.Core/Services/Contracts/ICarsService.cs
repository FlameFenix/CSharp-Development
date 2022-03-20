using Cars_Market.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars_Market.Core.Services.Contracts
{
    internal interface ICarsService
    {
        public Task AddCar(Car car);

        public Task RemoveCar(string carId);

        public Task<ICollection<Car>> ShowAllCars();

        public Task<ICollection<Car>> ShowMyCars(string userId);
    }
}
