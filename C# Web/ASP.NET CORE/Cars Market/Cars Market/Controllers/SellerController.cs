using AutoMapper;
using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Constants;
using Cars_Market.Infrastructure.Data.Models;
using Cars_Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Cars_Market.Controllers
{
    public class SellerController : Controller
    {
        private readonly ByteConverter converter;
        private readonly SellerService sellerService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMemoryCache memoryCache;
        private readonly IMapper mapper;
        public SellerController(ByteConverter _converter,
            SellerService _sellerService,
            IMemoryCache _memoryCache,

            UserManager<IdentityUser> _userManager,
            IMapper _mapper)
        {
            converter = _converter;
            sellerService = _sellerService;
            userManager = _userManager;
            memoryCache = _memoryCache;
            mapper = _mapper;
        }

        [Authorize]
        public IActionResult AddSeller() => View();

        [HttpPost]
        public async Task<IActionResult> AddSeller(AddSellerFormModel sellerModel)
        {
            var seller = await sellerService.GetSellerByEmail(sellerModel.Email);

            if (seller != null)
            {
                ViewBag.ErrorTitle = ErrorConstants.ERROR_TITLE_WHILE_CREATE_SELLER;
                ViewBag.ErrorMessage = ErrorConstants.ERROR_MESSAGE_WHILE_CREATE_SELLER;

                return View("Error");
            }

            seller = mapper.Map<AddSellerFormModel, Seller>(sellerModel);

            var profile = mapper.Map<AddSellerFormModel, Infrastructure.Data.Models.Profile>(sellerModel);
            profile.Picture = converter.ConvertToByteArray(sellerModel.Image);
            profile.Seller = seller;

            seller.Profile = profile;

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
