using Cars_Market.Controllers;
using Cars_Market.Models;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Cars_Market.Test.Controller
{
    public class CarsControllersTest
    {
        [Test]

        public void AddCarShouldReturnView()
        {
            //Arange 
            var carsController = new CarsController(null, null, null, null);

            //Assert

            var result = carsController.AddCar();

            //Act

            Assert.NotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]

        public void AddCarShouldReturnTask()
        {
            //Arange 
            var carsController = new CarsController(null, null, null, null);

            //Assert

            var result = carsController.AddCar(new AddCarFormModel());

            //Act

            Assert.NotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }

        [Test]

        public void DeleteCarShouldReturnTask()
        {
            //Arange 
            var carsController = new CarsController(null, null, null, null);

            //Assert

            var result = carsController.Delete(Guid.NewGuid().ToString());

            //Act

            Assert.NotNull(result);
            Assert.IsInstanceOf<Task<IActionResult>>(result);
        }
    }
}
