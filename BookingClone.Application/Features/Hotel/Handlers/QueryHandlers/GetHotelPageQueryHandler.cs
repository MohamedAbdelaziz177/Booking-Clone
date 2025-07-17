


using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Application.Helpers;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using HotelEntity = BookingClone.Domain.Entities.Hotel;

namespace BookingClone.Application.Features.Hotel.Handlers.QueryHandlers;

public class GetHotelPageQueryHandler : IRequestHandler<GetHotelPageQuery, Result<List<HotelResponseDto>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IRedisService redisService;
    private readonly ILogger<GetHotelPageQueryHandler> logger;

    public GetHotelPageQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IRedisService redisService,
        ILogger<GetHotelPageQueryHandler> logger)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.redisService = redisService;
        this.logger = logger;
    }

    public async Task<Result<List<HotelResponseDto>>> Handle(GetHotelPageQuery request,
        CancellationToken cancellationToken)
    {

        string redisKey = RedisKeyFactory<GetHotelPageQuery>.GenerateRedisKey(request);
      
        var hotels = await redisService.GetDataAsync<List<HotelEntity>>(redisKey);

        List<HotelResponseDto> hotelResponseDtos = new List<HotelResponseDto>();

        if(hotels == null)
        {
            logger.LogInformation("Key Not found in Cache");
            hotels = await unitOfWork.HotelRepo.GetAllAsync(request.PageIdx,
            request.PageSize,
            request.SortField,
            request.SortType.ToString());

            await redisService.SetDataAsync(redisKey, hotels, MagicValues.HOTEL_PAGE_REDIS_TAG);
        }
           
      
        hotelResponseDtos = hotels.Select(h => 
        mapper.Map<HotelResponseDto>(h)).ToList();

        return Result<List<HotelResponseDto>>.CreateSuccessResult(hotelResponseDtos);
    }


   
}
