namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
           return View();
            
        }

        public HttpResponse IndexLoggedIn()
        {
            return View();
        }
    }
}