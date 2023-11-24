using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models.Comment
{
	public class Comment
	{
		[Key]
		public int Id { get; set; }
		public string IdUser { get; set; }
		public int IdProduct { get; set; }
		public string Content { get; set; }
		[Range(0, 5)]
		public int Star { get; set; } = 0;
	}
}
