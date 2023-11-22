﻿using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<MadeinHeavenBookStoreUser> userManager)
        {
            _logger = logger;
            this._userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewData["ID"] = _userManager.GetUserId(this.User);
			return View();
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

		




		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}