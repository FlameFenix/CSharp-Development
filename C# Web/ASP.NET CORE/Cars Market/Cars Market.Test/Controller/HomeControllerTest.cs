using Cars_Market.Controllers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Cars_Market.Test.Controller
{
	public class HomeControllerTest
	{
        [Test]
        public void IndexShouldReturnView()
        {
            //Arange 
            var homeController = new HomeController(null);

            //Assert

            var result = homeController.Index();

            //Act

            Assert.NotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void PrivacyShouldReturnView()
        {
            //Arange 
            var homeController = new HomeController(null);

            //Assert

            var result = homeController.Privacy();

            //Act

            Assert.NotNull(result);
            Assert.IsInstanceOf<IActionResult>(result);
        }
    }
}
