namespace CarShop.Controllers
{
    using MyWebServer.Http;
    using MyWebServer.Controllers;

    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            return this.View();
        }
    }
}
