
using BookingClone.Application.Features.Room.Commands;
using FluentValidation;

namespace BookingClone.Application.Validators.RoomValidators;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.RoomNumber)
            .NotEmpty().WithMessage("Room number is required")
            .MaximumLength(20).WithMessage("Room number cannot exceed 20 characters");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be at least 1")
            .LessThanOrEqualTo(10).WithMessage("Capacity cannot exceed 10");

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0).WithMessage("Price per night must be positive");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid room type");

        RuleFor(x => x.HotelId)
            .GreaterThan(0).WithMessage("HotelId must be valid");


    }
}
