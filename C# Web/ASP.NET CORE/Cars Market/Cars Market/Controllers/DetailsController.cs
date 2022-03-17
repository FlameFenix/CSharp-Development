using Cars_Market.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
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
            ViewBag.Comments = data.Comments.Where(x => x.CarId.ToString() == carId).ToList();

            var sellerEmail = data.Sellers.Where(x => x.Id == car.SellerId).Select(x => x.Email).FirstOrDefault();
            var userPicture = data.Sellers.Where(x => x.Email == User.Identity.Name).Select(x => x.Profile.Picture).FirstOrDefault();

            ViewBag.CarOwner = sellerEmail;
            ViewBag.UserPicture = userPicture;

            return View();
        }

        [HttpPost]
        public IActionResult Details(string carId, AddCommentToCarFormModel commentModel)
        {
            var car = data.Cars.FirstOrDefault(x => x.Id.ToString() == carId);

            var user = data.Sellers.Include(x => x.Profile).FirstOrDefault(x => x.Email == User.Identity.Name);

            Comment comment = new Comment()
            {
                AuthorName = user.Profile.Name,
                AuthorPicture = user.Profile.Picture,
                Text = commentModel.Comment,
                CarId = car.Id
            };

            data.Comments.Add(comment);
            data.SaveChanges();

            return Redirect("/AllCars");
        }
    }
}
