using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.Services.Validator;
using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private ApplicationDbContext data;
        private Validator validator;
        public TripsController(ApplicationDbContext _data,
                                Validator _validator)
        {
            data = _data;
            validator = _validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            ICollection<Trip> trips = data.Trips.ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(TripViewModel model)
        {

            DateTime.TryParse(model.DepartureTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

            var trip = new Trip()
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = result,
                ImagePath = model.ImagePath,
                Description = model.Description,
                Seats = model.Seats,
            };

            data.Trips.Add(trip);

            data.SaveChanges();

            return Redirect("All");
        }
    }
}
