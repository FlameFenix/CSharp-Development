using CarShop.Data;
using CarShop.Data.Models;
using CarShop.Services.HashPassword;
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
    public class UsersController : Controller
    {
        private Validator validator;
        private HashPassword passwordHasher;
        private ApplicationDbContext data;

        public UsersController(Validator _validator,
            HashPassword _passwordHasher,
            ApplicationDbContext _data)
        {
            validator = _validator;
            passwordHasher = _passwordHasher;
            data = _data;
        }
        public HttpResponse Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

        public HttpResponse Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterViewModel model)
        {    
            var errors = validator.ValidateRegistration(model);

            if (errors.Any())
            {
                Redirect("Register");
            }

            User user = new User()
            {
                Email = model.Email,
                Password = passwordHasher.PasswordHasher(model.Password),
                Username = model.Username,
                IsMechanic = model.userType == "Client" ? true : false,
            };

            data.Users.Add(user);
            data.SaveChanges();

            return Redirect("/");
        }

        [HttpPost]
        public HttpResponse Login(LoginViewModel model)
        {
            var user = data.Users.FirstOrDefault(x => x.Username == model.Username);

            if(user == null ||
                user.Password != passwordHasher.PasswordHasher(model.Password))
            {
                return Redirect("Login");   
            }

            SignIn(user.Id);

            return Redirect("/");
        }

        public HttpResponse Logout()
        {
            SignOut();
            return Redirect("/");
        }
    }
}
