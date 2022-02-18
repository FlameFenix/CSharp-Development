using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Services.Validator;
using CarShop.ViewModels;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private ApplicationDbContext data;
        private Validator validator;
        public CarsController(ApplicationDbContext _data,
            Validator _validator)
        {
            data = _data;
            validator = _validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var allCars = data.Cars.ToList();

            return View(allCars);
        }

        [Authorize]
        public HttpResponse Add()
        {
            var user = data.Users.FirstOrDefault(x => x.Id == this.User.Id);

            if (user.IsMechanic)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Add(CarViewModel model)
        {
            var errors = validator.ValidateCarAdding(model);

            var carExists = data.Cars.FirstOrDefault(x => model.PlateNumber == x.PlateNumber);

            if (errors.Any() || carExists != null)
            {
                return Redirect("Add");
            }

            Car car = new Car()
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = this.User.Id,
            };

            data.Cars.Add(car);
            data.SaveChanges();


            return Redirect("/");
        }
    }
}
