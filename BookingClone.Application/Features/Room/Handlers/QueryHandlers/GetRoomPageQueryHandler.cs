
using AutoMapper;
using BookingClone.Application.Common;
using BookingClone.Application.Features.Room.Queries;
using BookingClone.Application.Features.Room.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Room.Handlers.QueryHandlers;

public class GetRoomPageQueryHandler : IRequestHandler<GetRoomPageQuery, Result<List<RoomResponseDto>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetRoomPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    public async Task<Result<List<RoomResponseDto>>> Handle(GetRoomPageQuery request, CancellationToken cancellationToken)
    {
        var rooms = (!string.IsNullOrEmpty(request.SortField) &&
            !string.IsNullOrEmpty(request.SortType.ToString())) ?

            await unitOfWork.RoomRepo.GetAllAsync(request.PageIdx,
            request.PageSize,
            request.SortField,
            request.SortType.ToString()!) :

            await unitOfWork.RoomRepo.GetAllAsync(request.PageIdx,
            request.PageSize);

        List<RoomResponseDto> roomDtos = rooms.Select(r => mapper.Map<RoomResponseDto>(r)).ToList();

        return new Result<List<RoomResponseDto>>(roomDtos);
    }
}
