using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{

    [Authorize]

    public class ProductDetailController : Controller
    {
        public IActionResult ProductDetail()
        {
            return View();
        }
    }
}
