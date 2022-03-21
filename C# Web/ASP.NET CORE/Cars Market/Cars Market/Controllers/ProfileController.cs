using Cars_Market.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext data;
        public ProfileController(ApplicationDbContext _data)
		{
            data = _data;
		}

        public async Task<IActionResult> MyProfile()
        {
            var userProfile = await data.Sellers
                .Where(x => x.Email == User.Identity.Name)
                .Select(x => x.Profile)
                .FirstOrDefaultAsync();

            ViewBag.Cars = await data.Cars.Where(x => x.Seller.Email == User.Identity.Name)
                                          .ToListAsync();

            return View(userProfile);
        }

        public async Task<IActionResult> Profile(string profileId)
        {
            var userProfile = await data.Profiles
                .Where(x => x.Id.ToString() == profileId)
                .FirstOrDefaultAsync();

            ViewBag.Cars = await data.Cars.Where(x => x.Seller.Profile.Id.ToString() == profileId)
                                          .ToListAsync();

            return View(userProfile);
        }
    }
}
