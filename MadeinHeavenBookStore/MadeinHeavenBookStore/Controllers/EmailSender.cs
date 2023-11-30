using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace MadeinHeavenBookStore.Controllers
{
	public class EmailSender
	{
		private readonly IConfiguration _configuration;

		public EmailSender(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void SendEmail(string to, string subject, string body)
		{
			var emailSettings = _configuration.GetSection("EmailSettings");

			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(subject, emailSettings["UserName"]));
			message.To.Add(new MailboxAddress("Recipient Name", to));
			message.Subject = subject;

			var bodyBuilder = new BodyBuilder();
			bodyBuilder.HtmlBody = body;
			message.Body = bodyBuilder.ToMessageBody();

			using (var client = new SmtpClient())
			{
				client.Connect(emailSettings["MailServer"], int.Parse(emailSettings["MailPort"]), false);
				client.Authenticate(emailSettings["UserName"], emailSettings["Password"]);
				client.Send(message);
				client.Disconnect(true);
			}
		}
	}
}
