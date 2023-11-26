using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize(Roles = "Admin, Mod")]

    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
