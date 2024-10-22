using App.Dtos.Email;
using App.Intefaces.Services;
using Domain.Settings;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;
using MailKit.Net.Smtp;
namespace Shared.Services
{
    public class EmailService : IEmailService
    {
        private MailSettings MailSettings { get; }

        public EmailService(IOptions<MailSettings> mailSettings)
        {
            MailSettings = mailSettings.Value;
        }

        public async Task SendAsync(EmailRequest emailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(emailRequest.From ?? MailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(emailRequest.To));
                email.Subject = emailRequest.Subject;

                var dyBuilder = new BodyBuilder();
                dyBuilder.HtmlBody = emailRequest.Body;
                email.Body = dyBuilder.ToMessageBody();

                using SmtpClient smtp = new();
                smtp.Connect(MailSettings.SmtpHost, MailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(MailSettings.SmtpUser, MailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
