using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize]

    public class ShoppingCartController : Controller
    {
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
