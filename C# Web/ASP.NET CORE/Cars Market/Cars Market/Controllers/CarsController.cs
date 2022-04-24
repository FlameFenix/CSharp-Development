using AutoMapper;
using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Constants;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    [Authorize(Roles = "User")]
    public class CarsController : Controller
    {
        private readonly CarsService carsService;
        private readonly SellerService sellerService;
        private readonly IMapper mapper;
        public CarsController(CarsService _carsService,
                              SellerService _sellerService,
                              IMapper _mapper)
        {
            carsService = _carsService;
            sellerService = _sellerService;
            mapper = _mapper;
        }

        public IActionResult AddCar() => View();

        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarFormModel carModel)
        {
            var seller = await sellerService.GetSellerByEmail(User.Identity.Name);

            if (seller == null)
            {
                return Redirect("/Seller/AddSeller");
            }

            if (!ModelState.IsValid)
            {
                return View(carModel);
            }

            var carDetails = mapper.Map<AddCarFormModel, CarDetails>(carModel);

            var car = mapper.Map<AddCarFormModel, Car>(carModel);
            car.Details = carDetails;
            car.Seller = seller;
            car.MainPicture = carsService.GetMainPicture(carModel.Image);

            if (carModel.Images != null)
            {
                var images = carsService.GetCarPictures(carModel.Images, car.Id);

                car.Pictures = images;
            }

            await carsService.AddCar(car);

            return RedirectToAction("MyCars");
        }

        public async Task<IActionResult> Edit(string carId)
        {
            var isOwner = await carsService.CheckCarOwner(carId, User.Identity.Name);

            if (isOwner == false)
            {
                ViewBag.ErrorTitle = ErrorConstants.EDIT_CAR_ERROR_TITLE;
                ViewBag.ErrorMessage = ErrorConstants.EDIT_CAR_OWNER_ERROR_MESSAGE;
                return View("Error");
            }

            var car = await carsService.GetCarByIdWithDetails(carId);
            ViewBag.Car = car;
            ViewBag.Description = car.Details.Description;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string carId, EditCarFormModel editModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorTitle = ErrorConstants.EDIT_CAR_ERROR_TITLE;
                ViewBag.ErrorMessage = ErrorConstants.EDIT_FORM_REQUIRED_FIELDS_ERROR_MESSAGE;
                return View("Error");
            }

            var newCar = mapper.Map<EditCarFormModel, Car>(editModel);
            var newDetails = mapper.Map<EditCarFormModel, CarDetails>(editModel);

            await carsService.UpdateCar(carId, newCar, newDetails);

            return RedirectToAction("MyCars");
        }

        public async Task<IActionResult> Delete(string carId)
        {
            var isOwner = await carsService.CheckCarOwner(carId, User.Identity.Name);

            if (isOwner == false)
            {
                ViewBag.ErrorTitle = ErrorConstants.DELETE_CAR_ERROR_TITLE;
                ViewBag.ErrorMessage = ErrorConstants.DELETE_CAR_ERROR_MESSAGE;
                return View("Error");
            }

            await carsService.RemoveCar(carId);

            return RedirectToAction("MyCars");

        }

        public async Task<IActionResult> MyCars()
        {
            var seller = await sellerService.GetSellerByEmail(User.Identity.Name);

            ViewBag.Cars = await carsService.ShowMyCars(seller.Id.ToString());

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> MyCars(FilterOptionsFormModel filterModel)
        {
            var seller = await sellerService.GetSellerByEmail(User.Identity.Name);

            ViewBag.Cars = await carsService.ShowMyCarsOrdered(seller.Id.ToString(),
                                                               filterModel.SortByType,
                                                               filterModel.SortBySecondType,
                                                               filterModel.OrderByType);
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> AllCars()
        {
            ViewBag.CarsList = await carsService.ShowAllCars();

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AllCars(FilterOptionsFormModel filterOptions)
        {
            ViewBag.CarsList = await carsService.ShowOrderedCars(filterOptions.SortByType,
                                                                 filterOptions.SortBySecondType,
                                                                 filterOptions.OrderByType);

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Search() =>  View();
    }
}
