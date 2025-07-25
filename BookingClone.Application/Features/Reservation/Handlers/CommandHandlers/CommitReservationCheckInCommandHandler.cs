
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Handlers.CommandHandlers;

public class CommitReservationCheckInCommandHandler : IRequestHandler<CommitReservationCheckInCommand, Result<string>>
{
    private readonly IReservationRepo reservationRepo;

    public CommitReservationCheckInCommandHandler(IReservationRepo reservationRepo)
    {
        this.reservationRepo = reservationRepo;
    }
    public async Task<Result<string>> Handle(CommitReservationCheckInCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepo.GetByIdAsync(request.reservationId);

        if (reservation == null)
            throw new EntityNotFoundException("No Reservation associated to this id");

        await reservationRepo.CommitCheckIn(request.reservationId);

        return Result<string>.CreateSuccessResult(data: "ReservationStatus Changed successfully");
    }
}       
