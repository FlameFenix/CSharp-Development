namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Data;

    public class HomeController : Controller
    {
        private readonly SMSDbContext data;
        public HomeController(SMSDbContext _data)
        {
            data = _data;
        }
        public HttpResponse Index()
        {
           if (this.User.IsAuthenticated)
           {
               return IndexLoggedIn();
           }

           return View();
            
        }

        public HttpResponse IndexLoggedIn()
        {
            return View(data.Products);
        }
    }
}