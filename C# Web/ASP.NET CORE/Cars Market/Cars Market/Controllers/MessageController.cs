using Cars_Market.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext data;
        public MessageController(ApplicationDbContext _data)
        {
            data = _data;
        }
        public IActionResult Inbox()
        {
            var currentUser = data.Sellers.Include(x => x.Messages).Where(x => x.Email == User.Identity.Name).FirstOrDefault();

            return View(currentUser);
        }

        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(SendMessageFormView messageModel)
        {
            var sender = await data.Sellers.FirstOrDefaultAsync(x => x.Email == User.Identity.Name);

            var reciever = await data.Sellers.FirstOrDefaultAsync(x => x.Email == messageModel.RecieverEmail);

            if (sender == null || reciever == null)
            {
                return BadRequest();
            }

            Message message = new Message()
            {
                SendFromEmail = sender.Email,
                SendToEmail = reciever.Email,
                SellerId = reciever.Id,
                Text = messageModel.Message,
                Title = messageModel.Title,

            };

            await data.Messages.AddAsync(message);

            await data.SaveChangesAsync();

            return Redirect("Inbox");
        }

    }
}
