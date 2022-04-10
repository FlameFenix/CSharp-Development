using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Test.Mocks;
using Moq;
using Xunit;

namespace Cars_Market.Test.Services
{
    public class DetailsServiceTest
    {
        [Fact]
        public void ReturnDetailsShouldReturnCarEntityWithAllDetails()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var fakeCar = new Car()
            {
                Make = "Audi",
                Model = "A6",
                MainPicture = new byte[] { }

            };

            var fakeDetails = new CarDetails()
            {
                Color = "Red",
                Description = "asd",
                FuelType = "Petrol",
                GearboxType = "auto"
            };

            fakeCar.Details = fakeDetails;

            data.Cars.Add(fakeCar);
            data.SaveChanges();

            var service = new DetailsService(data);

            var car = service.ReturnDetails(fakeCar.Id.ToString()).GetAwaiter().GetResult();

            Assert.True(car != null);
            Assert.True(car.Details != null);
        }

        [Fact]
        public void ReturnDetailsShouldThrowNullArgumentExceptionCarDoesntExists()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new DetailsService(data);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.ReturnDetails(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void CountVisitsShouldReturnCarEntityWithIncreasedVisits()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var fakeCar = new Car();

            var fakeDetails = new CarDetails();

            fakeDetails.Color = "Red";
            fakeDetails.Description = "asd";
            fakeDetails.FuelType = "Petrol";
            fakeDetails.GearboxType = "auto";

            fakeCar.Make = "Audi";
            fakeCar.Model = "A6";
            fakeCar.MainPicture = new byte[] { };
            fakeCar.Details = fakeDetails;

            data.Cars.Add(fakeCar);
            data.SaveChanges();

            var service = new DetailsService(data);

            service.CountCarVisits(fakeCar.Id.ToString()).GetAwaiter().GetResult();
            service.CountCarVisits(fakeCar.Id.ToString()).GetAwaiter().GetResult();
            service.CountCarVisits(fakeCar.Id.ToString()).GetAwaiter().GetResult();

            Assert.True(fakeCar.Details.Visits == 3);
        }

        [Fact]
        public void CountVisitsShouldThrowArgumentNullExceptionCarDoesntExists()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var fakeCar = Mock.Of<Car>();

            var service = new DetailsService(data);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.CountCarVisits(fakeCar.Id.ToString()));
        }

        [Fact]
        public void GetCarPicturesShouldReturnListPicturesAsString()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var fakeCar = new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Honda",
                Model = "Civic",
                MainPicture = new byte[] { },
                Pictures = new List<CarPicture>() 
                { 
                    new CarPicture() { Picture = new byte[] { }, Id = Guid.NewGuid() },
                    new CarPicture() { Picture = new byte[] { }, Id = Guid.NewGuid() },
                    new CarPicture() { Picture = new byte[] { }, Id = Guid.NewGuid() }
                }
            };

            var fakeDetails = new CarDetails()
            {
                Color = "Silver",
                Description = "Begachka",
                FuelType = "Petrol",
                GearboxType = "Manual"
            };

            fakeCar.Details = fakeDetails;

            data.Cars.Add(fakeCar);
            data.SaveChanges();

            var service = new DetailsService(data);

            var carPictures = service.GetCarPictures(fakeCar);

            Assert.IsType<List<string>>(carPictures);
            Assert.True(carPictures.Count == 3);
        }

        [Fact]
        public void GetCarPicturesShouldThrowArgumentNullExceptionBecauseCarDoesntExists()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new DetailsService(data);

            var fakeCar = Mock.Of<Car>();

            Assert.Throws<ArgumentNullException>(() => service.GetCarPictures(fakeCar));

        }

        [Fact]
        public void AddCommentToCarShouldReturnTrueCountOfComments()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var fakeCar = new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Honda",
                Model = "Civic",
                MainPicture = new byte[] { },
                Pictures = new List<CarPicture>()
                {
                    new CarPicture() { Picture = new byte[] { }, Id = Guid.NewGuid() },
                    new CarPicture() { Picture = new byte[] { }, Id = Guid.NewGuid() },
                    new CarPicture() { Picture = new byte[] { }, Id = Guid.NewGuid() }
                }, 
                Comments = new List<Comment>()
            };

            var fakeDetails = new CarDetails()
            {
                Color = "Silver",
                Description = "Begachka",
                FuelType = "Petrol",
                GearboxType = "Manual"
            };

            fakeCar.Details = fakeDetails;

            data.Sellers.Add(new Seller() 
            { Email = "admin@carsmarket.com",
                Profile = new Profile() 
                { 
                    Name = "Admin",
                    Picture = new byte[] { },
                    Location  = "Karlovo",
                    Phone = "0000"
                } 
            });
            data.Cars.Add(fakeCar);
            data.SaveChanges();

            var service = new DetailsService(data);

            service.AddCommentToCar(fakeCar.Id.ToString(), "admin@carsmarket.com", "top kola").GetAwaiter().GetResult();

            Assert.True(fakeCar.Comments.Count == 1);
        }

        [Fact]
        public void AddCommentToCarShouldNOTAddCommentBecauseSenderDoesntExists()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var fakeCar = new Car()
            {
                Id = Guid.NewGuid(),
                Make = "Honda",
                Model = "Civic",
                MainPicture = new byte[] { },
                Comments = new List<Comment>()
            };

            data.Cars.Add(fakeCar);
            data.SaveChanges();

            var service = new DetailsService(data);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.AddCommentToCar(fakeCar.Id.ToString(), null, "top kola"));
            Assert.ThrowsAsync<ArgumentNullException>(() => service.AddCommentToCar(null, "admin@carsmarket.com", "top kola"));
        }

        [Fact]
        public void GetUserPictureShouldReturnPicturesAsString()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var user = Mock.Of<Seller>(x => x.Email == "admin@carsmarket.com");

            var profile = new Profile()
            {
                Location = "random location",
                Name = "random name",
                Phone = "random phone",
                Picture = Guid.NewGuid().ToByteArray()
            };

            user.Profile = profile;
            data.Sellers.Add(user);

            data.SaveChanges();

            var service = new DetailsService(data);

            var userPicture = service.GetUserPictures("admin@carsmarket.com").GetAwaiter().GetResult();

            Assert.NotNull(userPicture);
            Assert.NotEmpty(userPicture);
            Assert.IsType<string>(userPicture);
        }

        [Fact]
        public void GetUserPictureShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var user = Mock.Of<Seller>(x => x.Email == "admin@carsmarket.com");

            data.Sellers.Add(user);

            data.SaveChanges();

            var service = new DetailsService(data);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.GetUserPictures("admin@carsmarket.com"));
        }
    }
}
