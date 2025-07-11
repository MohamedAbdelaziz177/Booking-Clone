


using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
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
    private readonly IRedisService redisService;

    public GetHotelPageQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IRedisService redisService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.redisService = redisService;
    }

    public async Task<Result<List<HotelResponseDto>>> Handle(GetHotelPageQuery request,
        CancellationToken cancellationToken)
    {

        string RedisKey = GetPageRedisKey(request);
        var hotels = await redisService.GetDataAsync<List<HotelEntity>>(RedisKey);

        List<HotelResponseDto> hotelResponseDtos = new List<HotelResponseDto>();

        if (hotels != null)
            hotelResponseDtos = hotels.Select(h =>
            mapper.Map<HotelResponseDto>(h)).ToList();

        else
        {
           hotels = await unitOfWork.HotelRepo.GetAllAsync(request.PageIdx,
           request.PageSize,
           request.SortField,
           request.SortType.ToString());

            await redisService.SetDataAsync(RedisKey, hotels, MagicValues.HOTEL_PAGE_REDIS_TAG);
        }
           
      
        hotelResponseDtos = hotels.Select(h => 
        mapper.Map<HotelResponseDto>(h)).ToList();

        return new Result<List<HotelResponseDto>>(hotelResponseDtos);
    }


    private string GetPageRedisKey(GetHotelPageQuery request)
    {
        return MagicValues.HOTEL_REDIS_KEY + ": "
           + request.PageIdx.ToString() + " "
           + request.PageSize.ToString() + " "
           + request.SortField!.ToString().ToUpper() + " "
           + request.SortType.ToString()!.ToUpper();
    }
}
