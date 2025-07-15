
using BookingClone.Application.Common;
using BookingClone.Application.Features.Reservation.Queries;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;

namespace BookingClone.Application.Features.Reservation.Handlers.QueryHandlers;

public class GetReservationsPageQueryHandler : IRequestHandler<GetReservationsPageQuery, Result<List<ReservationResponseDto>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetReservationsPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task<Result<List<ReservationResponseDto>>> Handle(GetReservationsPageQuery request, CancellationToken cancellationToken)
    {
        var reservations = await unitOfWork.ReservationRepo.GetByDateAsync(request.date,
            request.PageIdx,
            request.PageSize,
            request.SortField,
            request.SortType,
            request.hotelId);

        var Dtos = reservations.Select(r => mapper.Map<ReservationResponseDto>(r)).ToList();

        return new Result<List<ReservationResponseDto>>(Dtos);
    }
}
