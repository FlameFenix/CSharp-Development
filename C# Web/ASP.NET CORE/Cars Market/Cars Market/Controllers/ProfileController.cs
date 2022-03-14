using Cars_Market.Data;
using Cars_Market.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cars_Market.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext data;
        public ProfileController(ApplicationDbContext _data)
		{
            data = _data;
		}
        public IActionResult Profile(string sellerId)
        {
            var userProfile = data.Sellers
                .Where(x => x.Email == User.Identity.Name)
                .Select(x => x.Profile)
                .FirstOrDefault();

            return View(userProfile);
        }
    }
}
