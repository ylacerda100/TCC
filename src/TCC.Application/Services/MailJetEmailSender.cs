using MailJet.Client;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace TCC.Application.Services
{
    public class MailJetEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailJetClient client = new MailJetClient(
            "0285eb83501e14b2eed71411ece81988",
            "021af0f63cf9fff181cc327513d558c8"
            );

            

            // construct your email with builder
            var email = new TransactionalEmailBuilder()
                   .WithFrom(new SendContact("from@test.com"))
                   .WithSubject("Test subject")
                   .WithHtmlPart("<h1>Header</h1>")
                   .WithTo(new SendContact("to@test.com"))
                   .Build();

            // invoke API to send email
            var response = await client.SendTransactionalEmailAsync(email);

            // check response
            Assert.AreEqual(1, response.Messages.Length);
        }
    }
}
