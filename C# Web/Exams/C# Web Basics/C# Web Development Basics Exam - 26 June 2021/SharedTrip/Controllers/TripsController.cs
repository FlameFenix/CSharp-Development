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

            var errors = validator.AddTripValidation(model);

            if (errors.Any())
            {
                return Redirect("Add");
            }

            string strFormat = "dd.MM.yyyy HH:mm";

            DateTime.TryParseExact(model.DepartureTime,strFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);

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

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var trip = data.Trips.FirstOrDefault(x => x.Id == tripId);

            return View(trip);
        }

        [Authorize]

        public HttpResponse AddUserToTrip(string tripId)
        {

            var user = data.Users.FirstOrDefault(x => x.Id == this.User.Id);

            var trip = data.Trips.FirstOrDefault(x => x.Id == tripId);



            trip.Seats = trip.Seats - 1;

            UserTrip userTrip = new UserTrip()
            {
                TripId = tripId,
                UserId = this.User.Id,
            };

            if (data.UserTrips.Contains(userTrip))
            {
                return Redirect($"/Trips/Details?tripId={tripId}");
            }

            data.UserTrips.Add(userTrip);

            data.SaveChanges();

            return Redirect("/");
        }
    }
}
