using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize]

    public class ShopController : Controller
    {
        public IActionResult Shop()
        {
            return View();
        }
    }
}
