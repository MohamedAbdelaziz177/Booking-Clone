
using BookingClone.Application.Contracts;
using BookingClone.Domain.Enums;
using BookingClone.Domain.IRepositories;

namespace BookingClone.Infrastructure.BackgroundJobs;

public class RefundReminderJob
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IEmailService emailService;

    public RefundReminderJob(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        this.unitOfWork = unitOfWork;
        this.emailService = emailService;
    }

    public async Task SendReminderAsync(int reservationId)
    {
        var payment = await unitOfWork.PaymentRepo.GetPaymentByReservatioIdAsync(reservationId);

        if (payment != null && payment!.Status != PaymentStatus.Refunded)
        {
            var userEmail = payment.Reservation.User.Email;
            var userFname = payment.Reservation.User.Firstname;

            await emailService.SendMail(userEmail!, "Refund Reminder",
                $"Dear {userFname}, please complete your refund process after cancelling reservation #{reservationId}.");
        }
    }
}
