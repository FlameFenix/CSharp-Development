using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
