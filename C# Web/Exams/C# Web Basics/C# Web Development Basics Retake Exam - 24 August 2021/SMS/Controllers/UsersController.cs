using MyWebServer.Controllers;
using MyWebServer.Http;
using SMS.Data;
using SMS.Data.Models;
using SMS.Models;
using SMS.Services;
using System.Collections.Generic;
using System.Linq;

namespace SMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly Validator validator;
        private readonly SMSDbContext data;
        private readonly PasswordHasher hasher;
        public UsersController(Validator _validator,
            SMSDbContext _data,
            PasswordHasher _hasher)
        {
            validator = _validator;
            data = _data;
            hasher = _hasher;
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
            var isModelValid = validator.ValidateRegistration(model);

            if(data.Users.Any(x => x.Username == model.Username))
            {
                isModelValid.Add($"User with '{model.Username}' username already exist");
            }

            if(data.Users.Any(x => x.Email == model.Email))
            {
                isModelValid.Add($"Email '{model.Email}' already exist");
            }

            if (isModelValid.Any())
            {
                return Error(isModelValid);
            }

            var user = new User()
            {
                Username = model.Username,
                Password = hasher.HashPassword(model.Password),
                Email = model.Email,
                Cart = new Cart()
            };

            data.Users.Add(user);

            data.SaveChanges();

            return Redirect("Login");
        }

        [HttpPost]

        public HttpResponse Login(LoginViewModel model)
        {
            var validateLogin = validator.ValidateLogin(model);

            var userId = data.Users.Where(x => x.Username == model.Username && x.Password == hasher.HashPassword(model.Password))
                                   .Select(x => x.Id)
                                   .FirstOrDefault();


            if (string.IsNullOrWhiteSpace(userId))
            {
                validateLogin.Add("Incorrect username or password!");
                return Error(validateLogin);
            }

            this.SignIn(userId);
            System.Console.WriteLine(User.Id.ToString());

            return Redirect("/");
        }

        public HttpResponse Logout()
        {
            this.SignOut();
            return Redirect("/");
        }
    }
}
