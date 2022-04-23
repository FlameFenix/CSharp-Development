using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Constants;
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
                ViewBag.ErrorTitle = ErrorConstants.SHOW_DETAILS_ERROR_TITLE;
                ViewBag.ErrorMessage = ErrorConstants.SHOW_DETAILS_ERROR_MESSAGE;
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
                ViewBag.ErrorTitle = ErrorConstants.COMMENT_CAR_WITH_NULL_ERROR_TITLE;
                ViewBag.ErrorMessage = ErrorConstants.COMMENT_CAR_WITH_NULL_ERROR_MESSAGE;
                return View("Error");
            }

            var commentText = commentModel.Comment;

            await detailsService.AddCommentToCar(carId, User.Identity.Name, commentText);

            return Redirect($"/Details/Details?carId={carId}");
        }
    }
}
