
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
    private readonly IFileUploadService uploadService;
    private readonly IMapper mapper;

    public AddRoomImageCommandHandler(IUnitOfWork unitOfWork,
        IFileUploadService uploadService,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.uploadService = uploadService;
        this.mapper = mapper;
    }

    public async Task<Result<RoomResponseDto>> Handle(AddRoomImageCommand request, CancellationToken cancellationToken)
    {
        var room = await unitOfWork.RoomRepo.GetByIdAsync(request.RoomId);

        if (room == null)
            throw new EntityNotFoundException("No room associated to this Id");

        string Url =  await uploadService.SaveImageAndGetUrl(request.Image, "rooms");

        await unitOfWork.RoomImageRepo.AddAsync(new RoomImage() { ImgUrl = Url, RoomId = request.RoomId});

        RoomResponseDto responseDto = mapper.Map<RoomResponseDto>(room);

        return new Result<RoomResponseDto>(responseDto);
    }
}
