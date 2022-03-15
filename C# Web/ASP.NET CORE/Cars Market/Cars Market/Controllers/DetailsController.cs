using Cars_Market.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Controllers
{
    public class DetailsController : Controller
    {
        private ApplicationDbContext data;
        public DetailsController(ApplicationDbContext _data)
        {
            data = _data;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(string carId)
        {
            var car = data.Cars.Include(x => x.Details).FirstOrDefault(x => x.Id.ToString() == carId);

            var carDetails = data.CarDetails.FirstOrDefault(x => car.Id == x.CarId);

            carDetails.Visits++;

            data.SaveChanges();

            ViewBag.Car = car;
            ViewBag.Details = carDetails;

            var sellerEmail = data.Sellers.Where(x => x.Id == car.SellerId).Select(x => x.Email).FirstOrDefault();

            ViewBag.CarOwner = sellerEmail;

            return View();
        }
    }
}
