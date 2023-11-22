using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MadeinHeavenBookStore.Models
{
	// Customer.cs
	public class Customer
	{
		[Required]
		[BindNever]
		public int IdCustomer { get; set; }

		[Required]
		[BindNever]
		
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;

		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
	}

}
