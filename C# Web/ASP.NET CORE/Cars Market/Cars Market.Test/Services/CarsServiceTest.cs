using Cars_Market.Controllers;
using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Cars_Market.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Cars_Market.Test.Services
{
    public class CarsServiceTest
    {
        [Fact]
        public void ShouldReturnTwoCarsWithShowAllCarsMethodBecauseTheyAreApproved()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car()
            {
                Make = "Audi",
                Model = "A6",
                Approved = true,
                MainPicture = new byte[] { }
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "BMW",
                Model = "X5",
                Approved = true,
                MainPicture = new byte[] { }
            });

            data.SaveChanges();

            var carsService = new CarsService(data);

            var car = data.Cars.FirstOrDefault(x => x.Make == "BMW");

            var expectedCars = Task.Run(() => carsService.ShowAllCars()).GetAwaiter().GetResult();

            Assert.True(expectedCars.Count == 2);
        }

        [Fact]
        public void ShouldReturnZeroCarsWithShowAllCarsMethodBecauseTheyAreNOTApproved()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car()
            {
                Make = "Audi",
                Model = "A6",
                Approved = false,
                MainPicture = new byte[] { }
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "BMW",
                Model = "X5",
                Approved = false,
                MainPicture = new byte[] { }
            });

            data.SaveChanges();

            var carsService = new CarsService(data);

            var car = data.Cars.FirstOrDefault(x => x.Make == "BMW");

            var expectedCars = Task.Run(() => carsService.ShowAllCars()).GetAwaiter().GetResult();

            Assert.True(expectedCars.Count == 0);
        }

        [Fact]
        public void ShouldReturnTrueGetCarById()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Approved = true,
                Make = "Audi",
                Model = "A6",
                MainPicture = new byte[] { }
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Approved = true,
                Make = "BMW",
                Model = "X3",
                MainPicture = new byte[] { }
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Mercedes",
                Model = "E-Class",
                MainPicture = new byte[] { }
            });

            data.SaveChanges();

            var service = new CarsService(data);

            var carAudi = data.Cars.FirstOrDefault(x => x.Make == "Audi");

            var serviceCar = service.GetCarById(carAudi.Id.ToString()).GetAwaiter().GetResult();

            Assert.Equal(carAudi, serviceCar);
        }

        [Fact]
        public void ShouldReturnTrueDeleteCarById()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Approved = true,
                Make = "Audi",
                Model = "A6",
                MainPicture = new byte[] { }
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Approved = true,
                Make = "BMW",
                Model = "X3",
                MainPicture = new byte[] { }
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Mercedes",
                Model = "E-Class",
                MainPicture = new byte[] { }
            });

            data.SaveChanges();

            var service = new CarsService(data);

            var carAudi = data.Cars.FirstOrDefault(x => x.Make == "Audi");

            var serviceCar = service.RemoveCar(carAudi.Id.ToString());

            Assert.False(data.Cars.Contains(carAudi));
        }

        [Fact]
        public void ShouldReturnTrueAddCar()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new CarsService(data);

            service.AddCar(new Car() { Make = "Audi", Model = "A6", MainPicture = new byte[] { } }).GetAwaiter().GetResult();

            Assert.True(data.Cars.Count() == 1);
        }

        [Fact]
        public void ApproveCarShouldReturnTrue()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car() { Id = Guid.NewGuid(), Make = "Audi", Model = "A6", MainPicture = new byte[] { }, Approved = false });
            data.SaveChanges();

            var service = new CarsService(data);

            var car = data.Cars.FirstOrDefault(x => x.Make == "Audi");

            service.ApproveCar(car.Id.ToString()).GetAwaiter().GetResult();

            Assert.True(car.Approved);
        }

        [Fact]
        public void GetCarByIdWithDetailsShouldReturnEquals()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Audi",
                Model = "A6",
                MainPicture = new byte[] { },
                Approved = false,
                Details = new CarDetails()
                {
                    Color = "Red",
                    FuelType = "Petrol",
                    GearboxType = "Auto",
                    Description = "asddsaf"
                }
            });

            data.SaveChanges();

            var service = new CarsService(data);

            var car = data.Cars.FirstOrDefault(x => x.Make == "Audi");

            var carDetails = service.GetCarByIdWithDetails(car.Id.ToString()).GetAwaiter().GetResult();

            Assert.True(car.Details != null);
        }


        [Fact]
        public void GetUnaprovedCarsShouldReturnTwoFromThreeCars()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car() { 
                Id = Guid.NewGuid(),
                Make = "Audi",
                Model = "A6",
                MainPicture = new byte[] { },
                Approved = false,
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "BMX",
                Model = "X3",
                MainPicture = new byte[] { },
                Approved = false,
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Mercedes",
                Model = "C-Class",
                MainPicture = new byte[] { },
                Approved = true,
            });
            data.SaveChanges();

            var service = new CarsService(data);

            var result = service.GetUnaprovedCars().GetAwaiter().GetResult();

            Assert.True(result.Count == 2);
            Assert.IsType<List<Car>>(result);
        }

        [Fact]
        public void ShowMyCarsShouldReturnListWithCars()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var randomId = Guid.NewGuid();

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Audi",
                Model = "A6",
                MainPicture = new byte[] { },
                Approved = false,
                SellerId = randomId
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "BMX",
                Model = "X3",
                MainPicture = new byte[] { },
                Approved = false,
                SellerId = randomId
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Mercedes",
                Model = "C-Class",
                MainPicture = new byte[] { },
                Approved = true,
            });
            data.SaveChanges();

            var service = new CarsService(data);

            var result = service.ShowMyCars(randomId.ToString()).GetAwaiter().GetResult();

            Assert.True(result.Count == 2);
            Assert.IsType<List<Car>>(result);
        }
    }
}
