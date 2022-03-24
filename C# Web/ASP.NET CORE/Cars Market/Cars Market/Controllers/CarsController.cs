using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Cars_Market.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsService carsService;
        private readonly SellerService sellerService;
        private ApplicationDbContext data;
        private ByteConverter converter;
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

        [Authorize]
        public IActionResult AddCar()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarFormModel carModel)
        {
            var seller = await sellerService.GetSellerByEmail(User.Identity.Name);

            if (seller == null)
            {
                return Redirect("/Seller/AddSeller");
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
                Picture = converter.ConvertToByteArray(carModel.Image),
                SellerId = seller.Id,
                Details = carDetails
            };

            await carsService.AddCar(car);

            return Redirect("MyCars");
        }

        [Authorize(Roles = "Seller")]
        public IActionResult Edit(string carId)
        {
            var car = carsService.GetCarById(carId);

            ViewBag.Car = car;

            return BadRequest();
        }

        [Authorize]
        public async Task<IActionResult> Delete(string carId)
        {
            await carsService.RemoveCar(carId);

            return Redirect("MyCars");

        }

        [Authorize]
        public async Task<IActionResult> MyCars()
        {
            var seller = await sellerService.GetSellerByEmail(User.Identity.Name);

            var cars = await carsService.ShowMyCars(seller.Id.ToString());

            return View(cars);
        }

        public async Task<IActionResult> AllCars()
        {
            var cars = await carsService.ShowAllCars();

            return View(cars);
        }
    }
}
