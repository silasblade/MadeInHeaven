using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models.ShipandCoupon
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "coupon";
        public string Code { get; set; } = "coupon";
        public int remainnumber { get; set; } = 999999999;
        public string Description { get; set; } = "coupon";
        public int discount { get; set; } = 0;
        public bool percent { get; set; } = false;

    }
}
