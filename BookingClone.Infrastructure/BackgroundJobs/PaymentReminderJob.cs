
using BookingClone.Application.Contracts;
using BookingClone.Domain.Enums;
using BookingClone.Domain.IRepositories;

namespace BookingClone.Infrastructure.BackgroundJobs;

public class PaymentReminderJob
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IEmailService emailService;

    public PaymentReminderJob(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        this.unitOfWork = unitOfWork;
        this.emailService = emailService;
    }

    public async Task SendReminderAsync(int reservationId)
    {
        var payment = await unitOfWork.PaymentRepo.GetPaymentByReservatioIdAsync(reservationId);
        var reservation = await unitOfWork.ReservationRepo.GetByIdAsync(reservationId);
        
        if (payment == null && reservation!.ReservationStatus == ReservationStatus.Pending)
        {
            var userEmail = reservation!.User.Email;
            var userFname = reservation!.User.Firstname;

            await emailService.SendMail(userEmail!, "Payment Reminder",
                $"Dear {userFname}, please complete your payment for reservation #{reservationId}.");
        }
    }
}
