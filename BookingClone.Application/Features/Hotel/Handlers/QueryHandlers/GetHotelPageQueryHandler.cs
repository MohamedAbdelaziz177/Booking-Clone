


using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using HotelEntity = BookingClone.Domain.Entities.Hotel;

namespace BookingClone.Application.Features.Hotel.Handlers.QueryHandlers;

public class GetHotelPageQueryHandler : IRequestHandler<GetHotelPageQuery, Result<List<HotelResponseDto>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetHotelPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<List<HotelResponseDto>>> Handle(GetHotelPageQuery request,
        CancellationToken cancellationToken)
    {
        List<HotelEntity> hotels = (!string.IsNullOrEmpty(request.SortField) &&
            !string.IsNullOrEmpty(request.SortType.ToString())) ?

            await unitOfWork.HotelRepo.GetAllAsync(request.PageIdx,
            request.PageSize,
            request.SortField,
            request.SortType.ToString()!) :

            await unitOfWork.HotelRepo.GetAllAsync(request.PageIdx,
            request.PageSize);

       

        List<HotelResponseDto> hotelResponseDtos = hotels.Select(h => 
        mapper.Map<HotelResponseDto>(h)).ToList();

        return new Result<List<HotelResponseDto>>(hotelResponseDtos);
    }
}
