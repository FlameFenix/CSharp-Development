using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly CarsService carsService;
        private readonly SellerService sellerService;
        public DetailsController(ApplicationDbContext _data,
            CarsService _carsService,
        SellerService _sellerService)
        {
            data = _data;
            carsService = _carsService;
            sellerService = _sellerService;
        }
        public IActionResult Index() => View();

        public async Task<IActionResult> Details(string carId)
        {
            var car = await data.Cars.Include(x => x.Pictures).FirstOrDefaultAsync(x => x.Id.ToString() == carId);
            var carDetails = await data.CarDetails.FirstOrDefaultAsync(x => car.Id == x.CarId);

            carDetails.Visits++;

            data.SaveChanges();

            ViewBag.Car = car;
            ViewBag.Details = carDetails;
            ViewBag.Comments = await data.Comments.Where(x => x.CarId.ToString() == carId).ToListAsync();

            var sellerInfo = await data.Sellers.Where(x => x.Id == car.SellerId).Select(x => new
            {
                Name = x.Profile.Name,
                Email = x.Email,
                Location = x.Profile.Location,
                Phone = x.Profile.Phone
            }).FirstOrDefaultAsync();

            var userPicture = await data.Sellers.Where(x => x.Email == User.Identity.Name).Select(x => x.Profile.Picture).FirstOrDefaultAsync();

            var pictures = car.Pictures.Select(x => Convert.ToBase64String(x.Picture)).ToList();

            ViewBag.CarOwner = sellerInfo;
            ViewBag.UserPicture = userPicture;
            ViewBag.CarPictures = pictures;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(string carId, AddCommentToCarFormModel commentModel)
        {
            var car = await carsService.GetCarById(carId);

            var user = await sellerService.GetSellerWithProfile(User.Identity.Name);

            Comment comment = new Comment()
            {
                AuthorName = user.Profile.Name,
                AuthorPicture = user.Profile.Picture,
                Text = commentModel.Comment,
                CarId = car.Id
            };

            data.Comments.Add(comment);

            data.SaveChanges();

            return Redirect($"/Details/Details?carId={carId}");
        }
    }
}
