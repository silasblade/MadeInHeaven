using MadeinHeavenBookStore.Models;
using MadeinHeavenBookStore.Models.ShipandCoupon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers.AdminApi
{
    [Route("api/managecoupon")]
    [ApiController]
    public class ManageCoupon : ControllerBase
    {
        private readonly CouponService _couponService;
        private readonly MadeinHeavenBookStoreContext _context;
        public ManageCoupon(CouponService couponService, MadeinHeavenBookStoreContext context)
        {
            _couponService = couponService;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Coupon>>> GetCoupons()
        {
            var coupons = _context.Coupons.ToList();
    
            coupons.Reverse();
            return Ok(coupons);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Coupon>> DeleteCoupon(int id)
        {
                Coupon coupon = await _context.Coupons.FindAsync(id);
                _context.Coupons.Remove(coupon);
                _context.SaveChanges();
                return Ok("200");

        }

        [HttpPost]
        public async Task<ActionResult<List<Coupon>>> AddCoupon()
        {
            Console.WriteLine("api serv được gọi");
            Coupon coupon = new Coupon();
            coupon.Name = "New Coupon";
            coupon.Code = "default";
            coupon.Description = "default";
             _context.Coupons.Add(coupon);
            _context.SaveChanges();
            return Ok(coupon);
        }

        [HttpPut]
        public async Task<ActionResult<List<Coupon>>> SaveCoupon(List<Coupon> coupons)
        {
            foreach(Coupon cp in coupons)
            {
                Coupon coup = _context.Coupons.Find(cp.Id);
                coup.Name = cp.Name;
                coup.Code = cp.Code;
                coup.remainnumber = cp.remainnumber;
                coup.discount = cp.discount;
                coup.Description = cp.Description;
                coup.percent = cp.percent;
                _context.SaveChanges();
            }    
            return Ok(coupons);
        }
    }
}
