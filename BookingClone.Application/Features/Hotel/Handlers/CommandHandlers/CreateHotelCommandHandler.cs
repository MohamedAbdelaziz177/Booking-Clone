
using BookingClone.Application.Features.Hotel.Commands;
using HotelEntity = BookingClone.Domain.Entities.Hotel;
using MediatR;
using BookingClone.Domain.IRepositories;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Application.Common;
using MapsterMapper;

namespace BookingClone.Application.Features.Hotel.Handlers.CommandHandlers;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Result<HotelResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateHotelCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<HotelResponseDto>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
       
        HotelEntity hotel = mapper.Map<HotelEntity>(request);

        await unitOfWork.HotelRepo.AddAsync(hotel);

        return new Result<HotelResponseDto>(mapper.Map<HotelResponseDto>(hotel));
        
    }

    
}
