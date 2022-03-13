using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
