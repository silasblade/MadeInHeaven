using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
    public class CounterController : Controller
    {
        public IActionResult Counter()
        {
            return View();
        }
    }
}
