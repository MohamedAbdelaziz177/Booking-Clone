
using BookingClone.Domain.IRepositories;

namespace BookingClone.Infrastructure.BackgroundJobs;

public class CancelExpiredReservationsJob
{
    private readonly IReservationRepo reservationRepo;

    public CancelExpiredReservationsJob(IReservationRepo reservationRepo)
    {
        this.reservationRepo = reservationRepo;
    }

    public async Task InvalalidateExpiredReservations()
    {
        var reservations = await reservationRepo.GetExpiredReservations();

        foreach (var reservation in reservations)
            await reservationRepo.CommitCancelation(reservation.Id);

    }
}
