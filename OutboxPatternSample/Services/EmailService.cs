using System.Net;
using System.Net.Mail;

namespace OutboxPatternSample.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string body)
        {

            var client = new SmtpClient("smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential("xx", "xx"),
                EnableSsl = true
            };
            await client.SendMailAsync("from@example.com", email, subject, body);

        }
    }
}
