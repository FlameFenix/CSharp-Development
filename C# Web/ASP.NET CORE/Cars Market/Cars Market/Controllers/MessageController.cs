using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Controllers
{
    public class MessageController : Controller
    {
        private ApplicationDbContext data;
        private SellerService sellerService;
        public MessageController(ApplicationDbContext _data, SellerService _sellerService)
        {
            data = _data;
            sellerService = _sellerService; 
        }
        public async Task<IActionResult> Inbox()
        {
            var currentUser = await sellerService.GetSellerWithMessages(User.Identity.Name);

            return View(currentUser);
        }

        public IActionResult Send() => View();

        [HttpPost]
        public async Task<IActionResult> Send(SendMessageFormView messageModel)
        {
            var sender = await sellerService.GetSellerByEmail(User.Identity.Name);

            var reciever = await sellerService.GetSellerByEmail(messageModel.RecieverEmail);

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
