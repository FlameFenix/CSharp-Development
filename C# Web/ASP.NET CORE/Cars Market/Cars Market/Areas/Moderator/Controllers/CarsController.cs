using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;

namespace Cars_Market.Areas.Moderator.Controllers
{
    public class CarsController : ModeratorController
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

        public async Task<IActionResult> Edit(string carId)
        {
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
                ViewBag.ErrorTitle = "An error ocurred while trying to edit car and details";
                ViewBag.ErrorMessage = "All fields are required!";
                return View("Error");
            }

            var car = await carsService.GetCarByIdWithDetails(carId);

            car.Make = editModel.Make;
            car.Model = editModel.Model;
            car.Year = int.Parse(editModel.Year);
            car.Money = double.Parse(editModel.Money, CultureInfo.InvariantCulture);
            car.Details.FuelType = editModel.FuelType;
            car.Details.Description = editModel.Description;
            car.Details.GearboxType = editModel.GearboxType;

            await data.SaveChangesAsync();

            return RedirectToAction("MyCars");
        }

        public async Task<IActionResult> Delete(string carId)
        {

            await carsService.RemoveCar(carId);

            return RedirectToAction("MyCars");

        }

        public async Task<IActionResult> ModeratorMenu()
        {
            ViewBag.CarsList = await carsService.GetUnaprovedCars();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ModeratorMenu(FilterOptionsFormModel filterOptions)
        {
            ViewBag.CarsList = await carsService.GetUnaprovedCarsOrdered(filterOptions.SortByType, filterOptions.SortBySecondType, filterOptions.OrderByType);

            return View();
        }

        public async Task<IActionResult> ApproveCar(string carId)
        {
            await carsService.ApproveCar(carId);

            return RedirectToAction("ModeratorMenu");
        }

    }
}
