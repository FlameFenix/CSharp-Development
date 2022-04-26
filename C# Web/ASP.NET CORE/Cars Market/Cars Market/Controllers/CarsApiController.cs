using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarsApiController : ControllerBase
    {
        private readonly ApplicationDbContext data;
        public CarsApiController(ApplicationDbContext _data)
        {
            data = _data;
        }

        [Route("GetCars")]
        [HttpGet]
        public ActionResult<ICollection<Car>> GetCars()
        {
            return data.Cars.ToList();
        }

        [Route("GetCarApiModel")]
        [HttpGet]
        public ActionResult<ICollection<CarApiModel>> GetCarsWithDescription()
        {
            return data.Cars.Select(x => new CarApiModel
            {
                CarId = x.Id.ToString(),
                Make = x.Make,
                Model = x.Model,
                Price = x.Money,
                Year = x.Year,
                Engine = x.Details.FuelType,
                Description = x.Details.Description,
                Picture = Convert.ToBase64String(x.MainPicture),
            }).ToList();
        }

        [Route("GetCar")]
        [HttpGet]
        public ActionResult<Car> GetCar(string carId)
        {
            var car = data.Cars.FirstOrDefault(x => x.Id.ToString() == carId);

            if(car == null) return NotFound();

            return car;
        }
    }
}
