using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models.ShipandCoupon
{
	public class ShipMethod
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int price { get; set; }
	}
}
