using MadeinHeavenBookStore.Areas.Identity.Data;
using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers.AdminManage
{
    public class CouponManageController : Controller
    {
        private readonly UserManager<MadeinHeavenBookStoreUser> _userManager;
        private readonly MadeinHeavenBookStoreContext _context;


        public CouponManageController(UserManager<MadeinHeavenBookStoreUser> userManager, MadeinHeavenBookStoreContext context)
        {
            this._userManager = userManager;
            this._context = context;
        }
        public IActionResult CouponManage(string? percent, string? search)
        {
            List<Coupon> coupons = _context.Coupons.ToList();
            if(!string.IsNullOrEmpty(percent))
            {
                if (percent == "Percent")
                {
                    coupons = coupons.Where(c => c.percent == true).ToList();
                }
                else
                {
                    coupons = coupons.Where(c => c.percent == false).ToList();
                }
            }
            
            if(!string.IsNullOrWhiteSpace(search))
            {
                coupons = coupons.Where(c => c.Name.Contains(search)).ToList();
            }    

            return View(coupons);
        }
        [HttpPost]
        public IActionResult SaveCoupon(Coupon coupon)
        {
            // Lấy coupon từ cơ sở dữ liệu theo ID
            Coupon existingCoupon = _context.Coupons.Find(coupon.Id);

            // Kiểm tra xem có coupon không
     
                // Cập nhật các thuộc tính từ đối tượng nhận được từ form
                if(coupon.Id == 1)
            {
                return RedirectToAction("CouponManage");
            }

                existingCoupon.Name = coupon.Name;
                existingCoupon.Description = coupon.Description;
                existingCoupon.discount = coupon.discount;
                existingCoupon.remainnumber = coupon.remainnumber;
                existingCoupon.Code = coupon.Code;
                existingCoupon.percent = coupon.percent;
            _context.SaveChanges();

                // Lưu thay đổi vào cơ sở dữ liệu
     

            return RedirectToAction("CouponManage");
        }

        public IActionResult DeleteCoupon(int id)
        {
            if (id == 1)
            {
                return RedirectToAction("CouponManage");
            }
            Coupon coupon = _context.Coupons.Find(id);
            _context.Coupons.Remove(coupon);
            _context.SaveChanges();
            return RedirectToAction("CouponManage");

        }

        public IActionResult AddCoupon()
        {
            Coupon coupon = new Coupon();
            coupon.Name = "New Coupon";
            coupon.remainnumber = 0;
            coupon.discount = 0;
            coupon.Code = "default";
            coupon.Description = "default";
            _context.Coupons.Add(coupon);
            _context.SaveChanges();
            return RedirectToAction("CouponManage");

        }
    }
}
