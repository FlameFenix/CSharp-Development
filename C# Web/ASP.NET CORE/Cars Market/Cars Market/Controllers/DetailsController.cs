using Cars_Market.Core.Services;
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
            var carDetails = await detailsService.ReturnDetails(carId);
            await detailsService.CountCarVisits(carId);

            ViewBag.Car = carDetails;
            ViewBag.CarPictures = detailsService.GetCarPictures(carDetails);
            ViewBag.UserPicture = await detailsService.GetUserPictures(User.Identity.Name);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Details(string carId, AddCommentToCarFormModel commentModel)
        {

            if (string.IsNullOrWhiteSpace(commentModel.Comment))
            {
                return Redirect($"/Details/Details?carId={carId}");
            }

            var commentText = commentModel.Comment;

            await detailsService.AddCommentToCar(carId, User.Identity.Name, commentText);

            return Redirect($"/Details/Details?carId={carId}");
        }
    }
}
