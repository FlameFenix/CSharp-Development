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
                return Redirect($"/");
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
                return Redirect($"/Details/Details?carId={carId}");
            }

            var commentText = commentModel.Comment;

            await detailsService.AddCommentToCar(carId, User.Identity.Name, commentText);

            return Redirect($"/Details/Details?carId={carId}");
        }
    }
}
