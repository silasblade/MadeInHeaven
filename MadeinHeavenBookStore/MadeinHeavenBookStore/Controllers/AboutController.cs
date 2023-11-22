using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize]

    public class AboutController : Controller
    {
        public IActionResult About()
        {
            return View();
        }
    }
}
