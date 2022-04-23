using Cars_Market.Controllers;
using Cars_Market.Core.Services;
using Cars_Market.Models;
using Cars_Market.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Cars_Market.Test.Controllers
{
    public class CarsControllerTest
    {
        private readonly ByteConverter converter;
        public CarsControllerTest()
        {
            converter = new ByteConverter();
        }
        [Fact]

        public void CarsControllerAllCarsShoudReturnViewResult()
        {
            //Arange 
            using var data = CarsMarketDbContextMock.Instance;
            var carsService = new CarsService(data, converter);
            var sellerService = new SellerService(data);
            var carsController = new CarsController(carsService, sellerService, null);

            //Assert

            var result = carsController.AllCars().GetAwaiter().GetResult();

            //Act

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]

        public void CarsControllerAllCarsWithFilterOptionsShoudReturnViewResult()
        {
            //Arange 
            using var data = CarsMarketDbContextMock.Instance;
            var filteroptions = Mock.Of<FilterOptionsFormModel>();
            var carsService = new CarsService(data, converter);
            var sellerService = new SellerService(data);

            var carsController = new CarsController(carsService, sellerService, null);

            //Assert

            var result = carsController.AllCars(filteroptions).GetAwaiter().GetResult();


            //Act

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
