
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Application.Helpers;
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using HotelEntity = BookingClone.Domain.Entities.Hotel;

namespace BookingClone.Application.Features.Hotel.Handlers.QueryHandlers;

public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, Result<HotelResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IRedisService redisService;
    private readonly ILogger<GetHotelByIdQueryHandler> logger;

    public GetHotelByIdQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IRedisService redisService,
        ILogger<GetHotelByIdQueryHandler> logger)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.redisService = redisService;
        this.logger = logger;
    }

    public async Task<Result<HotelResponseDto>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        string redisKey = RedisKeyFactory<GetHotelByIdQuery>.GenerateRedisKey(request);
        var hotelCache = await redisService.GetDataAsync<HotelEntity>(redisKey);


        if (hotelCache is not null)
        {
            logger.LogInformation("Key found in Cache");
            return new Result<HotelResponseDto>(mapper.Map<HotelResponseDto>(hotelCache),
            true);
        }
            


        var hotel = await unitOfWork.HotelRepo.GetByIdAsync(request.id);

        if (hotel == null)
            throw new EntityNotFoundException("No such Entity existed");

        await redisService.SetDataAsync<HotelEntity>(redisKey, hotel);


        return new Result<HotelResponseDto>(mapper.Map<HotelResponseDto>(hotel),
            true);
    }
}
