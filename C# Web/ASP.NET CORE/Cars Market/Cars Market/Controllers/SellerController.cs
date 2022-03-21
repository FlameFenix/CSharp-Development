using Cars_Market.Core.Services;
using Cars_Market.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Controllers
{
    public class SellerController : Controller
    {
        private ApplicationDbContext data;
        private ByteConverter converter;
        private Validator validator;
        private readonly SellerService sellerService;
        public SellerController(ApplicationDbContext _data,
            ByteConverter _converter,
            Validator _validator,
            SellerService _sellerService)
        {
            data = _data;
            converter = _converter;
            validator = _validator;
            sellerService = _sellerService;
        }

        [Authorize]
        public IActionResult AddSeller()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSeller(AddSellerFormModel sellerModel)
        {
            var seller = await data.Sellers.FirstOrDefaultAsync(x => x.Email == sellerModel.Email);

            if (seller != null)
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
            
            await sellerService.AddSeller(seller);

            return Redirect("/Cars/Add");
        }

        public async Task<IActionResult> AllSellers()
        {
            var sellers = await data.Sellers.Include(x => x.Profile).ToListAsync();

            return View(sellers);
        }
    }
}
