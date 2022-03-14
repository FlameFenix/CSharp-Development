using Cars_Market.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext data;
        public MessageController(ApplicationDbContext _data)
        {
            data = _data;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Send(SendMessageFormView messageModel)
        {
            var sender = data.Sellers.FirstOrDefault(x => x.Email == User.Identity.Name);

            var reciever = data.Sellers.FirstOrDefault(x => x.Email == messageModel.RecieverEmail);

            if(sender == null || reciever == null)
            {
                return BadRequest();
            }

            Message message = new Message()
            {
                SendFromEmail = sender.Email,
                SendToEmail = reciever.Email,
                Text = messageModel.Message,
                Title = messageModel.Title
            };

            reciever.Messages.Add(message);

            data.SaveChanges();

            return View();
        }

    }
}
