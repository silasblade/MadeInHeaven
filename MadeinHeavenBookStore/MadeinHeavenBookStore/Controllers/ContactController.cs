using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize]

    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
