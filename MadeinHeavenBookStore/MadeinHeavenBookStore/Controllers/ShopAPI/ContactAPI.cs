using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers.ShopAPI
{
	[Route("api/ContactAPI")]
	[ApiController]
	public class ContactAPI : ControllerBase
    {
		private readonly EmailSender _emailSender;
		public ContactAPI(EmailSender emailSender)
        {
			_emailSender = emailSender;
		}

		[HttpPost]
		[Route("SendEmail/{mail}/{content}")]
		
		public async Task<ActionResult<string>> SendEmail(string mail, string content)
        {
			Console.WriteLine("Call Send" + mail);
			_emailSender.SendEmail("yumekoviolet@gmail.com", mail, content);
			return Ok("Email sent successfully!");
		}

        [HttpPost]
        [Route("ReturnEmail/{mail}/{subject}/{content}/{id}")]
        public async Task<ActionResult<string>> ReturnEmail(string mail, string subject, string content, string id)
		{
			string newcontent = content + "Please check information at: madeinheavenonline.azurewebsites.net/Order/OrderDetail/" + id;

            Console.WriteLine("Call Send" + mail);
			_emailSender.SendEmail(mail, subject, newcontent);
			return Ok("Email sent successfully!");
		}
	}
}
