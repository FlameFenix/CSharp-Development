using Cars_Market.Controllers;
using Cars_Market.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cars_Market.Test.Controllers
{
    public  class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            using var data = CarsMarketDbContextMock.Instance;
            //Arange 
            var homeController = new HomeController(null, data, null);

            //Assert

            var result = homeController.Index().GetAwaiter().GetResult();

            //Act

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
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
