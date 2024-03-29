﻿using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Test.Mocks;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace Cars_Market.Test.Services
{
    public class CarsServiceTest
    {
        private readonly ByteConverter converter;
        public CarsServiceTest()
        {
            converter = new ByteConverter();
        }

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

            var carsService = new CarsService(data, converter);

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

            var carsService = new CarsService(data, converter);

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

            var service = new CarsService(data, converter);

            var carAudi = data.Cars.FirstOrDefault(x => x.Make == "Audi");

            var serviceCar = service.GetCarById(carAudi.Id.ToString()).GetAwaiter().GetResult();

            Assert.Equal(carAudi, serviceCar);
        }

        [Fact]
        public void GetCarByIdShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new CarsService(data, converter);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.GetCarById(Guid.NewGuid().ToString()));
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

            var service = new CarsService(data, converter);

            var carAudi = data.Cars.FirstOrDefault(x => x.Make == "Audi");

            var serviceCar = service.RemoveCar(carAudi.Id.ToString());

            Assert.False(data.Cars.Contains(carAudi));
        }

        [Fact]
        public void DeleteCarByIdShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new CarsService(data, converter);

            var fakeCar = Mock.Of<Car>();

            Assert.ThrowsAsync<ArgumentNullException>(() => service.RemoveCar(fakeCar.Id.ToString()));
        }

        [Fact]
        public void ShouldReturnTrueAddCar()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new CarsService(data, converter);

            service.AddCar(new Car() { Make = "Audi", Model = "A6", MainPicture = new byte[] { } }).GetAwaiter().GetResult();

            var audiCar = data.Cars.FirstOrDefault();

            Assert.True(data.Cars.Count() == 1);
            Assert.True(audiCar.Model == "A6");
        }

        [Fact]
        public void AddCarShouldReturnThrow()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new CarsService(data, converter);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.AddCar(null));
        }

        [Fact]
        public void ApproveCarShouldReturnTrue()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car() { Id = Guid.NewGuid(), Make = "Audi", Model = "A6", MainPicture = new byte[] { }, Approved = false });
            data.SaveChanges();

            var service = new CarsService(data, converter);

            var car = data.Cars.FirstOrDefault(x => x.Make == "Audi");

            service.ApproveCar(car.Id.ToString()).GetAwaiter().GetResult();

            Assert.True(car.Approved);
        }

        [Fact]
        public void ApproveCarShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new CarsService(data, converter);

            var car = Mock.Of<Car>();

            Assert.ThrowsAsync<ArgumentNullException>(() => service.ApproveCar(car.Id.ToString()));
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

            var service = new CarsService(data, converter);

            var car = data.Cars.FirstOrDefault(x => x.Make == "Audi");

            var carDetails = service.GetCarByIdWithDetails(car.Id.ToString()).GetAwaiter().GetResult();

            Assert.True(car.Details != null);
        }

        [Fact]
        public void GetCarByIdWithDetailsShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new CarsService(data, converter);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.GetCarByIdWithDetails(Guid.NewGuid().ToString()));
        }


        [Fact]
        public void GetUnaprovedCarsShouldReturnTwoFromThreeCars()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car()
            {
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

            var service = new CarsService(data, converter);

            var result = service.GetUnaprovedCars().GetAwaiter().GetResult();

            Assert.True(result.Count == 2);
            Assert.IsType<List<Car>>(result);
        }

        [Fact]
        public void GetUnaprovedCarsShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new CarsService(data, converter);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.GetUnaprovedCars());
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

            var service = new CarsService(data, converter);

            var result = service.ShowMyCars(randomId.ToString()).GetAwaiter().GetResult();

            Assert.True(result.Count == 2);
            Assert.IsType<List<Car>>(result);
        }

        [Fact]
        public void ShowMyCarsShouldThrowArgumentNullExceptionThereAreNoCarsAvailable()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var randomId = Guid.NewGuid();

            var service = new CarsService(data, converter);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.ShowMyCars(randomId.ToString()));
        }

        [Fact]
        public void CheckCarOwnerShouldReturnFalseWhenUserIsNotOwner()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var randomId = Guid.NewGuid().ToString();

            var service = new CarsService(data, converter);

            Assert.False(service.CheckCarOwner(randomId, "random@random.com").GetAwaiter().GetResult());
        }

        [Fact]
        public void CheckCarOwnerShouldReturnTrueWhenUserIsOwner()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var seller = Mock.Of<Seller>(s => s.Email == "admin@carsmarket.com");

            data.Sellers.Add(seller);

            var car = new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Audi",
                Model = "A6",
                MainPicture = new byte[] { },
                Approved = false,
                SellerId = seller.Id
            };

            seller.Cars.Add(car);

            var randomId = Guid.NewGuid().ToString();

            var service = new CarsService(data, converter);

            Assert.False(service.CheckCarOwner(car.Id.ToString(), "admin@carsmarket.com").GetAwaiter().GetResult());
        }

        [Fact]
        public void ShowOrderedCarsShouldReturnListWithCarsOrderedAscending()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "BMW",
                Model = "X3",
                MainPicture = new byte[] { },
                Approved = true,
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Audi",
                Model = "A3",
                MainPicture = new byte[] { },
                Approved = true,
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Audi",
                Model = "A6",
                MainPicture = new byte[] { },
                Approved = true,
            });

            data.SaveChanges();

            var service = new CarsService(data, converter);

            var result = service.ShowOrderedCars("Make", "Model", "Ascending").GetAwaiter().GetResult().ToList();

            var audiA3 = result.FirstOrDefault();
            var bmwX3 = result.LastOrDefault();

            Assert.IsType<List<Car>>(result);
            Assert.True(result.Count == 3);
            Assert.True(audiA3.Model == "A3");
            Assert.True(bmwX3.Model == "X3");
        }

        [Fact]
        public void ShowOrderedCarsShouldReturnListWithCarsOrderedDescending()
        {
            using var data = CarsMarketDbContextMock.Instance;

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "BMW",
                Model = "X3",
                MainPicture = new byte[] { },
                Approved = true,
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Audi",
                Model = "A3",
                MainPicture = new byte[] { },
                Approved = true,
            });

            data.Cars.Add(new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Audi",
                Model = "A6",
                MainPicture = new byte[] { },
                Approved = true,
            });

            data.SaveChanges();

            var service = new CarsService(data, converter);

            var result = service.ShowOrderedCars("Make", "Model", "Descending").GetAwaiter().GetResult().ToList();

            var bmwX3 = result.FirstOrDefault();
            var audiA6 = result.LastOrDefault();
            

            Assert.IsType<List<Car>>(result);
            Assert.True(result.Count == 3);
            Assert.True(bmwX3.Model == "X3");
            Assert.True(audiA6.Model == "A6");
        }
    }
}
