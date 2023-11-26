using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers.AdminManage
{
    public class AdminController : Controller
    {
        private readonly MadeinHeavenBookStoreContext _context;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult AdminView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdminCreate(string mail)
        {
            MadeinHeavenBookStoreUser user = await _userManager.FindByEmailAsync(mail);

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await _roleManager.RoleExistsAsync("Mod"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Mod"));
            }

            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Mod");
                return RedirectToAction("AdminView");
            }


            return RedirectToAction("AdminView");
        }
    }
}
