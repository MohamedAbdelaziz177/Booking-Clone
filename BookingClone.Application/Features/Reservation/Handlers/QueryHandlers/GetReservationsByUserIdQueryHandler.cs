using AutoMapper;
using BookingClone.Application.Common;
using BookingClone.Application.Features.Reservation.Queries;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;
using System.Collections.Generic;
using ReservationEntity = BookingClone.Domain.Entities.Reservation;

namespace BookingClone.Application.Features.Reservation.Handlers.QueryHandlers;

public class GetReservationsByUserIdQueryHandler : IRequestHandler<GetReservationsByUserIdQuery, Result<List<ReservationResponseDto>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetReservationsByUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task<Result<List<ReservationResponseDto>>> Handle(GetReservationsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var reservations = await unitOfWork.ReservationRepo.GetByUserIdAsync(request.UserId);
        var res = reservations.Select(r => mapper.Map<ReservationResponseDto>(r)).ToList();

        return new Result<List<ReservationResponseDto>>(res);
    }
}
