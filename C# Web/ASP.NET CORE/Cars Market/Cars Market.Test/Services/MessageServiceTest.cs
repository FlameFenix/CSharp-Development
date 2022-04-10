using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Test.Mocks;
using Moq;
using Xunit;

namespace Cars_Market.Test.Services
{
    public class MessageServiceTest
    {
        [Fact]

        public void AllMessagesShouldReturnCorrectFormatAndCount()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new MessageService(data);

            var result = service.AllMessages().GetAwaiter().GetResult();

            Assert.IsType<List<Message>>(result);
            Assert.True(result.Count == 0);
        }

        [Fact]

        public void AllMessagesShouldReturnCorrectCountWhenAddMessage()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var message = new Message()
            {
                Id = Guid.NewGuid(),
                IsRead = false,
                SellerId = Guid.NewGuid(),
                SendFromEmail = "admin@carsmarket.com",
                SendToEmail = "user@carsmarket.com",
                Text = "zdr ko pr",
                Title = "leko povurhnosten message :P"
            };

            data.Messages.Add(message);
            data.SaveChanges();

            var service = new MessageService(data);

            var result = service.AllMessages().GetAwaiter().GetResult();

            Assert.IsType<List<Message>>(result);
            Assert.True(result.Count == 1);
        }

        [Fact]

        public void RemoveMessageShouldReturnFalse()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var service = new MessageService(data);

            var result = service.RemoveMessage(Guid.NewGuid().ToString()).GetAwaiter().GetResult();

            Assert.False(result);
        }

        [Fact]

        public void RemoveMessageShouldReturnTrue()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var message = new Message()
            {
                Id = Guid.NewGuid(),
                IsRead = false,
                SellerId = Guid.NewGuid(),
                SendFromEmail = "admin@carsmarket.com",
                SendToEmail = "user@carsmarket.com",
                Text = "zdr ko pr",
                Title = "leko povurhnosten message :P"
            };

            data.Messages.Add(message);
            data.SaveChanges();

            var service = new MessageService(data);

            var result = service.RemoveMessage(message.Id.ToString()).GetAwaiter().GetResult();

            Assert.True(result);
        }

        [Fact]

        public void UnreadedMessagesShouldReturnTrueCount()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var message = new Message()
            {
                Id = Guid.NewGuid(),
                IsRead = false,
                SellerId = Guid.NewGuid(),
                SendFromEmail = "admin@carsmarket.com",
                SendToEmail = "user@carsmarket.com",
                Text = "zdr ko pr",
                Title = "leko povurhnosten message :P"
            };

            var anotherMessage = new Message()
            {
                Id = Guid.NewGuid(),
                IsRead = false,
                SellerId = Guid.NewGuid(),
                SendFromEmail = "user@carsmarket.com",
                SendToEmail = "admin@carsmarket.com",
                Text = "zdr ko pr",
                Title = "leko povurhnosten message :P"
            };
            data.Messages.Add(message);
            data.Messages.Add(anotherMessage);
            data.SaveChanges();

            var service = new MessageService(data);

            var result = service.UnreadedMessages().GetAwaiter().GetResult();

            Assert.NotNull(result);
            Assert.True(result.Count == 2);
            Assert.IsType<List<Message>>(result);
        }

        [Fact]
        public void UnreadedMessagesShouldReturnTrueCountSecond()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var message = new Message()
            {
                Id = Guid.NewGuid(),
                IsRead = true,
                SellerId = Guid.NewGuid(),
                SendFromEmail = "admin@carsmarket.com",
                SendToEmail = "user@carsmarket.com",
                Text = "zdr ko pr",
                Title = "leko povurhnosten message :P"
            };

            var anotherMessage = new Message()
            {
                Id = Guid.NewGuid(),
                IsRead = true,
                SellerId = Guid.NewGuid(),
                SendFromEmail = "user@carsmarket.com",
                SendToEmail = "admin@carsmarket.com",
                Text = "zdr ko pr",
                Title = "leko povurhnosten message :P"
            };
            data.Messages.Add(message);
            data.Messages.Add(anotherMessage);
            data.SaveChanges();

            var service = new MessageService(data);

            var result = service.UnreadedMessages().GetAwaiter().GetResult();
            Assert.True(result.Count == 0);
            Assert.IsType<List<Message>>(result);
        }

        [Fact]
        public void ReadedMessagesShouldReturnTrueCountSecond()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var message = new Message()
            {
                Id = Guid.NewGuid(),
                IsRead = true,
                SellerId = Guid.NewGuid(),
                SendFromEmail = "admin@carsmarket.com",
                SendToEmail = "user@carsmarket.com",
                Text = "zdr ko pr",
                Title = "leko povurhnosten message :P"
            };

            var anotherMessage = new Message()
            {
                Id = Guid.NewGuid(),
                IsRead = true,
                SellerId = Guid.NewGuid(),
                SendFromEmail = "user@carsmarket.com",
                SendToEmail = "admin@carsmarket.com",
                Text = "zdr ko pr",
                Title = "leko povurhnosten message :P"
            };
            data.Messages.Add(message);
            data.Messages.Add(anotherMessage);
            data.SaveChanges();

            var service = new MessageService(data);

            var result = service.ReadedMessages().GetAwaiter().GetResult();
            Assert.True(result.Count == 2);
            Assert.IsType<List<Message>>(result);
        }

        [Fact]
        public void ReadedMessagesShouldReturnTrueCount()
        {
            using var data = CarsMarketDbContextMock.Instance;

            var message = new Message()
            {
                Id = Guid.NewGuid(),
                IsRead = false,
                SellerId = Guid.NewGuid(),
                SendFromEmail = "admin@carsmarket.com",
                SendToEmail = "user@carsmarket.com",
                Text = "zdr ko pr",
                Title = "leko povurhnosten message :P"
            };

            var anotherMessage = new Message()
            {
                Id = Guid.NewGuid(),
                IsRead = false,
                SellerId = Guid.NewGuid(),
                SendFromEmail = "user@carsmarket.com",
                SendToEmail = "admin@carsmarket.com",
                Text = "zdr ko pr",
                Title = "leko povurhnosten message :P"
            };
            data.Messages.Add(message);
            data.Messages.Add(anotherMessage);
            data.SaveChanges();

            var service = new MessageService(data);

            var result = service.ReadedMessages().GetAwaiter().GetResult();

            Assert.True(result.Count == 0);
            Assert.IsType<List<Message>>(result);
        }

        [Fact]
        public void SendMessageShouldReturnTrueCount()
        {
            var data = CarsMarketDbContextMock.Instance;

            var firstUser = new Seller
            {
                Email = "admin@carsmarket.com"
            };
            var secondUser = new Seller()
            {
                Email = "user@carsmarket.com"
            };

            data.Sellers.Add(firstUser);
            data.Sellers.Add(secondUser);
            data.SaveChanges();

            var service = new MessageService(data);

            service.SendMessage("Hello World!", "I'm Yordan Dimitrov", firstUser.Email, secondUser.Email).GetAwaiter().GetResult();

            Assert.True(data.Messages.Count() == 1);
        }

        [Fact]
        public void SendMessageShouldThrowNullException()
        {
            var data = CarsMarketDbContextMock.Instance;

            var secondUser = new Seller()
            {
                Email = "user@carsmarket.com"
            };

            data.Sellers.Add(secondUser);
            data.SaveChanges();

            var service = new MessageService(data);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.SendMessage("Hello World!", "I'm Yordan Dimitrov", null, secondUser.Email));
        }

        [Fact]
        public void RemoveMessageShouldRemoveWholeCollectionWithMessages()
        {
            var data = CarsMarketDbContextMock.Instance;

            var service = new MessageService(data);

            var secondUser = Mock.Of<Seller>(x => x.Email == "user@carsmarket.com");

            var messages = new List<Message>()
            {
               new Message() { Title = Guid.NewGuid().ToString(), Text = Guid.NewGuid().ToString(), SendFromEmail = Guid.NewGuid().ToString(), SendToEmail = "user@carsmarket.com" },
               new Message() { Title = Guid.NewGuid().ToString(), Text = Guid.NewGuid().ToString(), SendFromEmail = Guid.NewGuid().ToString(), SendToEmail = "user@carsmarket.com" },
               new Message() { Title = Guid.NewGuid().ToString(), Text = Guid.NewGuid().ToString(), SendFromEmail = Guid.NewGuid().ToString(), SendToEmail = "user@carsmarket.com" }
            };

            secondUser.Messages = messages;

            data.Sellers.Add(secondUser);
            data.SaveChanges();

            Assert.NotEmpty(secondUser.Messages);

            service.RemoveMessage(secondUser.Messages).GetAwaiter().GetResult();

            Assert.Empty(secondUser.Messages);
        }

        [Fact]
        public void RemoveMessageShouldThrowArgumentNullException()
        {
            var data = CarsMarketDbContextMock.Instance;

            var service = new MessageService(data);

            var secondUser = Mock.Of<Seller>(x => x.Email == "user@carsmarket.com");

            data.Sellers.Add(secondUser);
            data.SaveChanges();

            Assert.ThrowsAsync<ArgumentNullException>(() => service.RemoveMessage(secondUser.Messages));
        }

        [Fact]
        public void ReadMessageShouldReturnMessageEntity()
        {
            var data = CarsMarketDbContextMock.Instance;

            var service = new MessageService(data);

            var message = new Message()
            {
                Title = "Title",
                Text = "Text",
                SendFromEmail = "senderEmail",
                SendToEmail = "sendFromEmail"
            };

            data.Messages.Add(message);

            data.SaveChanges();

            var expectedMessage = service.ReadMessage(message.Id.ToString()).GetAwaiter().GetResult();

            Assert.IsType<Message>(expectedMessage);
        }

        [Fact]
        public void ReadMessageShouldThrowArgumentNullException()
        {
            var data = CarsMarketDbContextMock.Instance;

            var service = new MessageService(data);

            Assert.ThrowsAsync<ArgumentNullException>(() => service.ReadMessage(Guid.NewGuid().ToString()));
        }
    }
}
