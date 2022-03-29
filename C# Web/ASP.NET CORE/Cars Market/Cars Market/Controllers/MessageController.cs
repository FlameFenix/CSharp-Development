using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public async Task<IActionResult> Inbox()
        {
            var currentUser = await sellerService.GetSellerWithMessages(User.Identity.Name);

            return View(currentUser);
        }

        public async Task<IActionResult> Read(string messageId)
        {
            var message = await data.Messages.FirstOrDefaultAsync(x => x.Id.ToString() == messageId);

            return View(message);
        }

        public async Task<IActionResult> Delete(string messageId)
        {
            var message = await data.Messages.FirstOrDefaultAsync(x => x.Id.ToString() == messageId);

            if(message == null)
            {
                return RedirectToAction("Inbox");
            }

            data.Messages.Remove(message);

            await data.SaveChangesAsync();

            return RedirectToAction("Inbox");
        }

        [Authorize]
        public IActionResult Send() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Send(SendMessageFormView messageModel)
        {

            if (!ModelState.IsValid)
            {
                return View(messageModel);
            }

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
                IsRead = false
            };

            await data.Messages.AddAsync(message);

            await data.SaveChangesAsync();

            return Redirect("Inbox");
        }

    }
}
