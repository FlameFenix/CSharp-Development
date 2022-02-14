using MyWebServer.Controllers;
using MyWebServer.Http;
using SMS.Data;
using SMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Controllers
{
    public class CartsController : Controller
    {
        private readonly SMSDbContext data;
        private readonly CartService cartService;
        public CartsController(SMSDbContext _data, CartService _cartService)
        {
            data = _data;
            cartService = _cartService;
        }

        [Authorize]
        public HttpResponse Details()
        {
            var products = cartService.GetProducts(this.User.Id);

            return View(products);
        }
        
        [Authorize]
        public HttpResponse AddProduct(string productId)
        {
            cartService.AddProduct(productId, this.User.Id);

            return Redirect("Details");
        }

        public HttpResponse Buy()
        {
            cartService.BuyProducts(this.User.Id);

            return Redirect("/");
        }
    }
}
