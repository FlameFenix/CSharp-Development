using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    public class DetailsController : Controller
    {
        private readonly DetailsService detailsService;

        public DetailsController(DetailsService _detailsService)
        {
            detailsService = _detailsService;
        }
        public IActionResult Index() => View();

        public async Task<IActionResult> Details(string carId)
        {
            Car car = null;

            try
            {
                 car = await detailsService.ReturnDetails(carId);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorTitle = "An error ocurred while trying to see car details";
                ViewBag.ErrorMessage = "The resource you are looking for (or one of its dependencies) could have been removed," +
                " had its name changed, or is temporarily unavailable." +
                " Please review the following URL and make sure that it is spelled correctly.";
                return View("Error");
            }

            await detailsService.CountCarVisits(carId);

            ViewBag.Car = car;
            ViewBag.CarPictures = detailsService.GetCarPictures(car);
            ViewBag.UserPicture = await detailsService.GetUserPictures(User.Identity.Name);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(string carId, AddCommentToCarFormModel commentModel)
        {

            if (string.IsNullOrWhiteSpace(commentModel.Comment))
            {
                ViewBag.ErrorTitle = "An error ocurred while trying to post comment to car";
                ViewBag.ErrorMessage = "You are trying to send empty message!";
                return View("Error");
            }

            var commentText = commentModel.Comment;

            await detailsService.AddCommentToCar(carId, User.Identity.Name, commentText);

            return Redirect($"/Details/Details?carId={carId}");
        }
    }
}
