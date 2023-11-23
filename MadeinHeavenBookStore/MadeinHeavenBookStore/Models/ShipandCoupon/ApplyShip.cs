using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models.ShipandCoupon
{
	public class ApplyShip
	{
		[Key]
		public int Id { get; set; }
		public string IdUser { get; set; } = string.Empty;
		public int IdShip { get; set; }
	}
}
