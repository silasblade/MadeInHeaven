using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models.ShipandCoupon
{
	public class ApplyCoupon
	{
		[Key]
		public int Id { get; set; }
		public string IdUser { get; set; } = string.Empty;
		public int IdCoupon { get; set; }
	}
}
