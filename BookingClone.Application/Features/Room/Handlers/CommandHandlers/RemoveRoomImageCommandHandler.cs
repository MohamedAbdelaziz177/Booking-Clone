

using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Room.Handlers.CommandHandlers;

public class RemoveRoomImageCommandHandler : IRequestHandler<RemoveRoomImageCommand, Result<string>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IRedisService redisService;

    public RemoveRoomImageCommandHandler(IUnitOfWork unitOfWork, IRedisService redisService)
    {
        this.unitOfWork = unitOfWork;
        this.redisService = redisService;
    }
    public async Task<Result<string>> Handle(RemoveRoomImageCommand request, CancellationToken cancellationToken)
    {
        RoomImage? img = await unitOfWork.RoomImageRepo.GetByIdAsync(request.ImageId);

        if (img == null)
            throw new EntityNotFoundException("Image does not exist");

        await unitOfWork.RoomImageRepo.DeleteAsync(img);

        await redisService.RemoveDataAsync(MagicValues.ROOM_REDIS_KEY + img.RoomId);
        await redisService.RemoveByTagAsync(MagicValues.ROOM_PAGE_REDIS_TAG);

        return Result<string>.CreateSuccessResult("Deleted Successfully");
    }
}
