using Cars_Market.Core.Services.Contracts;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
namespace Cars_Market.Core.Services
{
    public class ContactsService : IContactsService
    {
        private readonly ApplicationDbContext data;
        public ContactsService(ApplicationDbContext _data)
        {
            data = _data;
        }
        public async Task ContactUs(string messageTitle, string messageText, string senderEmail)
        {
            var recieverId = await data.Sellers.Where(x => x.Email == "admin@carsmarket.com").Select(x => x.Id).FirstOrDefaultAsync();

            var message = new Message()
            {
                SendFromEmail = senderEmail,
                SendToEmail = "admin@carsmarket.com",
                SellerId = recieverId,
                Title = messageTitle,
                Text = messageText
            };

            await data.Messages.AddAsync(message);
            await data.SaveChangesAsync();

        }
    }
}
