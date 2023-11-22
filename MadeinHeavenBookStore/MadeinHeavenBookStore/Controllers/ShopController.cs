using MadeinHeavenBookStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize]

    public class ShopController : Controller
    {
        private readonly MadeinHeavenBookStoreContext _context;

        public ShopController(MadeinHeavenBookStoreContext context)
        {
            _context = context;
        }

        public IActionResult Shop()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}
