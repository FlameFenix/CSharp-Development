using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly ProfileService profileService;
        public ProfileController(
            ApplicationDbContext _data,
            ProfileService _profileService)
        {
            data = _data;
            profileService = _profileService;
        }

        public async Task<IActionResult> MyProfile()
        {
            var userProfile = await profileService.GetProfileByEmail(User.Identity.Name);

            ViewBag.Cars = await data.Cars.Where(x => x.Seller.Email == User.Identity.Name)
                                          .ToListAsync();

            return View(userProfile);
        }

        public async Task<IActionResult> Profile(string profileId)
        {
            var userProfile = await profileService.GetProfileById(profileId);

            if(userProfile == null)
            {
                ViewBag.ErrorTitle = "Error occured while looking user profile";
                ViewBag.ErrorMessage = "The resource you are looking for (or one of its dependencies) could have been removed," +
                    " had its name changed, or is temporarily unavailable." +
                    " Please review the following URL and make sure that it is spelled correctly.";
                return View("Error");
            }

            ViewBag.Cars = await data.Cars.Where(x => x.Seller.Profile.Id.ToString() == profileId)
                                          .ToListAsync();

            ViewBag.UserEmail = await profileService.GetProfileEmail(profileId);

            return View(userProfile);
        }
    }
}
