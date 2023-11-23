using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models.OrderDetail
{
	public class OrderProduct
	{
		[Key]
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}
