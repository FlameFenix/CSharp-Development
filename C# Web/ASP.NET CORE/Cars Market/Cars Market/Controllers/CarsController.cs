using Cars_Market.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Cars_Market.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Cars_Market.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext data;
        private ByteConverter converter;
        public CarsController(ApplicationDbContext _data, ByteConverter _converter)
        {
            data = _data;
            converter = _converter;
        }

        [Authorize]
        public IActionResult AddCar()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddCar(AddCarFormModel carModel)
        {
            var seller = data.Sellers.FirstOrDefault(x => x.Email == User.Identity.Name);

            if(seller == null)
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

            

            data.Cars.Add(car);

            data.SaveChanges();

            return Redirect("MyCars");
        }

        [Authorize]
        public IActionResult Edit(string carId)
        {
            var car = data.Cars.FirstOrDefault(x => x.Id.ToString() == carId);

            ViewBag.Car = car;

            return View();
        }

        [Authorize]
        public IActionResult Delete(string carId)
		{
            var car = data.Cars.FirstOrDefault(x => x.Id.ToString() == carId);

            data.Cars.Remove(car);

            data.SaveChanges();

            return Redirect("MyCars");
        }

        [Authorize]
        public IActionResult MyCars()
        {
            var seller = data.Sellers.FirstOrDefault(x => x.Email == User.Identity.Name);

            var cars = data.Cars.Where(x => x.SellerId == seller.Id).ToList();

            return View(cars);
        }

        public IActionResult AllCars()
        {
            var cars = data.Cars.ToList();

            return View(cars);
        }
    }
}
