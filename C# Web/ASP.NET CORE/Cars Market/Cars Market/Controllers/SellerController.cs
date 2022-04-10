using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Cars_Market.Controllers
{
    public class SellerController : Controller
    {

        private readonly ApplicationDbContext data;
        private readonly ByteConverter converter;
        private readonly SellerService sellerService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMemoryCache memoryCache;
        public SellerController(ApplicationDbContext _data,
            ByteConverter _converter,
            SellerService _sellerService,
            IMemoryCache _memoryCache,
            UserManager<IdentityUser> _userManager)
        {
            data = _data;
            converter = _converter;
            sellerService = _sellerService;
            userManager = _userManager;
            memoryCache = _memoryCache;
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
            seller = new Seller()
            {
                Email = sellerModel.Email,
            };

            var sellerProfile = new Profile()
            {
                Location = sellerModel.Location,
                Name = sellerModel.Name,
                Phone = sellerModel.Phone,
                Picture = converter.ConvertToByteArray(sellerModel.Picture),
                SellerId = seller.Id
            };

            seller.Profile = sellerProfile;

            await sellerService.AddSeller(seller);

            var user = await userManager.FindByEmailAsync(sellerModel.Email);

            await userManager.AddToRoleAsync(user, "User");

            return Redirect("/Cars/AllCars");
        }

        public async Task<IActionResult> AllSellers()
        {
            const string sellersCache = "sellersCache";

            var sellers = memoryCache.Get<List<Seller>>(sellersCache);

            if (sellers == null)
            {
                sellers = await sellerService.GetSellers();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                memoryCache.Set(sellersCache, sellers, cacheOptions);
            }

            return View(sellers);
        }
    }
}
