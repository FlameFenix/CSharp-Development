using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Test.Mocks;
using Moq;
using Xunit;

namespace Cars_Market.Test.Services
{
    public class SellerServiceTest
    {
        [Fact]
        public void AddSellerShouldAddSellerEntityToDatabase()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var seller = Mock.Of<Seller>(s => s.Email == "admin@carsmarket.com");

            service.AddSeller(seller).GetAwaiter().GetResult();

            Assert.True(data.Sellers.Count() == 1);
        }

        [Fact]
        public void AddSellerShouldThrowArgumentNulLException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            Seller seller = null;

            Assert.ThrowsAsync<ArgumentNullException>(() => service.AddSeller(seller));
        }


        [Fact]
        public void GetSellerByIdShouldSellerEntity()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var seller = Mock.Of<Seller>(s => s.Email == "admin@carsmarket.com");

            service.AddSeller(seller).GetAwaiter().GetResult();

            var expectedSeller = service.GetSellerById(seller.Id.ToString()).GetAwaiter().GetResult();

            Assert.NotNull(expectedSeller);
            Assert.True(seller.Id == expectedSeller.Id);
        }

        [Fact]
        public void GetSellerByIdShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var expectedSeller = service.GetSellerById(Guid.NewGuid().ToString());

            Assert.ThrowsAsync<ArgumentNullException>(() => expectedSeller);
        }

        [Fact]
        public void GetSellerByEmailShouldSellerWithCorrectEmail()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var seller = Mock.Of<Seller>(s => s.Email == "admin@carsmarket.com");

            service.AddSeller(seller).GetAwaiter().GetResult();

            var expectedSeller = service.GetSellerByEmail("admin@carsmarket.com").GetAwaiter().GetResult();

            Assert.NotNull(expectedSeller);
            Assert.True(expectedSeller.Email == "admin@carsmarket.com");
        }

        [Fact]
        public void GetSellerByEmailShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var expectedSeller = service.GetSellerByEmail("random@random.com");

            Assert.ThrowsAsync<ArgumentNullException>(() => expectedSeller);
        }

        [Fact]
        public void GetSellerWithMessagesShouldReturnSellerWithMessages()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var seller = Mock.Of<Seller>(s => s.Email == "admin@carsmarket.com");

            var messsages = Mock.Of<List<Message>>();

            var messageOne = new Message() { SendFromEmail = "admin", SendToEmail = seller.Email, Text = "Random Text asdasd", Title = "Random Title" };

            messsages.Add(messageOne);

            seller.Messages = messsages;

            service.AddSeller(seller).GetAwaiter().GetResult();

            var expectedSeller = service.GetSellerWithMessages(seller.Email).GetAwaiter().GetResult();

            Assert.NotNull(expectedSeller);
            Assert.True(expectedSeller.Messages.Count() == 1);
        }

        [Fact]
        public void GetSellerWithMessagesThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.GetSellerWithMessages(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void GetSellerWithProfileShouldReturnSellerWithProfile()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var seller = Mock.Of<Seller>(s => s.Email == "admin@carsmarket.com");

            var profile = new Profile() { Location = "random", Name = "random again", Phone = "random phone", Picture = new byte[] { } };

            seller.Profile = profile;

            service.AddSeller(seller).GetAwaiter().GetResult();

            var expectedSeller = service.GetSellerWithProfile(seller.Email).GetAwaiter().GetResult();

            Assert.NotNull(expectedSeller.Profile);
        }

        [Fact]
        public void GetSellerWithProfileShouldReturnSellerWithCorrectProfileInformation()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var seller = Mock.Of<Seller>(s => s.Email == "admin@carsmarket.com");

            var profile = new Profile() { Location = "random", Name = "random again", Phone = "random phone", Picture = new byte[] { } };

            seller.Profile = profile;

            service.AddSeller(seller).GetAwaiter().GetResult();

            var expectedSeller = service.GetSellerWithProfile(seller.Email).GetAwaiter().GetResult();

            Assert.NotNull(expectedSeller.Profile);
            Assert.True(expectedSeller.Profile.Name == profile.Name);
            Assert.True(expectedSeller.Profile.Phone == profile.Phone);
            Assert.True(expectedSeller.Profile.Location == profile.Location);
        }

        [Fact]
        public void GetSellerWithProfileShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var seller = Mock.Of<Seller>(s => s.Email == "admin@carsmarket.com");

            seller.Profile = null;

            service.AddSeller(seller).GetAwaiter().GetResult();

            Assert.ThrowsAsync<ArgumentNullException>(() => service.GetSellerWithProfile(seller.Email));
        }

        [Fact]
        public void ListOfAllSellersShouldReturnAllSellersInDatabase()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var sellerOne = Mock.Of<Seller>(s => s.Email == "admin@carsmarket.com");

            var sellerTwo = Mock.Of<Seller>(s => s.Email == "user@carsmarket.com");

            service.AddSeller(sellerOne).GetAwaiter().GetResult();
            service.AddSeller(sellerTwo).GetAwaiter().GetResult();

            var expectedSellersCollection = service.ListOfSellers().GetAwaiter().GetResult();

            Assert.NotEmpty(expectedSellersCollection);
            Assert.True(expectedSellersCollection.Count == 2);
        }

        [Fact]
        public void ListOfAllSellersShouldReturnEmptySellersCollection()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new SellerService(data);

            var expectedSellersCollection = service.ListOfSellers().GetAwaiter().GetResult();

            Assert.Empty(expectedSellersCollection);
            Assert.True(expectedSellersCollection.Count == 0);
        }
    }
}
