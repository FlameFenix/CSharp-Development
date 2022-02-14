using MyWebServer.Controllers;
using MyWebServer.Http;
using SMS.Data;
using SMS.Data.Models;
using SMS.Models;
using SMS.Services;
using System;
using System.Globalization;
using System.Linq;

namespace SMS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SMSDbContext data;
        private readonly Validator validator;
        public ProductsController(
            SMSDbContext _data, 
            Validator _validator)
        {
            validator = _validator;
            data = _data;
        }

        public object ValidateProduct { get; private set; }

        public HttpResponse Create()
        {
            return View();
        }

        [HttpPost]

        public HttpResponse Create(ProductViewModel model)
        {
            var errors = validator.ValidateProduct(model);

            var productId = data.Products.Where(x => x.Name == model.Name).Select(x => x.Id);

            if (string.IsNullOrWhiteSpace(productId.ToString()))
            {
                errors.Add("Product already exist");
            }

            if (errors.Any())
            {
                return Error(errors);
            }

            var product = new Product()
            {
                Name = model.Name,
                Price = decimal.Parse(model.Price, CultureInfo.InvariantCulture),
            };

            data.Products.Add(product);

            data.SaveChanges();

            return Redirect("/");
        }
    }
}
