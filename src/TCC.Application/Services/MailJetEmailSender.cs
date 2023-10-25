using MailJet.Client;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Reflection;

namespace TCC.Application.Services
{
    public class MailJetEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public MailJetEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailJetClient client = new MailJetClient(
                _config.GetValue<string>("MailJet:PublicKey"),
                _config.GetValue<string>("MailJet:SecretKey")
            );

            var message = new MailMessage(
                from: "logplay.suporte@outlook.com",
                to: email,
                subject: subject,
                htmlMessage
                );

            //// construct your email with builder
            //var email = new TransactionalEmailBuilder()
            //       .WithFrom(new SendContact("from@test.com"))
            //       .WithSubject("Test subject")
            //       .WithHtmlPart("<h1>Header</h1>")
            //       .WithTo(new SendContact("to@test.com"))
            //       .Build();

            // invoke API to send email
            var response = client.SendMessage(message);
        }
    }
}
