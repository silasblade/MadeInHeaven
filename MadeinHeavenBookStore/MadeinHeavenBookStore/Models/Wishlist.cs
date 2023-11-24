using MadeinHeavenBookStore.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models
{
	public class Wishlist
	{
		[Key]
		public int Id { get; set; }
		public MadeinHeavenBookStoreUser? User { get; set; } = new MadeinHeavenBookStoreUser();
		public string IdUser { get; set; }
		public Product Product { get; set; }
	}
}
