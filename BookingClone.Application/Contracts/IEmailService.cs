
namespace BookingClone.Application.Contracts;

public interface IEmailService
{
    Task<bool> SendMail(string To, string Subject, string Body);
}
