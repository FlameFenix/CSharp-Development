using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Constants;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly MessageService messageService;
        private readonly SellerService sellerService;
        public MessageController(SellerService _sellerService,
            MessageService _messageService)
        {
            sellerService = _sellerService; 
            messageService = _messageService;
        }

        
        public async Task<IActionResult> Inbox()
        {
            var currentUser = await sellerService.GetSellerWithMessages(User.Identity.Name);

            var unreadMessages = currentUser.Messages.Where(x => x.IsRead == false).Count();

            ViewBag.UnreadMessages = unreadMessages;

            return View(currentUser);
        }

        public async Task<IActionResult> Read(string messageId)
        {
            var message = await messageService.ReadMessage(messageId);

            if(message == null)
            {
                ViewBag.ErrorTitle = ErrorConstants.READ_MESSAGE_ERROR_TITLE;
                ViewBag.ErrorMessage = ErrorConstants.DELETE_OR_READ_MESSAGE_ERROR_MESSAGE;
                return View("Error");
            }

            return View(message);
        }

        public async Task<IActionResult> Delete(string messageId)
        {
            bool isDeleted = await messageService.RemoveMessage(messageId);

            if (!isDeleted)
            {
                ViewBag.ErrorTitle = ErrorConstants.DELETE_MESSAGE_ERROR_TITLE;
                ViewBag.ErrorMessage = ErrorConstants.DELETE_OR_READ_MESSAGE_ERROR_MESSAGE;
                return View("Error");
            }

            return RedirectToAction("Inbox");
        }

        public IActionResult Send() => View();

        [HttpPost]
        public async Task<IActionResult> Send(SendMessageFormView messageModel)
        {
            if (!ModelState.IsValid)
            {
                return View(messageModel);
            }

            await messageService.SendMessage(messageModel.Title, messageModel.Message, messageModel.RecieverEmail, User.Identity.Name);

            return Redirect("Inbox");
        }

    }
}
