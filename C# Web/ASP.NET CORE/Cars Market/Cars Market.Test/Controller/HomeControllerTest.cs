using Cars_Market.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cars_Market.Test.Controller
{
    public  class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            //Arange 
            var homeController = new HomeController(null, null, null);

            //Assert

            var result = homeController.Index();

            //Act

            Assert.NotNull(result);
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        public void PrivacyShouldReturnView()
        {
            //Arange 
            var homeController = new HomeController(null, null, null);

            //Assert

            var result = homeController.Privacy();

            //Act

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
