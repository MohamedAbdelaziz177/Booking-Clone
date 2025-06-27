
using AutoMapper;
using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;
using RoomEntity = BookingClone.Domain.Entities.Room;

namespace BookingClone.Application.Features.Room.Handlers.CommandHandlers;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Result<RoomResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task<Result<RoomResponseDto>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = mapper.Map<RoomEntity>(request);

        await unitOfWork.RoomRepo.AddAsync(room);

        RoomResponseDto roomResponseDto = mapper.Map<RoomResponseDto>(room);

        return ResultBuilder<RoomResponseDto>.CreateSuccessResponse(roomResponseDto);
    }
}
