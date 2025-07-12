
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.Entities;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;

namespace BookingClone.Application.Features.Room.Handlers.CommandHandlers;

public class AddRoomImageCommandHandler : IRequestHandler<AddRoomImageCommand, Result<RoomResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly ICloudinaryService cloudinaryService;
    private readonly IMapper mapper;
    private readonly IRedisService redisService;

    public AddRoomImageCommandHandler(IUnitOfWork unitOfWork,
        ICloudinaryService uploadService,
        IMapper mapper,
        IRedisService redisService)
    {
        this.unitOfWork = unitOfWork;
        this.cloudinaryService = uploadService;
        this.mapper = mapper;
        this.redisService = redisService;
    }

    public async Task<Result<RoomResponseDto>> Handle(AddRoomImageCommand request, CancellationToken cancellationToken)
    {
        var room = await unitOfWork.RoomRepo.GetByIdAsync(request.RoomId);

        if (room == null)
            throw new EntityNotFoundException("No room associated to this Id");

        string Url =  await cloudinaryService.SaveImageAndGetUrl(request.Image, "rooms");

        await unitOfWork.RoomImageRepo.AddAsync(new RoomImage() { ImgUrl = Url, RoomId = request.RoomId});

        await redisService.RemoveDataAsync(MagicValues.ROOM_REDIS_KEY + request.RoomId);
        await redisService.RemoveByTagAsync(MagicValues.ROOM_PAGE_REDIS_TAG);

        RoomResponseDto responseDto = mapper.Map<RoomResponseDto>(room);

        return new Result<RoomResponseDto>(responseDto);
    }
}
