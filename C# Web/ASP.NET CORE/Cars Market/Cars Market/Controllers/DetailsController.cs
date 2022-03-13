using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    public class DetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
