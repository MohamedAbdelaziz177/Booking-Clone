

using AutoMapper;
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Room.Queries;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Room.Handlers.QueryHandlers;

public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery, Result<RoomResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetRoomByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<RoomResponseDto>> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await unitOfWork.RoomRepo.GetByIdAsync(request.Id);

        if (room == null) 
            throw new EntityNotFoundException("Room Not existed");

        return ResultBuilder<RoomResponseDto>.CreateSuccessResponse(mapper.Map<RoomResponseDto>(room));
    }
}
