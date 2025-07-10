
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using HotelEntity = BookingClone.Domain.Entities.Hotel;

namespace BookingClone.Application.Features.Hotel.Handlers.QueryHandlers;

public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, Result<HotelResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IRedisService redisService;

    public GetHotelByIdQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IRedisService redisService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.redisService = redisService;
    }

    public async Task<Result<HotelResponseDto>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        var hotelCache = await redisService.GetDataAsync<HotelEntity>(MagicValues.HOTEL_REDIS_KEY + request.id.ToString());

        if(hotelCache is not null)
            return new Result<HotelResponseDto>(mapper.Map<HotelResponseDto>(hotelCache),
            true);


        var hotel = await unitOfWork.HotelRepo.GetByIdAsync(request.id);

        if (hotel == null)
            throw new EntityNotFoundException("No such Entity existed");

        await redisService.SetDataAsync<HotelEntity>(MagicValues.HOTEL_REDIS_KEY + request.id.ToString(), hotel);

        return new Result<HotelResponseDto>(mapper.Map<HotelResponseDto>(hotel),
            true);
    }
}
