using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly ContactsService contactsService;

        public ContactsController(ApplicationDbContext _data, ContactsService _contactsService)
        {
            data = _data;
            contactsService = _contactsService;
        }
        public async Task<IActionResult> Contacts()
        {

            ViewBag.OwnerProfileId = await data.Sellers.Where(x => x.Email == "admin@carsmarket.com")
                                                       .Select(x => x.Profile.Id.ToString())
                                                       .FirstOrDefaultAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contacts(ContactFormModel contactForm)
        {

            if (!ModelState.IsValid)
            {
                return View(contactForm);
            }

            await contactsService.ContactUs(contactForm.Subject, contactForm.Message, contactForm.Sender);

            return RedirectToAction("Contacts");
        }
    }
}
