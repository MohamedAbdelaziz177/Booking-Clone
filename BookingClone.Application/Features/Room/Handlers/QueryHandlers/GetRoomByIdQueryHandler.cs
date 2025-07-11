

using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Room.Queries;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Application.Helpers;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RoomEntity = BookingClone.Domain.Entities.Room;

namespace BookingClone.Application.Features.Room.Handlers.QueryHandlers;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<RoomResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IRedisService redisService;
    private readonly ILogger<GetRoomByIdQueryHandler> logger;

    public GetRoomByIdQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IRedisService redisService,
        ILogger<GetRoomByIdQueryHandler> logger)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.redisService = redisService;
        this.logger = logger;
    }

    public async Task<Result<RoomResponseDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        string redisKey = RedisKeyFactory<GetRoomByIdQuery>.GenerateRedisKey(request);

        var room = await redisService.GetDataAsync<RoomEntity>(redisKey);

        if (room != null)
        {
            logger.LogInformation("Key found in Cache");
            return new Result<RoomResponseDto>(mapper.Map<RoomResponseDto>(room));
        }
            

        room = await unitOfWork.RoomRepo.GetByIdAsync(request.Id);

        if (room == null) 
            throw new EntityNotFoundException("Room Not existed");

        await redisService.SetDataAsync(redisKey, room);

        return new Result<RoomResponseDto>(mapper.Map<RoomResponseDto>(room));
    }
}
