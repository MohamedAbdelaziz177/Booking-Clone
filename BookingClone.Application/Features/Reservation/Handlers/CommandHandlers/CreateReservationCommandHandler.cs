
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using ReservationEntity = BookingClone.Domain.Entities.Reservation;

namespace BookingClone.Application.Features.Reservation.Handlers.CommandHandlers;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Result<ReservationResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateReservationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<ReservationResponseDto>> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {

        using var Trx = await unitOfWork.GetSerializableTransaction();
        try
        {
            bool checkAvailable = await unitOfWork
            .RoomRepo
            .CheckAvailableBetweenAsync(request.RoomId, request.CheckInDate, request.CheckOutDate);

            if (!checkAvailable)
                return new Result<ReservationResponseDto>(success: false, "Room is occupied in that range");

            ReservationEntity entity = mapper.Map<ReservationEntity>(request);

            await unitOfWork.ReservationRepo.AddAsync(entity);

            ReservationResponseDto res = mapper.Map<ReservationResponseDto>(entity);

            await Trx.CommitAsync();

            return new Result<ReservationResponseDto>(res, true, "Added Successfully");
        } 
        catch (Exception ex)
        {
            await Trx.RollbackAsync();
            throw;
        }
    }
}
