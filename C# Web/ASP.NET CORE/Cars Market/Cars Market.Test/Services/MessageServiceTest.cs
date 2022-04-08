using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Test.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
