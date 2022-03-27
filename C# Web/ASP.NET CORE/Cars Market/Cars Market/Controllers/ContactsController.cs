using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    public class ContactsController : Controller
    {
        private ApplicationDbContext data;

        public ContactsController(ApplicationDbContext _data)
        {
            data = _data;
        }
        public IActionResult Contacts() => View();

        [HttpPost]
        public async Task<IActionResult> Contacts(ContactFormModel contactForm)
        {

            var reciever = data.Sellers.FirstOrDefault(x => x.Email == "admin@carsmarket.com");

            var message = new Message()
            {
                SendFromEmail = contactForm.Sender,
                SendToEmail = "admin@carsmarket.com",
                SellerId = reciever.Id,
                Title = contactForm.Subject,
                Text = contactForm.Message
            };

            await data.Messages.AddAsync(message);
            await data.SaveChangesAsync();

            return View();
        }
    }
}
