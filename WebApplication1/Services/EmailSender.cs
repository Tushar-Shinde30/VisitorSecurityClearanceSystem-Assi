using SendGrid;
using SendGrid.Helpers.Mail;
using WebApplication1.Common;

namespace WebApplication1.Services
{
    public class EmailSender
    {
        public async Task SendEmail(string subject,string toEmail,string username,string message)
        {
            var apiKey = Credentials.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("tidkeshubham10@gmail.com", "Demo");
            var to = new EmailAddress(toEmail, username);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendEmail(string subject, string toEmail, string username, string message, byte[]? pdfBytes)
        {
            
            var apiKey = Credentials.ApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("tidkeshubham10@gmail.com", "Demo");
            var to = new EmailAddress(toEmail, username);
            var plainTextContent = message;
            var htmlContent = "";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            if (pdfBytes != null)
            {
                msg.AddAttachment("VisitorPass.pdf", Convert.ToBase64String(pdfBytes));

            }

            var response = await client.SendEmailAsync(msg);
        }
    }
}
