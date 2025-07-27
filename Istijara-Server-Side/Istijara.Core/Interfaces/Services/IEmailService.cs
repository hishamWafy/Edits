using Istijara.Core.DTOs.Identity;

namespace Istijara.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
