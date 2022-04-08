using Cars_Market.Core.Services;
using Cars_Market.Test.Mocks;
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

        [Fact]

        public void ContactUsShouldThrowExceptionEmptyContactForm()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ContactsService(data);

            Assert.ThrowsAsync<Exception>(() => service.ContactUs(null, "zdravei", "zdr@zdr.bg"));
            Assert.ThrowsAsync<Exception>(() => service.ContactUs("zdr", null, "zdr@zdr.bg"));
            Assert.ThrowsAsync<Exception>(() => service.ContactUs("hello", "zdravei", null));
        }
    }
}
