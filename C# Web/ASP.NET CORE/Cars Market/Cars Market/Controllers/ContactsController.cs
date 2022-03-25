using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Contacts() => View();
    }
}
