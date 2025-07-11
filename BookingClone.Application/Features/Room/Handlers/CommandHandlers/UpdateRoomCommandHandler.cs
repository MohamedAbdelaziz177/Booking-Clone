
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;

namespace BookingClone.Application.Features.Room.Handlers.CommandHandlers;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Result<RoomResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IRedisService redisService;

    public UpdateRoomCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IRedisService redisService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.redisService = redisService;
    }

    public async Task<Result<RoomResponseDto>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await unitOfWork.RoomRepo.GetByIdAsync(request.Id);

        if (room == null)
            throw new EntityNotFoundException("No such entity found");

        mapper.Map(request, room);

        await unitOfWork.RoomRepo.UpdateAsync(room);

        await redisService.RemoveDataAsync(MagicValues.ROOM_REDIS_KEY + request.Id);
        await redisService.RemoveByTagAsync(MagicValues.ROOM_PAGE_REDIS_TAG); 

        RoomResponseDto roomResponse = mapper.Map<RoomResponseDto>(room);

        return new Result<RoomResponseDto>(roomResponse, true, "Updated Successfully");
    }
}
