using App.Dtos.Email;
using Domain.Settings;

namespace App.Intefaces.Services
{
    public interface IEmailService
    {
        //public MailSettings MailSettings { get; }
        Task SendAsync(EmailRequest request); 
    }
}
