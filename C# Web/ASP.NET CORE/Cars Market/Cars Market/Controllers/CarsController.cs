using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    public class CarsController : Controller
    {
        public IActionResult AddCar()
        {
            return View();
        }

        public IActionResult EditCar()
        {
            return View();
        }

        public IActionResult MyCars()
        {
            return View();
        }
    }
}
