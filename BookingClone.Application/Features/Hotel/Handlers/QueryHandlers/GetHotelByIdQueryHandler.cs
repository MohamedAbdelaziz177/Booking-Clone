
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Hotel.Queries;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BookingClone.Application.Features.Hotel.Handlers.QueryHandlers;

public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, Result<HotelResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetHotelByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<HotelResponseDto>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
    {
        var hotel = await unitOfWork.HotelRepo.GetByIdAsync(request.id);

        if (hotel == null)
            throw new EntityNotFoundException("No such Entity existed");

        return new Result<HotelResponseDto>(mapper.Map<HotelResponseDto>(hotel),
            true);
    }
}
