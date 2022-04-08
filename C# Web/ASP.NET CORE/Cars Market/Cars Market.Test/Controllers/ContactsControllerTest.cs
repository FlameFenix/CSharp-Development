using Cars_Market.Controllers;
using Cars_Market.Core.Services;
using Cars_Market.Core.Services.Contracts;
using Cars_Market.Models;
using Cars_Market.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Cars_Market.Test.Controllers
{
    public class ContactsControllerTest
    {
        [Fact]

        public void ContactsControllerShouldReturnView()
        {
            using var data = CarsMarketDbContextMock.Instance;
            var service = new ContactsService(data);
            var controller = new ContactsController(data, service);

            var result = controller.Contacts().GetAwaiter().GetResult();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
