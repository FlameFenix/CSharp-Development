using MyWebServer.Controllers;
using MyWebServer.Http;
using SMS.Models;
using SMS.Services;
using System.Linq;

namespace SMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly Validator validator;
        public UsersController()
        {
            validator = new Validator();
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

            if (isModelValid.Any())
            {
                return Error(isModelValid);
            }

            return View();
        }
    }
}
