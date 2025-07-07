

using AutoMapper;
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Reservation.Commands;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using ReservationEntity = BookingClone.Domain.Entities.Reservation;
namespace BookingClone.Application.Features.Reservation.Handlers.CommandHandlers;

public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, Result<ReservationResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ILogger<UpdateReservationCommandHandler> logger;

    public UpdateReservationCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<UpdateReservationCommandHandler> logger)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.logger = logger;
    }
    public async Task<Result<ReservationResponseDto>> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        ReservationEntity? reservation = await unitOfWork.ReservationRepo.GetByIdAsync(request.Id);

        if (reservation == null)
            throw new EntityNotFoundException("Reservation not found");

        bool checkAvailable = await unitOfWork
            .RoomRepo
            .CheckAvailableBetweenAsync(request.RoomId, request.CheckInDate, request.CheckOutDate);

        if (!checkAvailable)
            return new Result<ReservationResponseDto>(success:false , "Room is occupied in that range");

        mapper.Map(request, reservation);

        ReservationResponseDto res = mapper.Map<ReservationResponseDto>(reservation);

        return new Result<ReservationResponseDto>(res, true, 
            "Reservation Updated Successfully");
    }
}
