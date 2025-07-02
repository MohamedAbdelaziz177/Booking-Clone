
using BookingClone.Application.Features.Room.Commands;
using BookingClone.Domain.IRepositories;
using FluentValidation;

namespace BookingClone.Application.Validators.RoomValidators;

public class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    private readonly IRoomRepo roomRepo;

    public UpdateRoomCommandValidator(IRoomRepo roomRepo)
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Room ID must be valid.")
            .MustAsync(async (id, ct) => await CheckNotChangedId(id));

        RuleFor(x => x.RoomNumber)
            .NotEmpty().WithMessage("Room number is required.")
            .MaximumLength(20).WithMessage("Room number cannot exceed 20 characters.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be at least 1.")
            .LessThanOrEqualTo(10).WithMessage("Capacity cannot exceed 10.");

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0).WithMessage("Price per night must be positive.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid room type.");

        RuleFor(x => x.HotelId)
            .GreaterThan(0).WithMessage("HotelId must be valid.");
        this.roomRepo = roomRepo;
    }


    private async Task<bool> CheckNotChangedId(int id)
    {
        var room = await roomRepo.GetByIdAsync(id);
        return room != null && room.Id == id;
    }
}
