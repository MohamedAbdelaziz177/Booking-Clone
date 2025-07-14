
using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Queries;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;

namespace BookingClone.Application.Features.Room.Handlers.QueryHandlers;

public class GetAllRoomsAvailableBetweenQueryHandler :
    IRequestHandler<GetAllRoomsAvailableBetweenQuery, Result<List<RoomResponseDto>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAllRoomsAvailableBetweenQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task<Result<List<RoomResponseDto>>> Handle(GetAllRoomsAvailableBetweenQuery request, CancellationToken cancellationToken)
    {
        var rooms = await unitOfWork.RoomRepo.GetAvaliableRoomsBetween(request.start,
            request.end,
            request.hotelId,
            request.minPrice,
            request.maxPrice,
            request.PageIdx,
            request.PageSize,
            request.SortField,
            request.SortType
            );

        var response = rooms.Select(r => mapper.Map<RoomResponseDto>(r)).ToList();

        return new Result<List<RoomResponseDto>>(response);
    }
}
