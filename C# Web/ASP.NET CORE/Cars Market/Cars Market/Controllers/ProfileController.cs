﻿using Cars_Market.Core.Services;
using Cars_Market.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cars_Market.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext data;
        private ProfileService profileService;
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

            ViewBag.Cars = await data.Cars.Where(x => x.Seller.Profile.Id.ToString() == profileId)
                                          .ToListAsync();

            return View(userProfile);
        }
    }
}