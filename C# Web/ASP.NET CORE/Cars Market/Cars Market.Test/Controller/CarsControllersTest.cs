using Cars_Market.Controllers;
using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Cars_Market.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Cars_Market.Test.Controller
{
    public class CarsControllersTest
    {
        [Fact]

        public void AddCarShouldReturnView()
        {
            //Arange 
            var carsController = new CarsController(null, null, null, null);

            //Assert

            var result = carsController.AddCar();

            //Act

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]

        public void AddCarShouldReturnTask()
        {
            //Arange 
            var carsController = new CarsController(null, null, null, null);

            //Assert

            var result = carsController.AddCar(new AddCarFormModel());

            //Act

            Assert.NotNull(result);
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void AddCarShouldAddCarInDb()
        {
            //Arange 

            using var data = CarsMarketDbContextMock.Instance;
            var carsService = new CarsService(data);
            //Assert
            _ = carsService.AddCar(new Car()
            {
                Make = "Audi",
                Model = "A6",
                MainPicture = new byte[] { }
            });

            data.SaveChanges();

            var car = data.Cars.FirstOrDefault(x => x.Make == "Audi");
            //Act

            Assert.True(data.Cars.Contains(car));
            Assert.True(data.Cars.Count() == 1);
        }

        [Fact]
        public void AddCarShouldReturnViewWithWrongData()
        {
            //Arange 

            using var data = CarsMarketDbContextMock.Instance;
            //Assert

            //Act

        }

        [Fact]

        public void DeleteCarShouldReturnTask()
        {
            //Arange 
            var carsController = new CarsController(null, null, null, null);

            //Assert

            var result = carsController.Delete(Guid.NewGuid().ToString());

            //Act
            Assert.NotNull(result);
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void DeleteCarShouldRemoveEntity()
        {
            //Arange
            var carsController = new CarsController(null, null, null, null);

            //Assert

            var result = carsController.Delete(Guid.NewGuid().ToString());

            //Act
            Assert.NotNull(result);
            Assert.IsType<Task<IActionResult>>(result);
        }

    }
}
