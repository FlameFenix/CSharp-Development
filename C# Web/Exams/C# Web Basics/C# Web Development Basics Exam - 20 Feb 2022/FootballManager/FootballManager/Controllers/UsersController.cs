using FootballManager.Data;
using FootballManager.Data.Models;
using FootballManager.Services.PasswordHash;
using FootballManager.Services.Validator;
using FootballManager.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace FootballManager.Controllers
{
    public class UsersController : Controller
    {
        private Validator validator;
        private PasswordHasher passwordHasher;
        private FootballManagerDbContext data;

        public UsersController(Validator _validator,
            PasswordHasher _passwordHasher,
            FootballManagerDbContext _data)
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
                return Redirect("Register");
            }

            User user = new User()
            {
                Username = model.Username,
                Password = passwordHasher.HashPassword(model.Password),
                Email = model.Email,
            };

            data.Users.Add(user);
            data.SaveChanges();

            return Redirect("Login");
        }

        [HttpPost]
        public HttpResponse Login(LoginViewModel model)
        {
            var errors = validator.ValidateLogin(model);

            if (errors.Any())
            {
                return Redirect("Login");
            }

            var user = data.Users.FirstOrDefault(x => x.Username == model.Username);

            SignIn(user.Id);

            return Redirect("/");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            SignOut();
            return Redirect("/");
        }
    }
}
