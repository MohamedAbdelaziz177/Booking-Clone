﻿
using BookingClone.Application.Common;
using BookingClone.Application.Exceptions;
using BookingClone.Application.Features.Hotel.Commands;
using BookingClone.Application.Features.Hotel.Responses;
using BookingClone.Domain.IRepositories;
using MediatR;

namespace BookingClone.Application.Features.Hotel.Handlers.CommandHandlers;

public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Result<HotelResponseDto>>
{
    private readonly IUnitOfWork unitOfWork;

    public DeleteHotelCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<HotelResponseDto>> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
    {
        var Hotel = await unitOfWork.HotelRepo.GetByIdAsync(request.Id);

        if (Hotel == null)
            throw new EntityNotFoundException("No such entity found");

        await unitOfWork.HotelRepo.DeleteAsync(Hotel);

        return ResultBuilder<HotelResponseDto>.CreateSuccessResponse("Deleted Successfully");
    }
}
