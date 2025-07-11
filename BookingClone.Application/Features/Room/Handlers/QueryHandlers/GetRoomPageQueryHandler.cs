
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Features.Room.Queries;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Application.Helpers;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RoomEntity = BookingClone.Domain.Entities.Room;
namespace BookingClone.Application.Features.Room.Handlers.QueryHandlers;

public class GetRoomPageQueryHandler : IRequestHandler<GetRoomPageQuery, Result<List<RoomResponseDto>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IRedisService redisService;
    private readonly ILogger<GetRoomPageQueryHandler> logger;

    public GetRoomPageQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IRedisService redisService,
        ILogger<GetRoomPageQueryHandler> logger)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.redisService = redisService;
        this.logger = logger;
    }
    public async Task<Result<List<RoomResponseDto>>> Handle(GetRoomPageQuery request, CancellationToken cancellationToken)
    {
        string redisKey = RedisKeyFactory<GetRoomPageQuery>.GenerateRedisKey(request);

        var rooms = await redisService.GetDataAsync<List<RoomEntity>>(redisKey);

        if(rooms == null)
        {
            logger.LogInformation("Key Not found in Cache");
            rooms = await unitOfWork.RoomRepo.GetAllAsync(request.PageIdx,
               request.PageSize,
               request.SortField,
               request.SortType.ToString());

            await redisService.SetDataAsync(redisKey, rooms, MagicValues.ROOM_PAGE_REDIS_TAG);
        }
         
        List<RoomResponseDto> roomDtos = rooms.Select(r => mapper.Map<RoomResponseDto>(r)).ToList();

        return new Result<List<RoomResponseDto>>(roomDtos);
    }
}
