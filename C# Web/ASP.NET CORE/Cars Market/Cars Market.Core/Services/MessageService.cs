using Cars_Market.Core.Services.Contracts;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Core.Services
{
    public class MessageService : IMessageService
    {
        private ApplicationDbContext data;

        public MessageService(ApplicationDbContext _data)
        {
            data = _data;
        }
        public async Task<ICollection<Message>> AllMessages()
        {
            return await data.Messages.ToListAsync();
        }

        public async Task<ICollection<Message>> ReadedMessages()
        {
            return await data.Messages.Where(x => x.IsRead == true).ToListAsync();
        }

        public async Task<bool> RemoveMessage(string messageId)
        {
            var message = await data.Messages.FirstOrDefaultAsync(x => x.Id.ToString() == messageId);

            if (message == null)
            {
                return false;
            }

            data.Messages.Remove(message);
            await data.SaveChangesAsync();

            return true;
        }

        public async Task RemoveMessage(ICollection<Message> messages)
        {
            data.Messages.RemoveRange(messages);
            await data.SaveChangesAsync();
        }

        public async Task SendMessage(Message message)
        {
            data.Messages.Add(message);
            await data.SaveChangesAsync();
        }

        public async Task<ICollection<Message>> UnreadedMessages()
        {
            return await data.Messages.Where(x => x.IsRead == false).ToListAsync();
        }
    }
}
