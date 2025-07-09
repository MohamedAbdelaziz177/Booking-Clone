
using BookingClone.Application.Common;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Domain.IRepositories;
using Mapster;
using MapsterMapper;
using MediatR;
using HotelEntity = BookingClone.Domain.Entities.Hotel;

namespace BookingClone.Application.Features.Hotel.Handlers.QueryHandlers;

public class GetHotelsByCityOrCountryHandler : IRequestHandler<GetHotelsByCityOrCountryQuery,
    Result<List<HotelResponseDto>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetHotelsByCityOrCountryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<List<HotelResponseDto>>> Handle(GetHotelsByCityOrCountryQuery request, CancellationToken cancellationToken)
    {
        List<HotelEntity> Hotels = new List<HotelEntity>();

        if(string.IsNullOrEmpty(request.City))
            Hotels = await unitOfWork.HotelRepo.GetByCountryAsync(request.Country);

        Hotels = await unitOfWork.HotelRepo.GetByCityAsync(request.City);

        var res =  Hotels.Select(h => mapper.Map<HotelResponseDto>(h)).ToList();

        return new Result<List<HotelResponseDto>>(res);
    }
}
