
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Handlers.CommandHandlers;

public class CommitReservationCheckOutCommandHandler : IRequestHandler<CommitReservationCheckOutCommand, Result<string>>
{
    private readonly IReservationRepo reservationRepo;

    public CommitReservationCheckOutCommandHandler(IReservationRepo reservationRepo)
    {
        this.reservationRepo = reservationRepo;
    }

    public async Task<Result<string>> Handle(CommitReservationCheckOutCommand request, CancellationToken cancellationToken)
    {
        var reservation = await reservationRepo.GetByIdAsync(request.reservationId);

        if (reservation == null)
            throw new EntityNotFoundException("No Reservation associated to this id");

        await reservationRepo.CommitCheckOut(request.reservationId);

        return Result<string>.CreateSuccessResult(data: "ReservationStatus Changed successfully");
    }
}
