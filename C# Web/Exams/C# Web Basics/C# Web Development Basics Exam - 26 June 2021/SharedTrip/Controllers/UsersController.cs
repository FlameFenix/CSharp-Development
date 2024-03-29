﻿using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.Services.HashPassword;
using SharedTrip.Services.Validator;
using SharedTrip.ViewModels;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private Validator validator;
        private ApplicationDbContext data;
        private HashPassword hashPassword;
        public UsersController(Validator _validator, ApplicationDbContext _data, HashPassword _hashPassword) 
        {
            validator = _validator;
            data = _data;
            hashPassword = _hashPassword;
        }

        public HttpResponse Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginViewModel model)
        {
            var user = data.Users.FirstOrDefault(x => x.Username == model.Username);

            if (user == null)
            {
                return Redirect("Login");
            }

            if(user.Password != hashPassword.PasswordHasher(model.Password))
            {
                return Redirect("Login");
            }

            SignIn(user.Id);

            return Redirect("/");
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
            var errors = validator.RegisterValidation(model);

            var userExists = data.Users.FirstOrDefault(x => x.Username == model.Username);

            if(userExists != null)
            {
                errors.Add("Username already exists");
            }

            if (errors.Any())
            {
                return Redirect("Register");
            }

            var user = new User()
            {
                Username = model.Username,
                Password = hashPassword.PasswordHasher(model.Password),
                Email = model.Email,
            };

            data.Users.Add(user);
            data.SaveChanges();

            return Redirect("Login");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();
            return Redirect("/");
        }
    }

}
