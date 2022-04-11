using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;

namespace Cars_Market.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsService carsService;
        private readonly SellerService sellerService;
        private readonly ApplicationDbContext data;
        private readonly ByteConverter converter;
        public CarsController(
            ApplicationDbContext _data,
            ByteConverter _converter,
            CarsService _carsService,
            SellerService _sellerService)
        {
            data = _data;
            converter = _converter;
            carsService = _carsService;
            sellerService = _sellerService;
        }

        [Authorize(Roles = "User")]
        public IActionResult AddCar() => View();

        [Authorize(Roles = "User")]
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

            var carDetails = new CarDetails()
            {
                Color = carModel.Color,
                Description = carModel.Description,
                GearboxType = carModel.GearboxType,
                FuelType = carModel.FuelType,
                Visits = 0,
                IsSold = false
            };


            var car = new Car()
            {
                Id = carModel.Id,
                Make = carModel.Make,
                Model = carModel.Model,
                Money = double.Parse(carModel.Money, CultureInfo.InvariantCulture),
                Year = int.Parse(carModel.Year),
                MainPicture = converter.ConvertToByteArray(carModel.Image),
                SellerId = seller.Id,
                Details = carDetails
            };

            if (carModel.Images != null)
            {
                var images = new List<CarPicture>();

                foreach (var picture in carModel.Images)
                {
                    var carPicture = new CarPicture()
                    {
                        CarId = car.Id,
                        Picture = converter.ConvertToByteArray(picture)
                    };

                    images.Add(carPicture);
                }

                car.Pictures = images;


            }

            await carsService.AddCar(car);

            return RedirectToAction("MyCars");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(string carId)
        {
            var isOwner = await carsService.CheckCarOwner(carId, User.Identity.Name);
            
            if(isOwner == false)
            {
                return Redirect("/");
            }

            var car = await carsService.GetCarByIdWithDetails(carId);
            ViewBag.Car = car;
            ViewBag.Description = car.Details.Description;
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Edit(string carId, EditCarFormModel editModel)
        {
            if (ModelState.IsValid)
            {
                var car = await carsService.GetCarByIdWithDetails(carId);

                    car.Make = editModel.Make;
                    car.Model = editModel.Model;
                    car.Year = int.Parse(editModel.Year);
                    car.Money = double.Parse(editModel.Money, CultureInfo.InvariantCulture);
                    car.Details.FuelType = editModel.FuelType;
                    car.Details.Description = editModel.Description;
                    car.Details.GearboxType = editModel.GearboxType;

                    await data.SaveChangesAsync();
            }

            return RedirectToAction("MyCars");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(string carId)
        {
            var isOwner = await carsService.CheckCarOwner(carId, User.Identity.Name);

            if (isOwner == false)
            {
                return Redirect("/");
            }

            await carsService.RemoveCar(carId);

            return RedirectToAction("MyCars");

        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyCars()
        {
            var seller = await sellerService.GetSellerByEmail(User.Identity.Name);

            ViewBag.Cars = await carsService.ShowMyCars(seller.Id.ToString());

            return View();
        }

        [Authorize(Roles = "User")]
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

        
        public async Task<IActionResult> AllCars()
        {
            ViewBag.CarsList = await carsService.ShowAllCars();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AllCars(FilterOptionsFormModel filterOptions)
        {
            ViewBag.CarsList = await carsService.ShowOrderedCars(filterOptions.SortByType, filterOptions.SortBySecondType, filterOptions.OrderByType);

            return View();
        }

        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> ModeratorMenu()
        {
            ViewBag.CarsList = await carsService.GetUnaprovedCars();

            return View();
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public async Task<IActionResult> ModeratorMenu(FilterOptionsFormModel filterOptions)
        {
            ViewBag.CarsList = await carsService.GetUnaprovedCarsOrdered(filterOptions.SortByType, filterOptions.SortBySecondType, filterOptions.OrderByType);

            return View();
        }


        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> ApproveCar(string carId)
        {
            await carsService.ApproveCar(carId);

            return RedirectToAction("ModeratorMenu");
        }

    }
}
