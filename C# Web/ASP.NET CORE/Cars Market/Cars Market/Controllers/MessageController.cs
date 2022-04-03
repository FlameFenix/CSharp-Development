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
        private readonly MessageService messageService;
        private SellerService sellerService;
        public MessageController(ApplicationDbContext _data,
            SellerService _sellerService,
            MessageService _messageService)
        {
            data = _data;
            sellerService = _sellerService; 
            messageService = _messageService;
        }

        [Authorize]
        public async Task<IActionResult> Inbox()
        {
            var currentUser = await sellerService.GetSellerWithMessages(User.Identity.Name);

            var unreadMessages = currentUser.Messages.Where(x => x.IsRead == false).Count();

            ViewBag.UnreadMessages = unreadMessages;

            return View(currentUser);
        }

        public async Task<IActionResult> Read(string messageId)
        {
            var message = await data.Messages.FirstOrDefaultAsync(x => x.Id.ToString() == messageId);

            if(message != null)
            {
                message.IsRead = true;

                await data.SaveChangesAsync();
            }

            return View(message);
        }

        public async Task<IActionResult> Delete(string messageId)
        {
            bool isDeleted = await messageService.RemoveMessage(messageId);

            if (!isDeleted)
            {
                return RedirectToAction("Inbox");
            }

            return RedirectToAction("Inbox");
        }

        [Authorize]
        public IActionResult Send() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Send(SendMessageFormView messageModel)
        {
            var sender = await sellerService.GetSellerByEmail(User.Identity.Name);

            var reciever = await sellerService.GetSellerByEmail(messageModel.RecieverEmail);

            if (sender == null || reciever == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(messageModel);
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

            await messageService.SendMessage(message);

            return Redirect("Inbox");
        }

    }
}
