using Cars_Market.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

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
        [Route("Get")]
        [HttpGet]
        public IActionResult GetCars()
        {
            return Ok(data.Cars.ToList());

        }
    }
}
