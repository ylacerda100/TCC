using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace TCC.Application.Services
{
    public class SmtpEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp-mail.outlook.com",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("logplay.suporte@outlook.com", "FcwpzL3CJ6SHES")
            };

            var message = new MailMessage(
                from: "logplay.suporte@outlook.com",
                to: email,
                subject: subject,
                htmlMessage
                );

            message.IsBodyHtml = true;

            return client.SendMailAsync("logplay.suporte@outlook.com", email, subject, htmlMessage);
        }
    }
}
