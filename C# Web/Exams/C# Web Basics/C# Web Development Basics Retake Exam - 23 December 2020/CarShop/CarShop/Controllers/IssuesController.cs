using CarShop.Data;
using CarShop.Data.Models;
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
    
    public class IssuesController : Controller
    {
        private ApplicationDbContext data;
        public IssuesController(ApplicationDbContext _data)
        {
            data = _data;
        }
        public HttpResponse CarIssues(string carId)
        {
            var car = data.Cars.FirstOrDefault(x => x.Id == carId);

            return View(car);
        }

        public HttpResponse Add()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Add(string carId, IssueViewModel model)
        {
            var car = data.Cars.FirstOrDefault(x => x.Id == carId);

            var issue = new Issue()
            {
                CarId = carId,
                Description = model.Description,
                IsFixed = false
            };

            car.Issues.Add(issue);

            data.SaveChanges();

            return Redirect("CarIssues");
        }
    }
}
