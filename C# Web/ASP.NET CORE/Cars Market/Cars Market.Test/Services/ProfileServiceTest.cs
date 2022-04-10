using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Test.Mocks;
using Moq;
using Xunit;

namespace Cars_Market.Test.Services
{
    public class ProfileServiceTest
    {
        [Fact]

        public void AddProfileEntityShouldReturnDataWithOneProfileEntity()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ProfileService(data);

            var profile = Mock.Of<Profile>();
            profile.Name = Guid.NewGuid().ToString();
            profile.Phone = Guid.NewGuid().ToString();
            profile.Location = Guid.NewGuid().ToString();
            profile.Picture = new byte[] { };

            data.Profiles.Add(profile);
            data.SaveChanges();

            Assert.NotEmpty(data.Profiles);
            Assert.True(data.Profiles.Count() == 1);
        }

        [Fact]

        public void GetProfileByIdShouldReturnProfileEntity()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ProfileService(data);

            var profile = new Profile()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Phone = Guid.NewGuid().ToString(),
                Location = Guid.NewGuid().ToString(),
                Picture = new byte[] { }
            };

            data.Profiles.Add(profile);
            data.SaveChanges();

            var expectedProfile = service.GetProfileById(profile.Id.ToString()).GetAwaiter().GetResult();

            Assert.IsType<Profile>(expectedProfile);
        }

        [Fact]
        public void GetProfileByIdShouldReturnCorrectProfileInformation()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ProfileService(data);

            var profile = new Profile()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Phone = Guid.NewGuid().ToString(),
                Location = Guid.NewGuid().ToString(),
                Picture = new byte[] { }
            };

            data.Profiles.Add(profile);
            data.SaveChanges();

            var expectedProfile = service.GetProfileById(profile.Id.ToString()).GetAwaiter().GetResult();

            Assert.True(profile.Id == expectedProfile.Id);
            Assert.True(profile.Name == expectedProfile.Name);
            Assert.True(profile.Phone == expectedProfile.Phone);
            Assert.True(profile.Location == expectedProfile.Location);
            Assert.True(profile.Picture == expectedProfile.Picture);
        }

        [Fact]
        public void GetProfileByIdShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ProfileService(data);

            var expectedProfile = service.GetProfileById(Guid.NewGuid().ToString());

            Assert.ThrowsAsync<ArgumentNullException>(() => expectedProfile);
        }

        [Fact]

        public void GetProfileByEmailShouldReturnProfileEntity()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ProfileService(data);

            var profile = new Profile()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Phone = Guid.NewGuid().ToString(),
                Location = Guid.NewGuid().ToString(),
                Picture = new byte[] { }
            };

            var seller = new Seller()
            {
                Email = Guid.NewGuid().ToString()
            };

            seller.Profile = profile;

            data.Sellers.Add(seller);
            data.Profiles.Add(profile);
            data.SaveChanges();

            var expectedProfile = service.GetProfileByEmail(seller.Email.ToString()).GetAwaiter().GetResult();

            Assert.IsType<Profile>(expectedProfile);
        }

        [Fact]
        public void GetProfileByEmailShouldReturnCorrectProfileInformation()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ProfileService(data);

            var profile = new Profile()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Phone = Guid.NewGuid().ToString(),
                Location = Guid.NewGuid().ToString(),
                Picture = new byte[] { }
            };

            var seller = new Seller()
            {
                Email = Guid.NewGuid().ToString()
            };

            seller.Profile = profile;

            data.Sellers.Add(seller);
            data.Profiles.Add(profile);
            data.SaveChanges();

            var expectedProfile = service.GetProfileByEmail(seller.Email).GetAwaiter().GetResult();

            Assert.True(profile.Id == expectedProfile.Id);
            Assert.True(profile.Name == expectedProfile.Name);
            Assert.True(profile.Phone == expectedProfile.Phone);
            Assert.True(profile.Location == expectedProfile.Location);
            Assert.True(profile.Picture == expectedProfile.Picture);
        }

        [Fact]
        public void GetProfileByEmailShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ProfileService(data);

            var expectedProfile = service.GetProfileByEmail(Guid.NewGuid().ToString());

            Assert.ThrowsAsync<ArgumentNullException>(() => expectedProfile);
        }

        [Fact]

        public void GetProfileEmailShouldReturnStringResult()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ProfileService(data);

            var profile = new Profile()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Phone = Guid.NewGuid().ToString(),
                Location = Guid.NewGuid().ToString(),
                Picture = new byte[] { }
            };

            var seller = new Seller()
            {
                Email = Guid.NewGuid().ToString()
            };

            seller.Profile = profile;

            data.Sellers.Add(seller);
            data.Profiles.Add(profile);
            data.SaveChanges();

            var expectedEmail = service.GetProfileEmail(profile.Id.ToString()).GetAwaiter().GetResult();

            Assert.IsType<string>(expectedEmail);
        }

        [Fact]
        public void GetProfileEmailShouldReturnCorrectEmail()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ProfileService(data);

            var profile = new Profile()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Phone = Guid.NewGuid().ToString(),
                Location = Guid.NewGuid().ToString(),
                Picture = new byte[] { }
            };

            var seller = new Seller()
            {
                Email = Guid.NewGuid().ToString()
            };

            seller.Profile = profile;

            data.Sellers.Add(seller);
            data.Profiles.Add(profile);
            data.SaveChanges();

            var expectedEmail = service.GetProfileEmail(profile.Id.ToString()).GetAwaiter().GetResult();

            Assert.True(expectedEmail == seller.Email);
        }

        [Fact]
        public void GetProfileEmailShouldThrowArgumentNullException()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new ProfileService(data);

            var expectedEmail = service.GetProfileEmail(Guid.NewGuid().ToString()).GetAwaiter().GetResult();

            Assert.True(string.IsNullOrEmpty(expectedEmail));
        }
    }
}
