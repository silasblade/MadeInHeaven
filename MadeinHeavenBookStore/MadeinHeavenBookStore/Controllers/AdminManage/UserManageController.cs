using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers.AdminManage
{
    public class UserManageController : Controller
    {
        private readonly MadeinHeavenBookStoreContext _context;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserManageController(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> UserManage(string role)
        {
            List<string> mods = new List<string>();
            List<string> users = new List<string>();
            List<MadeinHeavenBookStoreUser> Allusers = _userManager.Users.ToList();
            foreach (var user in Allusers)
            {
                if(!await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    if (await _userManager.IsInRoleAsync(user, "Mod"))
                    {
                        mods.Add(user.Email);
                    }
                    else
                    {
                        users.Add(user.Email);
                    }
                }
                   
            }
            if(!string.IsNullOrEmpty(role))
            {
                if (role == "Modders")
                {
                    users.Clear();
                }
                if (role == "Normal Users")
                {
                    mods.Clear();
                }
            }
            
            var data = new Tuple<List<string>, List<string>>(mods, users);
            return View(data);
        }

        public async Task<IActionResult> ToModder(string email)
        {
            if (!await _roleManager.RoleExistsAsync("Mod"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Mod"));
            }
            MadeinHeavenBookStoreUser user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(user, "Mod");
            return RedirectToAction("UserManage");
        }

        public async Task<IActionResult> ToUser(string email)
        {
            MadeinHeavenBookStoreUser user = await _userManager.FindByEmailAsync(email);
            await _userManager.RemoveFromRoleAsync(user, "Mod");
            return RedirectToAction("UserManage");
        }

        public async Task<IActionResult> RemoveUser(string email)
        {
            MadeinHeavenBookStoreUser user = await _userManager.FindByEmailAsync(email);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("UserManage");
        }
    }
}
