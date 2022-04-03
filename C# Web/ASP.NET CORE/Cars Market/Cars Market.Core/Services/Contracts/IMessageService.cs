using Cars_Market.Infrastructure.Data.Models;

namespace Cars_Market.Core.Services.Contracts
{
    public interface IMessageService
    {
        public Task SendMessage(Message message);

        public Task<bool> RemoveMessage(string messageId);

        public Task RemoveMessage(ICollection<Message> messages);

        public Task<ICollection<Message>> AllMessages();

        public Task<ICollection<Message>> ReadedMessages();

        public Task<ICollection<Message>> UnreadedMessages();
    }
}
