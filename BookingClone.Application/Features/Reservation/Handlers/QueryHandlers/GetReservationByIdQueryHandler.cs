
using AutoMapper;
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Reservation.Queries;
using BookingClone.Application.Features.Reservation.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;
using ReservationEntity = BookingClone.Domain.Entities.Reservation; 
namespace BookingClone.Application.Features.Reservation.Handlers.QueryHandlers;

public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, Result<ReservationResponseDto>>
{

    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetReservationByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task<Result<ReservationResponseDto>> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {   
         ReservationEntity? r = await unitOfWork.ReservationRepo.GetByIdAsync(request.Id);
         
         if (r == null)
             throw new EntityNotFoundException("Reservation not found");
         
         ReservationResponseDto res = mapper.Map<ReservationResponseDto>(r);
         
         return ResultBuilder<ReservationResponseDto>.CreateSuccessResponse(res);
        
    }
}
