using Cars_Market.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cars_Market.Core.Services.Contracts
{
    public interface IMessageService
    {
        public Task SendMessage(string messageTitle, string messageText, string recieverEmail, string senderEmail);

        public Task<Message> ReadMessage(string messageId);

        public Task<bool> RemoveMessage(string messageId);

        public Task RemoveMessage(ICollection<Message> messages);

        public Task<ICollection<Message>> AllMessages();

        public Task<ICollection<Message>> ReadedMessages();

        public Task<ICollection<Message>> UnreadedMessages();
    }
}
