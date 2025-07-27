using Istijara.Core.DTOs.Identity;
using Istijara.Core.Interfaces.Services;
using Istijara.Service.Configurations;
using MailKit.Net.Smtp;
using MimeKit;

namespace Istijara.Service.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfigurations _emailConfig;

        public EmailService(EmailConfigurations emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            SendAsync(emailMessage);

        }


        private MimeMessage CreateEmailMessage(EmailMessage message)
        {

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(
                "<h2 style='color:red'>{0}</h2>", message.Content
                )
            };

            return emailMessage;

        }


        private async Task SendAsync(MimeMessage emailMessage)
        {

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(emailMessage);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }

        }
    }
}
