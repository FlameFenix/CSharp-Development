using Cars_Market.Core.Services;
using Cars_Market.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cars_Market.Test.Services
{
    public class ContactsServiceTest
    {
        [Fact]

        public void ContactUsShouldAddNewMessageToData()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ContactsService(data);

            service.ContactUs("Zdravei", "zdravei", "zdr@zdr.bg").GetAwaiter().GetResult();

            Assert.True(data.Messages.Count() == 1);
        }
    }
}
