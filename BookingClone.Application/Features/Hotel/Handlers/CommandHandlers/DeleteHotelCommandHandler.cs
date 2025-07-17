
using BookingClone.Application.Common;
using BookingClone.Application.Contracts;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Hotel.Commands;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Hotel.Handlers.CommandHandlers;

public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Result<string>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IRedisService redisService;

    public DeleteHotelCommandHandler(IUnitOfWork unitOfWork, IRedisService redisService)
    {
        this.unitOfWork = unitOfWork;
        this.redisService = redisService;
    }

    public async Task<Result<string>> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        var Hotel = await unitOfWork.HotelRepo.GetByIdAsync(request.Id);

        if (Hotel == null)
            throw new EntityNotFoundException("No such entity found");

        await unitOfWork.HotelRepo.DeleteAsync(Hotel);

        await redisService.RemoveDataAsync(MagicValues.HOTEL_REDIS_KEY + request.Id.ToString());
        await redisService.RemoveByTagAsync(MagicValues.HOTEL_PAGE_REDIS_TAG);

        return Result<string>.CreateSuccessResult(data: "Deleted successfully");
    }
}
