﻿using Cars_Market.Infrastructure.Data;
using Cars_Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace Cars_Market.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext data;

        public HomeController(ILogger<HomeController> logger, 
            ApplicationDbContext _data,
            IMemoryCache _memoryCache)
        {
            _logger = logger;
            data = _data;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.FirstCar = await data.Cars.OrderByDescending(x => x.Details.Visits).Take(1).Include(x => x.Pictures).FirstOrDefaultAsync();

            ViewBag.LeftCars = await data.Cars.OrderByDescending(x => x.Details.Visits).Skip(1).Take(2).ToListAsync();

            return View();
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}