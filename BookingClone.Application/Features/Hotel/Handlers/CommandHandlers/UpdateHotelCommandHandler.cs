

using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Hotel.Commands;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Domain.IRepositories;
using MapsterMapper;
using MediatR;
using HotelEntity = BookingClone.Domain.Entities.Hotel;

namespace BookingClone.Application.Features.Hotel.Handlers.CommandHandlers;

public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, Result<HotelResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IRedisService redisService;

    public UpdateHotelCommandHandler(IUnitOfWork unitOfWork,
        IMapper mapper,
        IRedisService redisService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.redisService = redisService;
    }

    public async Task<Result<HotelResponseDto>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
    {
        var Hotel = await unitOfWork.HotelRepo.GetByIdAsync(request.Id);

        if (Hotel == null)
            throw new EntityNotFoundException("No such entity found");

        mapper.Map(request, Hotel);

        await unitOfWork.HotelRepo.UpdateAsync(Hotel);

        await redisService.RemoveDataAsync(MagicValues.HOTEL_REDIS_KEY + request.Id);
        await redisService.RemoveByTagAsync(MagicValues.HOTEL_PAGE_REDIS_TAG);

        HotelResponseDto hotelRes = mapper.Map<HotelResponseDto>(Hotel);

        return Result<HotelResponseDto>.CreateSuccessResult(hotelRes);
    }
}
