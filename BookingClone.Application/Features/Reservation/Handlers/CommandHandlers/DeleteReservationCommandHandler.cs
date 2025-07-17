

using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Domain.Enums;
using BookingClone.Domain.IRepositories;
using MediatR;
using ReservationEntity = BookingClone.Domain.Entities.Reservation;

namespace BookingClone.Application.Features.Reservation.Handlers.CommandHandlers;

public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Result<string>>
{
    private readonly IUnitOfWork unitOfWork;

    public DeleteReservationCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        ReservationEntity? reservation = await unitOfWork.ReservationRepo.GetByIdAsync(request.Id);

        if (reservation == null)
            throw new EntityNotFoundException("No such reservation found");

        reservation.ReservationStatus = ReservationStatus.Cancelled;
        reservation.CheckInDate = DateTime.Now;
        reservation.CheckOutDate = DateTime.Now;

        await unitOfWork.ReservationRepo.UpdateAsync(reservation);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.CreateSuccessResult("Reservation Cancelled successfully");
    }
}
