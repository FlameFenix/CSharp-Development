using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Cars_Market.Controllers
{
    public class SellerController : Controller
    {
        private ApplicationDbContext data;
        private readonly ByteConverter converter;
        private readonly SellerService sellerService;
        public SellerController(ApplicationDbContext _data,
            ByteConverter _converter,
            SellerService _sellerService)
        {
            data = _data;
            converter = _converter;
            sellerService = _sellerService;
        }

        [Authorize]
        public IActionResult AddSeller() => View();

        [HttpPost]
        public async Task<IActionResult> AddSeller(AddSellerFormModel sellerModel)
        {
            var seller = await sellerService.GetSellerByEmail(sellerModel.Email);

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

            // Testing !
            var identity = new IdentityUserRole<string>()
            {
                RoleId = data.Roles.FirstOrDefault(x => x.Name == "Seller").Id,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()
            };

            data.UserRoles.Add(identity);

            data.SaveChanges();

            return Redirect("/Cars/Add");
        }

        public async Task<IActionResult> AllSellers()
        {
            var sellers = await data.Sellers.Include(x => x.Profile).ToListAsync();

            return View(sellers);
        }
    }
}
