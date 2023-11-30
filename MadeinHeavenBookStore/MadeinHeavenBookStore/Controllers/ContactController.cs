using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MadeinHeavenBookStore.Controllers
{
    [Authorize]

    public class ContactController : Controller
    {
		private readonly EmailSender _emailSender;
		public ContactController(EmailSender emailSender)
        {
			_emailSender = emailSender;
		}
        public IActionResult Contact()
        {
            return View();
        }
		public IActionResult SendEmail(string mail, string content)
        {
            Console.WriteLine(mail + content);
			_emailSender.SendEmail("yumekoviolet@gmail.com", mail, content);
			TempData["SuccessMessage"] = "Email sent successfully!";

			return RedirectToAction("Contact");
		}
    }
}
