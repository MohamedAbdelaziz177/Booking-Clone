
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using RoomEntity = BookingClone.Domain.Entities.Room;

namespace BookingClone.Application.Features.Room.Handlers.CommandHandlers;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Result<RoomResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ILogger<CreateRoomCommandHandler> logger;
    private readonly IRedisService redisService;

    public CreateRoomCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<CreateRoomCommandHandler> logger,
        IRedisService redisService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.logger = logger;
        this.redisService = redisService;
    }
    public async Task<Result<RoomResponseDto>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Request hotel id: " + request.HotelId.ToString());

        var room = mapper.Map<RoomEntity>(request);

        logger.LogInformation("entity hotel id: " + room.ToString());

        room.RoomNumber = Guid.NewGuid().ToString();

        await unitOfWork.RoomRepo.AddAsync(room);

        await redisService.RemoveByTagAsync(MagicValues.ROOM_PAGE_REDIS_TAG);

        RoomResponseDto roomResponseDto = mapper.Map<RoomResponseDto>(room);

        return new Result<RoomResponseDto>(roomResponseDto);
    }
}
