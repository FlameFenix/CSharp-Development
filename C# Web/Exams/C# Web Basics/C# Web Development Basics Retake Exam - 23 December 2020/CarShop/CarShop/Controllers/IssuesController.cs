using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels;
using Microsoft.EntityFrameworkCore;
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
            var car = data.Cars.Include(x => x.Issues).FirstOrDefault(x => x.Id == carId);
            return View(car);
        }

        public HttpResponse Add()
        {
            return View();
        }

        [HttpPost]
        public HttpResponse Add(string carId, IssueViewModel model)
        {
            var car = data.Cars.Include(x => x.Issues).FirstOrDefault(x => x.Id == carId);

            var issue = new Issue()
            {
                CarId = carId,
                Description = model.Description,
                IsFixed = false
            };

            car.Issues.Add(issue);

            data.SaveChanges();

            return View("CarIssues", car);
        }

        public HttpResponse Fix(string carId, string issueId)
        {
            var user = data.Users.FirstOrDefault(x => x.Id == this.User.Id);

            var car = data.Cars.Include(x => x.Issues).FirstOrDefault(x => x.Id == carId);

            var issue = car.Issues.FirstOrDefault(x => x.Id == issueId);

            if (user.IsMechanic)
            {
               

                issue.IsFixed = true;

                data.SaveChanges();
            }

            return View("CarIssues", car);
        }

        public HttpResponse Delete(string carId, string issueId)
        {
            var user = data.Users.FirstOrDefault(x => x.Id == this.User.Id);

            var car = data.Cars.Include(x => x.Issues).FirstOrDefault(x => x.Id == carId);

            var issue = car.Issues.FirstOrDefault(x => x.Id == issueId);

            car.Issues.Remove(issue);

            data.SaveChanges();

            return View("CarIssues", car);
        }
    }
}
