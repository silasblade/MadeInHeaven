using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
    public class CouponController : Controller
    {

        private readonly MadeinHeavenBookStoreContext _context;
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        public CouponController(MadeinHeavenBookStoreContext context, UserManager<MadeinHeavenBookStoreUser> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }
        public IActionResult Coupon()
        {
            List<Coupon> coupons = _context.Coupons.ToList();
            return View(coupons);
        }
    }
}
