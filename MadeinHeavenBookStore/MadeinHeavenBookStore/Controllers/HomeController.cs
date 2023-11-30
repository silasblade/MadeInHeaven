using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
		private readonly MadeinHeavenBookStoreContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ILogger<HomeController> logger, UserManager<MadeinHeavenBookStoreUser> userManager, MadeinHeavenBookStoreContext context, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            this._userManager = userManager;
			this._context = context;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            if(!(await _roleManager.RoleExistsAsync("Admin")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            var usermail = _userManager.Users.FirstOrDefault(u => u.Id == _userManager.GetUserId(this.User));
            if(usermail != null)
            {
                if (usermail.Email == "admin@gmail.com")
                {
                    MadeinHeavenBookStoreUser user = await _userManager.FindByEmailAsync("admin@gmail.com");
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
            


            ViewData["ID"] = _userManager.GetUserId(this.User);
			return View();
        }

		public async Task<IActionResult> Home()
		{
	
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