

using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Room.Queries;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using RoomEntity = BookingClone.Domain.Entities.Room;

namespace BookingClone.Application.Features.Room.Handlers.QueryHandlers;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<RoomResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IRedisService redisService;

    public GetRoomByIdQueryHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IRedisService redisService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.redisService = redisService;
    }

    public async Task<Result<RoomResponseDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await redisService.GetDataAsync<RoomEntity>(MagicValues.ROOM_REDIS_KEY
            + request.Id.ToString());

        if (room != null)
            return new Result<RoomResponseDto>(mapper.Map<RoomResponseDto>(room)); ;

        room = await unitOfWork.RoomRepo.GetByIdAsync(request.Id);

        if (room == null) 
            throw new EntityNotFoundException("Room Not existed");

        return new Result<RoomResponseDto>(mapper.Map<RoomResponseDto>(room));
    }
}
