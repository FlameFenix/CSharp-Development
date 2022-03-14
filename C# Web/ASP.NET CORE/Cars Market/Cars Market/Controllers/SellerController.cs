using Cars_Market.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Cars_Market.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    public class SellerController : Controller
    {
        private ApplicationDbContext data;
        private ByteConverter converter;
        public SellerController(ApplicationDbContext _data, ByteConverter _converter)
        {
            data = _data;
            converter = _converter;
        }

        [Authorize]
        public IActionResult AddSeller()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSeller(AddSellerFormModel sellerModel)
        {
            var seller = data.Sellers.FirstOrDefault(x => x.Email == sellerModel.Email);

            if(seller != null)
            {
                return BadRequest();
            }

            var sellerProfile = new Profile()
            {
                Location = sellerModel.Location,
                Name = sellerModel.Name,
                Phone = sellerModel.Phone,
                Picture = converter.ConvertToByteArray(sellerModel.Picture)
            };

            seller = new Seller()
            {
                Email = sellerModel.Email,
                Profile = sellerProfile
            };

            data.Sellers.Add(seller);

            data.SaveChanges();

            return Redirect("/Cars/Add");
        }
    }
}
