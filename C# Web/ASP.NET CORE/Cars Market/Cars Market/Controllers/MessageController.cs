﻿using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cars_Market.Controllers
{
    public class MessageController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly MessageService messageService;
        private readonly SellerService sellerService;
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
            var message = await messageService.ReadMessage(messageId);

            if(message == null)
            {
                ViewBag.ErrorTitle = "An error ocurred while trying to read message";
                ViewBag.ErrorMessage = "Message doesnt exists / or it was removed";
                return View("Error");
            }

            return View(message);
        }

        public async Task<IActionResult> Delete(string messageId)
        {
            bool isDeleted = await messageService.RemoveMessage(messageId);

            if (!isDeleted)
            {
                ViewBag.ErrorTitle = "An error ocurred while trying to delete message";
                ViewBag.ErrorMessage = "Message doesnt exists / or it was removed";
                return View("Error");
            }

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

            await messageService.SendMessage(messageModel.Title, messageModel.Message, messageModel.RecieverEmail, User.Identity.Name);

            return Redirect("Inbox");
        }

    }
}
