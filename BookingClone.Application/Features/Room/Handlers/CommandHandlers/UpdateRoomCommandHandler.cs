
using AutoMapper;
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Room.Handlers.CommandHandlers;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, Result<RoomResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public UpdateRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<RoomResponseDto>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await unitOfWork.RoomRepo.GetByIdAsync(request.Id);

        if (room == null)
            throw new EntityNotFoundException("No such entity found");

        mapper.Map(request, room);

        await unitOfWork.RoomRepo.UpdateAsync(room);

        RoomResponseDto roomResponse = mapper.Map<RoomResponseDto>(room);

        return new Result<RoomResponseDto>(roomResponse, true, "Updated Successfully");
    }
}
