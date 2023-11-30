using MadeinHeavenBookStore.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Models.MVCService
{
    public class UserService
    {
        private readonly MadeinHeavenBookStoreContext _context;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserService(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Tuple<List<string>, List<string>>> GetUser(string role, string search)
        {
            List<string> mods = new List<string>();
            List<string> users = new List<string>();
            List<MadeinHeavenBookStoreUser> Allusers = _userManager.Users.ToList();
            foreach (var user in Allusers)
            {

                if (!await _userManager.IsInRoleAsync(user, "Admin") && user.Email != null)
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
            if (!string.IsNullOrEmpty(role) )
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

            if(search != null)
            {
                users = users.Where(c => c.Contains(search)).ToList();
                mods = mods.Where(c => c.Contains(search)).ToList();
            }

            var data = new Tuple<List<string>, List<string>>(mods, users);
            return data;
        }

        public async Task ToModder(string email)
        {
            if (!await _roleManager.RoleExistsAsync("Mod"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Mod"));
            }
            MadeinHeavenBookStoreUser user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(user, "Mod");
        }

        public async Task ToUser(string email)
        {
            MadeinHeavenBookStoreUser user = await _userManager.FindByEmailAsync(email);
            await _userManager.RemoveFromRoleAsync(user, "Mod");
        }

        public async Task RemoveUser(string email)
        {
            MadeinHeavenBookStoreUser user = await _userManager.FindByEmailAsync(email);
            await _userManager.DeleteAsync(user);

        }
    }
}
