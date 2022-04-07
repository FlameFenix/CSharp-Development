using Cars_Market.Core.Services.Contracts;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext data;

        public MessageService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task<Message> ReadMessage(string messageId)
        {
            var message = await data.Messages.Where(x => x.Id.ToString() == messageId).FirstOrDefaultAsync();

            message.IsRead = true;

            await data.SaveChangesAsync();

            return message;
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

        public async Task SendMessage(string messageTitle, string messageText, string recieverEmail, string senderEmail)
        {
            var reciever = await data.Sellers.FirstOrDefaultAsync(x => x.Email == recieverEmail);

            Message message = new()
            {
                SendFromEmail = senderEmail,
                SendToEmail = reciever.Email,
                SellerId = reciever.Id,
                Text = messageText,
                Title = messageTitle,
                IsRead = false
            };

            data.Messages.Add(message);

            await data.SaveChangesAsync();
        }

        public async Task<ICollection<Message>> UnreadedMessages()
        {
            return await data.Messages.Where(x => x.IsRead == false).ToListAsync();
        }
    }
}
