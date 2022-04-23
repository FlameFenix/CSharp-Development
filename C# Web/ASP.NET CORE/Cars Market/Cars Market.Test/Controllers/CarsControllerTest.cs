//using Cars_Market.Controllers;
//using Cars_Market.Core.Services;
//using Cars_Market.Models;
//using Cars_Market.Test.Mocks;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace Cars_Market.Test.Controllers
//{
//    public class CarsControllerTest
//    {
//        [Fact]

//        public void CarsControllerAllCarsShoudReturnViewResult()
//        {
//            //Arange 
//            using var data = CarsMarketDbContextMock.Instance;
//            var carsService = new CarsService(data);
//            var carsController = new CarsController(data, null, carsService, null);

//            //Assert

//            var result = carsController.AllCars().GetAwaiter().GetResult();

//            //Act

//            Assert.NotNull(result);
//            Assert.IsType<ViewResult>(result);
//        }

//        [Fact]

//        public void CarsControllerAllCarsWithFilterOptionsShoudReturnViewResult()
//        {
//            //Arange 
//            using var data = CarsMarketDbContextMock.Instance;
//            var filteroptions = Mock.Of<FilterOptionsFormModel>();
//            var carsService = new CarsService(data);
//            var sellerService = new SellerService(data);

//            var carsController = new CarsController(data, null, carsService, sellerService);

//            //Assert

//            var result = carsController.AllCars(filteroptions).GetAwaiter().GetResult();


//            //Act

//            Assert.NotNull(result);
//            Assert.IsType<ViewResult>(result);
//        }
//    }
//}
