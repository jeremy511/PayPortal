
using PayPortal.Core.Application.Dtos.Email;

namespace PayPortal.Core.Application.Interfaces.Services
{
    public interface IEmailServices
    {
        Task SendAssync(EmailRequest request);

    }
}